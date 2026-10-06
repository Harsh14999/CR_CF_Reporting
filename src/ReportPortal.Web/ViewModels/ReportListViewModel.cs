using System.Collections.Generic;

namespace ReportPortal.Web.ViewModels
{
    public class ReportListViewModel
    {
        public string ApplicationCode { get; set; }
        public IList<ReportItemViewModel> Reports { get; set; }
    }
}