using System;
using System.Collections.Generic;
using System.Linq;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Mock
{
    public class MockDocumentRepository : IDocumentRepository
    {
        public IList<ApplicationDocumentInfo> GetDocumentsForApplication(string applicationCode)
        {
            return MockDataStore.Documents
                .Where(d => d.IsActive && string.Equals(d.ApplicationCode, applicationCode, StringComparison.OrdinalIgnoreCase))
                .OrderBy(d => d.DisplayOrder)
                .ToList();
        }

        public ApplicationDocumentInfo GetDocument(int documentId)
        {
            return MockDataStore.Documents.FirstOrDefault(d => d.IsActive && d.DocumentId == documentId);
        }
    }
}