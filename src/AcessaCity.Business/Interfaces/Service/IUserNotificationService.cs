using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Interfaces.Service
{
    public interface IUserNotificationService : IDisposable
    {
        Task<IEnumerable<UserNotification>> GetAllUnreadUserNotifications(Guid userId);
        Task<bool> MarkAsRead(Guid notificationId);
        Task Add(UserNotification notification);
    }
}