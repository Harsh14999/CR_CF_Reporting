using System;
using System.Collections.Generic;
using System.Linq;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Mock
{
    public class MockApplicationRepository : IApplicationRepository
    {
        public IList<ApplicationInfo> GetAuthorizedApplications(string userName)
        {
            if (!string.Equals(userName, MockDataStore.SampleUserName, StringComparison.OrdinalIgnoreCase))
            {
                return new List<ApplicationInfo>();
            }

            return MockDataStore.Applications
                .Where(a => a.IsActive)
                .OrderBy(a => a.DisplayOrder)
                .Select(a => new ApplicationInfo
                {
                    ApplicationId = a.ApplicationId,
                    ApplicationCode = a.ApplicationCode,
                    ApplicationName = a.ApplicationName,
                    Description = a.Description,
                    IconCss = a.IconCss,
                    DisplayOrder = a.DisplayOrder,
                    IsActive = a.IsActive,
                    HasReports = MockDataStore.Reports.Any(r => r.ApplicationId == a.ApplicationId && r.IsActive),
                    HasDocuments = MockDataStore.Documents.Any(d => d.ApplicationId == a.ApplicationId && d.IsActive)
                })
                .ToList();
        }
    }
}