using System.Threading.Tasks;
using AcessaCity.Business.Dto.UserNotification;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.App.UserNotifications
{
    public class NotificationHandler
    {
        private readonly IUserNotificationService _userNotificationService;

        public NotificationHandler(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }

        public async Task SendUserNotification(CreateUserNotification notification)
        {
            UserNotification appNotification = new UserNotification()
            {
                Title = notification.Title,
                Description = notification.Description,
                UserId = notification.UserId,
                ReportId = notification.ReportId,
                InteractionHistoryId = notification.InteractionHistoryId,            
                Read = false,
            };

            await SendAppUserNotification(appNotification);
        }

        private async Task SendAppUserNotification(UserNotification notification)
        {
            await _userNotificationService.Add(notification);
        }
    }
}