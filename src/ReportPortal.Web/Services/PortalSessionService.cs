using System;
using System.Web;
using System.Web.Mvc;

namespace ReportPortal.Web.Services
{
    public class PortalSessionService
    {
        private const string CurrentUserSessionKey = "CurrentUser";
        private const string CurrentUserCookieName = "ReportPortalUser";

        public string GetCurrentUser(Controller controller)
        {
            var sessionUser = controller.Session == null ? null : controller.Session[CurrentUserSessionKey] as string;
            if (!string.IsNullOrWhiteSpace(sessionUser))
            {
                return sessionUser;
            }

            var cookie = controller.Request == null ? null : controller.Request.Cookies[CurrentUserCookieName];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
            {
                return null;
            }

            if (controller.Session != null)
            {
                controller.Session[CurrentUserSessionKey] = cookie.Value;
            }

            return cookie.Value;
        }

        public void SignIn(Controller controller, string userName, bool keepMeSignedIn)
        {
            controller.Session[CurrentUserSessionKey] = userName;

            if (!keepMeSignedIn)
            {
                return;
            }

            var cookie = new HttpCookie(CurrentUserCookieName, userName)
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(30)
            };
            controller.Response.Cookies.Add(cookie);
        }

        public void SignOut(Controller controller)
        {
            if (controller.Session != null)
            {
                controller.Session.Remove(CurrentUserSessionKey);
            }

            var cookie = new HttpCookie(CurrentUserCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            controller.Response.Cookies.Add(cookie);
        }
    }
}