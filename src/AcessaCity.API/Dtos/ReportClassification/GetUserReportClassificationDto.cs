using System;

namespace AcessaCity.API.Dtos.ReportClassification
{
    public class GetUserReportClassificationDto
    {
        public Guid UserId { get; set; }
        public Guid ReportId { get; set; }
    }
}