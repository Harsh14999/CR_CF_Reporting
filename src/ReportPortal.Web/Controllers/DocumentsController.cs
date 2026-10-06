using System.Collections.Generic;
using System.Web.Mvc;
using ReportPortal.Web.Models;
using ReportPortal.Web.Repositories;
using ReportPortal.Web.ViewModels;

namespace ReportPortal.Web.Controllers
{
    public class DocumentsController : BaseController
    {
        private readonly IAccessRepository accessRepository;
        private readonly IAuditRepository auditRepository;
        private readonly IDocumentRepository documentRepository;

        public DocumentsController()
        {
            accessRepository = RepositoryFactory.CreateAccessRepository();
            auditRepository = RepositoryFactory.CreateAuditRepository();
            documentRepository = RepositoryFactory.CreateDocumentRepository();
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

            var documents = documentRepository.GetDocumentsForApplication(applicationCode);
            return View(new DocumentListViewModel
            {
                ApplicationCode = applicationCode,
                Documents = ToDocumentItems(documents)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Open(int id)
        {
            if (!IsLoggedIn)
            {
                return RequireLogin();
            }

            var document = documentRepository.GetDocument(id);
            if (document == null || !accessRepository.UserHasApplicationAccess(CurrentUser, document.ApplicationCode))
            {
                return AccessDenied();
            }

            auditRepository.LogDocumentOpen(CurrentUser, document.ApplicationId, document.DocumentId, document.DocumentUrl);
            return Redirect(document.DocumentUrl);
        }

        private static IList<DocumentItemViewModel> ToDocumentItems(IEnumerable<ApplicationDocumentInfo> documents)
        {
            var items = new List<DocumentItemViewModel>();
            foreach (var document in documents)
            {
                items.Add(new DocumentItemViewModel
                {
                    DocumentId = document.DocumentId,
                    DocumentName = document.DocumentName,
                    DocumentDescription = document.DocumentDescription
                });
            }

            return items;
        }
    }
}