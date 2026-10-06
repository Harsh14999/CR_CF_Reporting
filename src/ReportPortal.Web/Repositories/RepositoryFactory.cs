using System;
using System.Configuration;
using ReportPortal.Web.Repositories.Mock;
using ReportPortal.Web.Repositories.Sql;

namespace ReportPortal.Web.Repositories
{
    public static class RepositoryFactory
    {
        public static IApplicationRepository CreateApplicationRepository()
        {
            return UseMockData() ? (IApplicationRepository)new MockApplicationRepository() : new SqlApplicationRepository(new SqlConnectionFactory());
        }

        public static IReportRepository CreateReportRepository()
        {
            return UseMockData() ? (IReportRepository)new MockReportRepository() : new SqlReportRepository(new SqlConnectionFactory());
        }

        public static IAccessRepository CreateAccessRepository()
        {
            return UseMockData() ? (IAccessRepository)new MockAccessRepository() : new SqlAccessRepository(new SqlConnectionFactory());
        }

        public static IAuditRepository CreateAuditRepository()
        {
            return UseMockData() ? (IAuditRepository)new MockAuditRepository() : new SqlAuditRepository(new SqlConnectionFactory());
        }

        public static IDocumentRepository CreateDocumentRepository()
        {
            return UseMockData() ? (IDocumentRepository)new MockDocumentRepository() : new SqlDocumentRepository(new SqlConnectionFactory());
        }

        private static bool UseMockData()
        {
            var value = ConfigurationManager.AppSettings["UseMockData"];
            return string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}