using System;

namespace AcessaCity.Business.Dto.UserNotification
{
    public class CreateUserNotification
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? InteractionHistoryId { get; set; }
        public string Description { get; set; }
    }
}