using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;

namespace AcessaCity.Business.Services
{
    public class UserNotificationService : ServiceBase, IUserNotificationService
    {
        private readonly IUserNotificationRepository _repository;
        
        public UserNotificationService(
            INotifier notifier,
            IUserNotificationRepository repository
            ) : base(notifier)
        {
            _repository = repository;
        }

        public async Task Add(UserNotification notification)
        {
            await _repository.Add(notification);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<IEnumerable<UserNotification>> GetAllUnreadUserNotifications(Guid userId)
        {
            return await _repository.Find(x => x.UserId == userId && x.Read == false);
        }

        public async Task<bool> MarkAsRead(Guid notificationId)
        {
            var notification = await _repository.GetById(notificationId);
            if (notification != null)
            {
                notification.Read = true;
                await _repository.Update(notification);
                return true;
            }

            return false;
        }
    }
}