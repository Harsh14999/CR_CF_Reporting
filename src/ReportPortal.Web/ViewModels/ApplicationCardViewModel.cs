namespace ReportPortal.Web.ViewModels
{
    public class ApplicationCardViewModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public string IconCss { get; set; }
        public bool HasReports { get; set; }
        public bool HasDocuments { get; set; }
    }
}