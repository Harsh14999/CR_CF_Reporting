using System;
using System.Collections.Generic;
using System.Linq;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Mock
{
    public class MockReportRepository : IReportRepository
    {
        public IList<ReportInfo> GetReportsForApplication(string applicationCode)
        {
            return MockDataStore.Reports
                .Where(r => r.IsActive && string.Equals(r.ApplicationCode, applicationCode, StringComparison.OrdinalIgnoreCase))
                .OrderBy(r => r.DisplayOrder)
                .ToList();
        }

        public ReportInfo GetReport(int reportId)
        {
            return MockDataStore.Reports.FirstOrDefault(r => r.IsActive && r.ReportId == reportId);
        }
    }
}