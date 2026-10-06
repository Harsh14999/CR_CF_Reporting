using System.Collections.Generic;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories
{
    public interface IDocumentRepository
    {
        IList<ApplicationDocumentInfo> GetDocumentsForApplication(string applicationCode);
        ApplicationDocumentInfo GetDocument(int documentId);
    }
}