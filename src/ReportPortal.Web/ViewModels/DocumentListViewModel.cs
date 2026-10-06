using System.Collections.Generic;

namespace ReportPortal.Web.ViewModels
{
    public class DocumentListViewModel
    {
        public string ApplicationCode { get; set; }
        public IList<DocumentItemViewModel> Documents { get; set; }
    }
}