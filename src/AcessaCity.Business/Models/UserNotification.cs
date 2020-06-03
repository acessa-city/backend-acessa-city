using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcessaCity.Business.Models
{
    public class UserNotification : Entity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ReportId { get; set; }
        public Guid? InteractionHistoryId { get; set; }
        public bool Read { get; set; }

        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
        
        [ForeignKey("InteractionHistoryId")]
        public virtual InteractionHistory InteractionHistory { get; set; }
    }
}