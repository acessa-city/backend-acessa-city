using System;
using System.Threading.Tasks;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Interfaces.Service
{
    public interface IReportClassificationService : IDisposable
    {
        Task UpdateUserRating(Guid userId, Guid reportId, int rating);
        Task<ReportClassification> FindRatingByUserAndReport(Guid userId, Guid reportId);
    }
}