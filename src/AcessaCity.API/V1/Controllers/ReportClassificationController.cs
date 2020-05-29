using System.Threading.Tasks;
using AcessaCity.API.Controllers;
using AcessaCity.API.Dtos.ReportClassification;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using AcessaCity.Business.Models;

namespace AcessaCity.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/report-classification")]    
    public class ReportClassificationController : MainController
    {
        private readonly IReportClassificationService _service;

        public ReportClassificationController(
            INotifier notifier,
            IReportClassificationService service) : base(notifier)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ReportClassification>> GetUserRating([FromQuery]GetUserReportClassificationDto filter)
        {
            return CustomResponse(await _service.FindRatingByUserAndReport(filter.UserId, filter.ReportId));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRating(ReportClassificationUpdateDto userClassification)
        {
            await _service.UpdateUserRating(userClassification.UserId, userClassification.ReportId, userClassification.Rating);
            return CustomResponse();            
        }
    }
}