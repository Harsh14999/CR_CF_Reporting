using System.Web.Mvc;
using ReportPortal.Web.Services;
using ReportPortal.Web.ViewModels;

namespace ReportPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly PortalSessionService sessionService;

        public AccountController()
        {
            sessionService = new PortalSessionService();
        }

        public ActionResult Login()
        {
            if (!string.IsNullOrWhiteSpace(sessionService.GetCurrentUser(this)))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            sessionService.SignIn(this, model.WindowsUserId.Trim(), model.KeepMeSignedIn);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            sessionService.SignOut(this);
            return RedirectToAction("Login", "Account");
        }
    }
}