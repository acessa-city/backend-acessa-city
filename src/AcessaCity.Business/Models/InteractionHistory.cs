using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcessaCity.Business.Models
{
   public class InteractionHistory: Entity
   {
      public Guid UserId { get; set; }
      public Guid ReportId { get; set; }
      public Guid NewReportStatusId { get; set; }
      public Guid? oldReportStatusId { get; set; }
      public string Description { get; set; }
      public DateTime CreationDate { get; set; }

      public virtual IEnumerable<InteractionHistoryCommentary> Commentaries { get; set; }
      [ForeignKey("UserId")]
      public virtual User User { get; set; }
      public virtual Report Report { get; set; }
      public virtual ReportStatus NewReportStatus { get; set; }
      public virtual ReportStatus OldReportStatus { get; set; }
   }
}