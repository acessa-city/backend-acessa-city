using System;
using System.Linq;
using System.Threading.Tasks;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Services
{
    public class ReportInProgressService : ServiceBase, IReportInProgressService
    {
        private readonly IReportInProgressRepository _repository;
        public ReportInProgressService(
            INotifier notifier,
            IReportInProgressRepository repository
            ) : base(notifier)
        {
            _repository = repository;
        }

        public async Task Add(ReportInProgress report)
        {
            await _repository.Add(report);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<ReportInProgress> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ReportInProgress> GetByReportId(Guid reportId)
        {
            var list = await _repository.Find(x => x.ReportId == reportId);
            return list.FirstOrDefault();
        }

        public Task Remove(ReportInProgress report)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ReportInProgress report)
        {
           await _repository.Update(report);
        }
    }
}