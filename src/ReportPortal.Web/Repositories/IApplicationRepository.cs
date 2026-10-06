using System.Collections.Generic;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories
{
    public interface IApplicationRepository
    {
        IList<ApplicationInfo> GetAuthorizedApplications(string userName);
    }
}