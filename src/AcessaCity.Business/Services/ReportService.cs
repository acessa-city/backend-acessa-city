using System;
using System.Linq;
using System.Threading.Tasks;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;
using AcessaCity.Business.Models.Constants;
using AcessaCity.Business.Models.Validations;

namespace AcessaCity.Business.Services
{
    public class ReportService : ServiceBase, IReportService
    {
        private readonly IReportRepository _repository;
        
        public ReportService(
            INotifier notifier,
            IReportRepository repository) : base(notifier)
        {
            _repository = repository;
        }

        public async Task Add(Report report)
        {
            if (!ExecuteValidation(new ReportValidation(), report)) return;

            await _repository.Add(report);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public DashboardReportInfo GetDashboardInfo(Guid cityId)
        {
            int approved = _repository
                .Find(x => x.CityId == cityId
                && x.ReportStatusId == StatusReport.Approved).Result.Count();

            int denied = _repository
                .Find(x => x.CityId == cityId
                && x.ReportStatusId == StatusReport.Denied).Result.Count();

            int inProgress = _repository
                .Find(x => x.CityId == cityId
                && x.ReportStatusId == StatusReport.InProgress).Result.Count();


            int inAnalysis = _repository
                .Find(x => x.CityId == cityId
                && x.ReportStatusId == StatusReport.InAnalysis).Result.Count();

            int finished = _repository
                .Find(x => x.CityId == cityId
                && x.ReportStatusId == StatusReport.Finished).Result.Count();

            return new DashboardReportInfo()
            {
                Approved = approved,
                Denied = denied,
                Finished = finished,
                InAnalysis = inAnalysis,
                InProgress = inProgress
            };
        }

        public DashboardReportInfo GetDashboardInfoByUser(Guid userId)
        {
            int approved = _repository
                .Find(x => x.UserId == userId
                && x.ReportStatusId == StatusReport.Approved).Result.Count();

            int denied = _repository
                .Find(x => x.UserId == userId
                && x.ReportStatusId == StatusReport.Denied).Result.Count();

            int inProgress = _repository
                .Find(x => x.UserId == userId
                && x.ReportStatusId == StatusReport.InProgress).Result.Count();


            int inAnalysis = _repository
                .Find(x => x.UserId == userId
                && x.ReportStatusId == StatusReport.InAnalysis).Result.Count();

            int finished = _repository
                .Find(x => x.UserId == userId
                && x.ReportStatusId == StatusReport.Finished).Result.Count();

            return new DashboardReportInfo()
            {
                Approved = approved,
                Denied = denied,
                Finished = finished,
                InAnalysis = inAnalysis,
                InProgress = inProgress
            };
        }

        public Task Remove(Report id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Report report)
        {
            throw new System.NotImplementedException();
        }
    }
}