using System.Collections.Generic;

namespace ReportPortal.Web.ViewModels
{
    public class DashboardViewModel
    {
        public string CurrentUser { get; set; }
        public IList<ApplicationCardViewModel> Applications { get; set; }
    }
}