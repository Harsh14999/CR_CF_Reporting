namespace ReportPortal.Web.Models
{
    public class ApplicationDocumentInfo
    {
        public int DocumentId { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}