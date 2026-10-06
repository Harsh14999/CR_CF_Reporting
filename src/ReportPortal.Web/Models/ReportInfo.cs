namespace ReportPortal.Web.Models
{
    public class ReportInfo
    {
        public int ReportId { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ReportServerPath { get; set; }
        public string OpenMode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}