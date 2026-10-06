using System.Collections.Generic;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Mock
{
    internal static class MockDataStore
    {
        public const string SampleUserName = "DOMAIN\\Harsh.V";
        private const string OrmsUatReportsUrl = "https://connectpbidev.dibuat.ae/reports/browse/ORMS-%20UAT";

        public static readonly IList<ApplicationInfo> Applications = new List<ApplicationInfo>
        {
            new ApplicationInfo { ApplicationId = 1, ApplicationCode = "ORMS", ApplicationName = "Operational Risk Management System", Description = "Operational risk reports and monitoring", IconCss = "icon-orms", DisplayOrder = 1, IsActive = true },
            new ApplicationInfo { ApplicationId = 2, ApplicationCode = "CADCRM", ApplicationName = "CADCRM", Description = "CADCRM reports and static documents", IconCss = "icon-cadcrm", DisplayOrder = 2, IsActive = true }
        };

        public static readonly IList<ReportInfo> Reports = new List<ReportInfo>
        {
            new ReportInfo { ReportId = 1, ApplicationId = 1, ApplicationCode = "ORMS", ApplicationName = "Operational Risk Management System", ReportName = "ORMS UAT Reports", ReportDescription = "Power BI Report Server UAT folder", ReportServerPath = OrmsUatReportsUrl, OpenMode = "NewTab", DisplayOrder = 1, IsActive = true },
            new ReportInfo { ReportId = 6, ApplicationId = 2, ApplicationCode = "CADCRM", ApplicationName = "CADCRM", ReportName = "CADCRM Summary Report", ReportServerPath = "/CADCRM/CADCRMSummary", OpenMode = "NewTab", DisplayOrder = 1, IsActive = true },
            new ReportInfo { ReportId = 7, ApplicationId = 2, ApplicationCode = "CADCRM", ApplicationName = "CADCRM", ReportName = "CADCRM Detail Report", ReportServerPath = "/CADCRM/CADCRMDetail", OpenMode = "NewTab", DisplayOrder = 2, IsActive = true }
        };

        public static readonly IList<ApplicationDocumentInfo> Documents = new List<ApplicationDocumentInfo>
        {
            new ApplicationDocumentInfo { DocumentId = 1, ApplicationId = 2, ApplicationCode = "CADCRM", ApplicationName = "CADCRM", DocumentName = "CADCRM Policy Document", DocumentDescription = "CADCRM policy and procedure document", DocumentUrl = "http://your-document-server/CADCRM/CADCRMPolicy.pdf", DisplayOrder = 1, IsActive = true },
            new ApplicationDocumentInfo { DocumentId = 2, ApplicationId = 2, ApplicationCode = "CADCRM", ApplicationName = "CADCRM", DocumentName = "CADCRM User Guide", DocumentDescription = "CADCRM user guide", DocumentUrl = "http://your-document-server/CADCRM/CADCRMUserGuide.pdf", DisplayOrder = 2, IsActive = true }
        };
    }
}