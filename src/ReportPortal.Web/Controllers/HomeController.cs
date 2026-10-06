using System.Collections.Generic;
using System.Web.Mvc;
using ReportPortal.Web.Models;
using ReportPortal.Web.Repositories;
using ReportPortal.Web.ViewModels;

namespace ReportPortal.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IApplicationRepository applicationRepository;

        public HomeController()
        {
            applicationRepository = RepositoryFactory.CreateApplicationRepository();
        }

        public ActionResult Index()
        {
            if (!IsLoggedIn)
            {
                return RequireLogin();
            }

            var applications = applicationRepository.GetAuthorizedApplications(CurrentUser);
            return View(new DashboardViewModel
            {
                CurrentUser = CurrentUser,
                Applications = ToApplicationCards(applications)
            });
        }

        private static IList<ApplicationCardViewModel> ToApplicationCards(IEnumerable<ApplicationInfo> applications)
        {
            var items = new List<ApplicationCardViewModel>();
            foreach (var application in applications)
            {
                items.Add(new ApplicationCardViewModel
                {
                    ApplicationId = application.ApplicationId,
                    ApplicationCode = application.ApplicationCode,
                    ApplicationName = application.ApplicationName,
                    Description = application.Description,
                    IconCss = application.IconCss,
                    HasReports = application.HasReports,
                    HasDocuments = application.HasDocuments
                });
            }

            return items;
        }
    }
}