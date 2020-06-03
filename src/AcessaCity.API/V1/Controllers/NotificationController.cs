using System;
using System.Threading.Tasks;
using AcessaCity.API.Controllers;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace AcessaCity.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/notifications")]
    public class NotificationController : MainController
    {
        private readonly IUserNotificationService _service;

        public NotificationController(
            INotifier notifier,
            IUserNotificationService userNotificationService
        ) : base(notifier)
        {
            _service = userNotificationService;

        }

        [HttpPut("{notificationId:guid}/mark-as-read")]
        public async Task<ActionResult> MarkNotificationAsRead(Guid notificationId)
        {
            return CustomResponse(await _service.MarkAsRead(notificationId));
        }
    }
}