using System;
using System.Linq;

namespace ReportPortal.Web.Repositories.Mock
{
    public class MockAccessRepository : IAccessRepository
    {
        public bool UserHasApplicationAccess(string userName, string applicationCode)
        {
            return string.Equals(userName, MockDataStore.SampleUserName, StringComparison.OrdinalIgnoreCase)
                && MockDataStore.Applications.Any(a => a.IsActive && string.Equals(a.ApplicationCode, applicationCode, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserHasReportAccess(string userName, int reportId)
        {
            var report = MockDataStore.Reports.FirstOrDefault(r => r.IsActive && r.ReportId == reportId);
            return report != null && UserHasApplicationAccess(userName, report.ApplicationCode);
        }
    }
}