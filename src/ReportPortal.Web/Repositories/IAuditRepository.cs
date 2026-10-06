namespace ReportPortal.Web.Repositories
{
    public interface IAuditRepository
    {
        void LogReportLaunch(string userName, int applicationId, int reportId, string reportUrl, string parametersJson);
        void LogDocumentOpen(string userName, int applicationId, int documentId, string documentUrl);
    }
}