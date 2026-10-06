USE [ReportPortalDb];
GO

DECLARE @SampleUser nvarchar(200) = N'DOMAIN\Harsh.V';
DECLARE @OrmsId int;
DECLARE @CadcrmId int;

IF NOT EXISTS (SELECT 1 FROM dbo.Applications WHERE ApplicationCode = N'ORMS')
BEGIN
    INSERT INTO dbo.Applications (ApplicationCode, ApplicationName, Description, IconCss, DisplayOrder, IsActive)
    VALUES (N'ORMS', N'Operational Risk Management System', N'Operational risk reports and monitoring', N'icon-orms', 1, 1);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Applications WHERE ApplicationCode = N'CADCRM')
BEGIN
    INSERT INTO dbo.Applications (ApplicationCode, ApplicationName, Description, IconCss, DisplayOrder, IsActive)
    VALUES (N'CADCRM', N'CADCRM', N'CADCRM reports and static documents', N'icon-cadcrm', 2, 1);
END

SELECT @OrmsId = ApplicationId FROM dbo.Applications WHERE ApplicationCode = N'ORMS';
SELECT @CadcrmId = ApplicationId FROM dbo.Applications WHERE ApplicationCode = N'CADCRM';

IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @OrmsId AND ReportServerPath = N'/ORMS/RptRCSADetailSA')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@OrmsId, N'RCSA Detail SA', N'/ORMS/RptRCSADetailSA', N'NewTab', 1, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @OrmsId AND ReportServerPath = N'/ORMS/RptRCSADetailInAu')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@OrmsId, N'RCSA Detail Input Authorization', N'/ORMS/RptRCSADetailInAu', N'NewTab', 2, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @OrmsId AND ReportServerPath = N'/ORMS/RPTEventDetail')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@OrmsId, N'Event Detail', N'/ORMS/RPTEventDetail', N'NewTab', 3, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @OrmsId AND ReportServerPath = N'/ORMS/RptKRI')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@OrmsId, N'KRI Report', N'/ORMS/RptKRI', N'NewTab', 4, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @OrmsId AND ReportServerPath = N'/ORMS/RptFRA')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@OrmsId, N'FRA Report', N'/ORMS/RptFRA', N'NewTab', 5, 1);

IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @CadcrmId AND ReportServerPath = N'/CADCRM/CADCRMSummary')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@CadcrmId, N'CADCRM Summary Report', N'/CADCRM/CADCRMSummary', N'NewTab', 1, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Reports WHERE ApplicationId = @CadcrmId AND ReportServerPath = N'/CADCRM/CADCRMDetail')
    INSERT INTO dbo.Reports (ApplicationId, ReportName, ReportServerPath, OpenMode, DisplayOrder, IsActive) VALUES (@CadcrmId, N'CADCRM Detail Report', N'/CADCRM/CADCRMDetail', N'NewTab', 2, 1);

IF NOT EXISTS (SELECT 1 FROM dbo.ApplicationDocuments WHERE ApplicationId = @CadcrmId AND DocumentName = N'CADCRM Policy Document')
    INSERT INTO dbo.ApplicationDocuments (ApplicationId, DocumentName, DocumentDescription, DocumentUrl, DisplayOrder, IsActive) VALUES (@CadcrmId, N'CADCRM Policy Document', N'CADCRM policy and procedure document', N'http://your-document-server/CADCRM/CADCRMPolicy.pdf', 1, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.ApplicationDocuments WHERE ApplicationId = @CadcrmId AND DocumentName = N'CADCRM User Guide')
    INSERT INTO dbo.ApplicationDocuments (ApplicationId, DocumentName, DocumentDescription, DocumentUrl, DisplayOrder, IsActive) VALUES (@CadcrmId, N'CADCRM User Guide', N'CADCRM user guide', N'http://your-document-server/CADCRM/CADCRMUserGuide.pdf', 2, 1);

IF NOT EXISTS (SELECT 1 FROM dbo.UserApplicationAccess WHERE UserName = @SampleUser AND ApplicationId = @OrmsId)
    INSERT INTO dbo.UserApplicationAccess (UserName, ApplicationId, IsActive) VALUES (@SampleUser, @OrmsId, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.UserApplicationAccess WHERE UserName = @SampleUser AND ApplicationId = @CadcrmId)
    INSERT INTO dbo.UserApplicationAccess (UserName, ApplicationId, IsActive) VALUES (@SampleUser, @CadcrmId, 1);
GO