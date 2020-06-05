using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcessaCity.API.Controllers;
using AcessaCity.API.Dtos.Report;
using AcessaCity.Business.App.Reports;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcessaCity.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/report")]        
    public class ReportController : MainController
    {
        private readonly IReportService _service;
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGeolocationService _geolocation;
        private readonly ICityRepository _cityRepository;
        private readonly IReportInProgressService _reportInProgressService;
        
        public ReportController(
            INotifier notifier,
            IReportRepository repository,
            IReportService service,
            IMapper mapper,
            IGeolocationService geolocation,
            ICityRepository cityRepository,
            IReportInProgressService reportInProgressService) : base(notifier)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
            _geolocation = geolocation;
            _cityRepository = cityRepository;
            _reportInProgressService = reportInProgressService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var report = await _repository.GetById(id);
            
            return Ok(report);
        }

        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery]Guid category, 
            [FromQuery]DateTime date,
            [FromQuery]Guid status,
            [FromQuery]Guid city,
            [FromQuery]string street,
            [FromQuery]string neighborhood,
            [FromQuery]Guid coordinatorId,
            [FromQuery]Guid userId)
        {
            var fStreet = street == null ? "" : street.ToLower();
            var fNeighborhood = neighborhood == null ? "" : neighborhood.ToLower();
            var reportList = await _repository.Find(r =>
                (r.CategoryId == category || category == Guid.Empty)
                &&
                (r.ReportStatusId == status || status == Guid.Empty)
                &&
                (r.CoordinatorId == coordinatorId || coordinatorId == Guid.Empty)
                &&                
                (r.CityId == city || city == Guid.Empty)
                &&                
                (r.UserId == userId || userId == Guid.Empty)
                &&
                (r.Street.ToLower().Contains(fStreet.ToLower()) || street == null)
                &&
                (r.Neighborhood.ToLower().Contains(fNeighborhood.ToLower()) || neighborhood == null)                
                &&
                ((r.CreationDate.DayOfYear == date.DayOfYear) || date.DayOfYear == DateTime.MinValue.DayOfYear)
            );

            return CustomResponse(reportList);            
        }

        [HttpGet("{id:guid}/{date:datetime}")]
        public async Task<ActionResult> Get([FromQuery]Guid? category, [FromQuery]DateTime date)
        {
            var reportList = await _repository.Find(r =>
                (r.CategoryId == category || category == Guid.Empty) &
                (r.CreationDate.DayOfYear == date.DayOfYear || date == null) 
            );

            return CustomResponse(reportList.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            ReportInsertDto report,
            [FromServices] ReportStatusUpdate statusUpdater          
            )
        {
            Report newReport = _mapper.Map<Report>(report);
            newReport.CreationDate = DateTime.Now;

            var city = await _geolocation.GetCityInfoFromLocation(
                report.Latitude,
                report.Longitude
            );

            IEnumerable<City> cities = new List<City>();

            if (city != null)
            {
                cities = await _cityRepository.Find(
                    c => c.Name.ToLower().Contains(city.Name.ToLower())
                ); 
            }

            var cityFromRepo = cities.FirstOrDefault();
            if (cityFromRepo == null)
            {
                NotifyError("As coordenadas informadas não estão cadastradas.");
                return CustomResponse();
            }

            newReport.CityId = cityFromRepo.Id;
            newReport.Street = city.Street;
            newReport.Neighborhood = city.Neighborhood;
            await _service.Add(newReport);

            if (ValidOperation())
            {
                var created = await _repository.GetById(newReport.Id);

                await statusUpdater.StatusUpdate(
                    newReport.UserId,
                    newReport.Id,
                    newReport.ReportStatusId,
                    "Denúncia criada"
                );

                return CreatedAtAction(nameof(GetById), new {Id = newReport.Id, Version = "1.0"}, created);                
            }

            return CustomResponse();
        }

        [HttpPost("{reportId:guid}/status-update")]
        public async Task<ActionResult> StatusUpdate(
            Guid reportId, 
            ReportStatusUpdateDto status,
            [FromServices]ReportStatusUpdate updater)
        {
            await updater.StatusUpdate(status.UserId, reportId, status.ReportStatusId, status.Description);
            return Ok();
        }

        [HttpPost("{reportId:guid}/coordinator-update")]
        public async Task<ActionResult> CoordinatorUpdate(
            Guid reportId, 
            ReportCoordinatorUpdateDto coordinator,
            [FromServices]ReportCoordinatorUpdate updater)
        {
            await updater.CoordinatorUpdate(reportId, coordinator.CoordinatorId);            
            return Ok();
        }

        [HttpPost("start-progress")]
        public async Task<ActionResult> ReportStartProgress(
            ReportStartProgressDto progress,
            [FromServices]ReportStatusUpdate updater)
        {
            Guid statusInProgress = Guid.Parse("c37d9588-1875-44dd-8cf1-6781de7533c3");
            await updater.StatusUpdate(progress.UserId, progress.ReportId, statusInProgress, progress.Description);
            
            return Ok(progress);
        }

        [HttpPost("end-progress")]
        public async Task<ActionResult> ReportStartProgress(
            ReportEndProgressDto progress,
            [FromServices]ReportStatusUpdate updater)
        {
            Guid statusInProgress = Guid.Parse("ee6dda1a-51e2-4041-9d21-7f5c8f2e94b0");
            await updater.StatusUpdate(progress.UserId, progress.ReportId, statusInProgress, progress.Description);

            var inProgress = await _reportInProgressService.GetByReportId(progress.ReportId);
            inProgress.DoneDate = progress.EndDate;
            inProgress.OwnerUserId = progress.UserId;
            await _reportInProgressService.Update(inProgress);
                        
            return Ok(progress);
        }        
    }
}