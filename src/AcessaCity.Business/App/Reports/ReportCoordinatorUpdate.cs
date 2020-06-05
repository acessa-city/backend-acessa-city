using System;
using System.Threading.Tasks;
using AcessaCity.Business.App.UserNotifications;
using AcessaCity.Business.Dto.UserNotification;
using AcessaCity.Business.Interfaces.Repository;

namespace AcessaCity.Business.App.Reports
{
    public class ReportCoordinatorUpdate
    {
        private readonly IReportRepository _reportRepository;        
        private readonly NotificationHandler _notificationHandler;

        public ReportCoordinatorUpdate(
            IReportRepository reportRepo,
            NotificationHandler notificationHandler)
        {
            _reportRepository = reportRepo;        
            _notificationHandler = notificationHandler;    
        }

        public async Task<bool> CoordinatorUpdate(Guid reportId, Guid coordinatorId)
        {
            var reportToUpdate = await _reportRepository.GetById(reportId);

            reportToUpdate.CoordinatorId = coordinatorId;
            await _reportRepository.Update(reportToUpdate);

            CreateUserNotification notification = new CreateUserNotification()
            {
                Description = "Uma nova denúncia foi atribuída para você.",
                ReportId = reportId,
                Title = "Nova denúncia moderada.",
                UserId = coordinatorId
            };
            
            await _notificationHandler.SendUserNotification(notification);
            
            return true;
        }
    }
}