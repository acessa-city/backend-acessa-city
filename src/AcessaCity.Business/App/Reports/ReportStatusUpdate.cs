using System;
using System.Linq;
using System.Threading.Tasks;
using AcessaCity.Business.App.UserNotifications;
using AcessaCity.Business.Dto.UserNotification;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.App.Reports
{
    public class ReportStatusUpdate
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportInteractionHistoryRepository _interactionRepository;
        private readonly IReportInProgressRepository _progressRepo;
        private readonly IReportStatusRepository _statusRepository;
        private readonly NotificationHandler _notificationHandler;

        public ReportStatusUpdate(
            IReportRepository reportRepository,
            IReportInteractionHistoryRepository interactionRepository,
            IReportInProgressRepository progressRepo,
            IReportStatusRepository statusRepository,
            NotificationHandler notificationHandler
            )
        {
            _interactionRepository = interactionRepository;
            _reportRepository = reportRepository;
            _progressRepo = progressRepo;
            _statusRepository = statusRepository;
            _notificationHandler = notificationHandler;
        }

        private async Task SendNotifications(Report report, Guid interactionId)
        {
            InteractionHistory interaction = await _interactionRepository.GetById(interactionId);
            string description = "A denúncia {0} sofreu uma atualização de status.\n Data de alteração: {1}";

            CreateUserNotification notification = new CreateUserNotification()
            {
               Description = string.Format(description, report.Title, interaction.CreationDate.ToShortDateString()),
               ReportId = report.Id,
               Title = $"Alteração de status na denúncia {report.Title}",
               InteractionHistoryId = interactionId,
               UserId = report.UserId
            };

            await _notificationHandler.SendUserNotification(notification);
        }
        
        public async Task<bool> StatusUpdate(Guid userId, Guid reportId, Guid newStatusId, string updateDescription)
        {
            var reportToUpdate = await _reportRepository.GetById(reportId);
            
            Guid oldStatus = reportToUpdate.ReportStatusId;
            reportToUpdate.ReportStatusId = newStatusId;

            await _reportRepository.Update(reportToUpdate);

            InteractionHistory interaction = new InteractionHistory()
            {
                UserId = userId,
                ReportId = reportId,
                oldReportStatusId = oldStatus,
                NewReportStatusId = newStatusId,
                Description = updateDescription,
                CreationDate = DateTime.Now
            };

            await _interactionRepository.Add(interaction);

            var currentStatus = await _statusRepository.GetById(newStatusId);

            if (currentStatus.InProgress)
            {
                var records = await _progressRepo.Find(x => x.ReportId == reportToUpdate.Id);
                
                if (!records.Any())
                {
                    ReportInProgress inProgress = new ReportInProgress()
                    {
                        InteractionHistoryId = interaction.Id,
                        ReportId = reportToUpdate.Id,
                        UserId = interaction.UserId,                        
                        CreationDate = DateTime.Now
                    };

                    //TO-DO: Disparar notificacao para os usuários
                    await _progressRepo.Add(inProgress);
                }
            }

            await SendNotifications(reportToUpdate, interaction.Id);

            return true;
        }
    }
}