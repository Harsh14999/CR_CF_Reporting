namespace ReportPortal.Web.Repositories
{
    public interface IAccessRepository
    {
        bool UserHasApplicationAccess(string userName, string applicationCode);
        bool UserHasReportAccess(string userName, int reportId);
    }
}