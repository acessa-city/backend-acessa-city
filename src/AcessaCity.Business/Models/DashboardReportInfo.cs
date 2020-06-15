namespace AcessaCity.Business.Models
{
    public class DashboardReportInfo
    {
        public int InProgress { get; set; }
        public int Denied { get; set; }
        public int InAnalysis { get; set; }
        public int Approved { get; set; }
        public int Finished { get; set; }
    }
}