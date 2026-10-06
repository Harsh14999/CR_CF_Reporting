using System.Collections.Generic;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories
{
    public interface IReportRepository
    {
        IList<ReportInfo> GetReportsForApplication(string applicationCode);
        ReportInfo GetReport(int reportId);
    }
}