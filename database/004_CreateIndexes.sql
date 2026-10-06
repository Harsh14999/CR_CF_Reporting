USE [ReportPortalDb];
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_Applications_ApplicationCode' AND object_id = OBJECT_ID(N'dbo.Applications'))
    CREATE UNIQUE INDEX UX_Applications_ApplicationCode ON dbo.Applications(ApplicationCode);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Reports_Application_Active_Order' AND object_id = OBJECT_ID(N'dbo.Reports'))
    CREATE INDEX IX_Reports_Application_Active_Order ON dbo.Reports(ApplicationId, IsActive, DisplayOrder);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_UserApplicationAccess_User_Application' AND object_id = OBJECT_ID(N'dbo.UserApplicationAccess'))
    CREATE UNIQUE INDEX UX_UserApplicationAccess_User_Application ON dbo.UserApplicationAccess(UserName, ApplicationId);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_UserApplicationAccess_User_Active' AND object_id = OBJECT_ID(N'dbo.UserApplicationAccess'))
    CREATE INDEX IX_UserApplicationAccess_User_Active ON dbo.UserApplicationAccess(UserName, IsActive);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_UserReportAccess_User_Report' AND object_id = OBJECT_ID(N'dbo.UserReportAccess'))
    CREATE UNIQUE INDEX UX_UserReportAccess_User_Report ON dbo.UserReportAccess(UserName, ReportId);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ApplicationDocuments_Application_Active_Order' AND object_id = OBJECT_ID(N'dbo.ApplicationDocuments'))
    CREATE INDEX IX_ApplicationDocuments_Application_Active_Order ON dbo.ApplicationDocuments(ApplicationId, IsActive, DisplayOrder);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ReportExecutionLog_User_ExecutedOn' AND object_id = OBJECT_ID(N'dbo.ReportExecutionLog'))
    CREATE INDEX IX_ReportExecutionLog_User_ExecutedOn ON dbo.ReportExecutionLog(UserName, ExecutedOn);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_DocumentAccessLog_User_OpenedOn' AND object_id = OBJECT_ID(N'dbo.DocumentAccessLog'))
    CREATE INDEX IX_DocumentAccessLog_User_OpenedOn ON dbo.DocumentAccessLog(UserName, OpenedOn);
GO