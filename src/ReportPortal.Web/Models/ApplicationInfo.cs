namespace ReportPortal.Web.Models
{
    public class ApplicationInfo
    {
        public int ApplicationId { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public string IconCss { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool HasReports { get; set; }
        public bool HasDocuments { get; set; }
    }
}