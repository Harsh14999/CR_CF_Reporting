using System.Configuration;
using System.Web.Mvc;
using ReportPortal.Web.Services;

namespace ReportPortal.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly PortalSessionService SessionService;

        protected BaseController()
        {
            SessionService = new PortalSessionService();
        }

        protected string CurrentUser
        {
            get { return SessionService.GetCurrentUser(this); }
        }

        protected bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(CurrentUser); }
        }

        protected ActionResult RequireLogin()
        {
            return RedirectToAction("Login", "Account");
        }

        protected ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.UseMockData = string.Equals(ConfigurationManager.AppSettings["UseMockData"], "true", System.StringComparison.OrdinalIgnoreCase);
            base.OnActionExecuting(filterContext);
        }
    }
}