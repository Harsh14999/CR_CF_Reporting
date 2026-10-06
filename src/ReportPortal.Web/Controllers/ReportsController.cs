using System.Collections.Generic;
using System.Web.Mvc;
using ReportPortal.Web.Models;
using ReportPortal.Web.Repositories;
using ReportPortal.Web.Services;
using ReportPortal.Web.ViewModels;

namespace ReportPortal.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IAccessRepository accessRepository;
        private readonly IAuditRepository auditRepository;
        private readonly IReportRepository reportRepository;
        private readonly ReportUrlBuilder reportUrlBuilder;

        public ReportsController()
        {
            accessRepository = RepositoryFactory.CreateAccessRepository();
            auditRepository = RepositoryFactory.CreateAuditRepository();
            reportRepository = RepositoryFactory.CreateReportRepository();
            reportUrlBuilder = new ReportUrlBuilder();
        }

        public ActionResult Application(string applicationCode)
        {
            if (!IsLoggedIn)
            {
                return RequireLogin();
            }

            if (!accessRepository.UserHasApplicationAccess(CurrentUser, applicationCode))
            {
                return AccessDenied();
            }

            var reports = reportRepository.GetReportsForApplication(applicationCode);
            return View(new ReportListViewModel
            {
                ApplicationCode = applicationCode,
                Reports = ToReportItems(reports)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Launch(int id)
        {
            if (!IsLoggedIn)
            {
                return RequireLogin();
            }

            var report = reportRepository.GetReport(id);
            if (report == null || !accessRepository.UserHasReportAccess(CurrentUser, id))
            {
                return AccessDenied();
            }

            var url = reportUrlBuilder.BuildReportUrl(report.ReportServerPath, new Dictionary<string, string>());
            auditRepository.LogReportLaunch(CurrentUser, report.ApplicationId, report.ReportId, url, null);
            return Redirect(url);
        }

        private static IList<ReportItemViewModel> ToReportItems(IEnumerable<ReportInfo> reports)
        {
            var items = new List<ReportItemViewModel>();
            foreach (var report in reports)
            {
                items.Add(new ReportItemViewModel
                {
                    ReportId = report.ReportId,
                    ReportName = report.ReportName,
                    ReportDescription = report.ReportDescription,
                    OpenMode = report.OpenMode
                });
            }

            return items;
        }
    }
}