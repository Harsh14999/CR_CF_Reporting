namespace ReportPortal.Web.Repositories.Mock
{
    public class MockAuditRepository : IAuditRepository
    {
        public void LogReportLaunch(string userName, int applicationId, int reportId, string reportUrl, string parametersJson)
        {
        }

        public void LogDocumentOpen(string userName, int applicationId, int documentId, string documentUrl)
        {
        }
    }
}