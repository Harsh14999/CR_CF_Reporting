USE [ReportPortalDb];
GO

IF OBJECT_ID(N'dbo.Applications', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Applications
    (
        ApplicationId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Applications PRIMARY KEY,
        ApplicationCode nvarchar(50) NOT NULL,
        ApplicationName nvarchar(200) NOT NULL,
        Description nvarchar(500) NULL,
        IconCss nvarchar(100) NULL,
        DisplayOrder int NOT NULL CONSTRAINT DF_Applications_DisplayOrder DEFAULT (0),
        IsActive bit NOT NULL CONSTRAINT DF_Applications_IsActive DEFAULT (1)
    );
END
GO

IF OBJECT_ID(N'dbo.Reports', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Reports
    (
        ReportId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Reports PRIMARY KEY,
        ApplicationId int NOT NULL,
        ReportName nvarchar(200) NOT NULL,
        ReportDescription nvarchar(500) NULL,
        ReportServerPath nvarchar(500) NOT NULL,
        OpenMode nvarchar(50) NOT NULL CONSTRAINT DF_Reports_OpenMode DEFAULT (N'NewTab'),
        DisplayOrder int NOT NULL CONSTRAINT DF_Reports_DisplayOrder DEFAULT (0),
        IsActive bit NOT NULL CONSTRAINT DF_Reports_IsActive DEFAULT (1),
        CONSTRAINT FK_Reports_Applications FOREIGN KEY (ApplicationId) REFERENCES dbo.Applications(ApplicationId)
    );
END
GO

IF OBJECT_ID(N'dbo.UserApplicationAccess', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserApplicationAccess
    (
        Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_UserApplicationAccess PRIMARY KEY,
        UserName nvarchar(200) NOT NULL,
        ApplicationId int NOT NULL,
        IsActive bit NOT NULL CONSTRAINT DF_UserApplicationAccess_IsActive DEFAULT (1),
        CONSTRAINT FK_UserApplicationAccess_Applications FOREIGN KEY (ApplicationId) REFERENCES dbo.Applications(ApplicationId)
    );
END
GO

IF OBJECT_ID(N'dbo.UserReportAccess', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserReportAccess
    (
        Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_UserReportAccess PRIMARY KEY,
        UserName nvarchar(200) NOT NULL,
        ReportId int NOT NULL,
        CanView bit NOT NULL CONSTRAINT DF_UserReportAccess_CanView DEFAULT (1),
        CONSTRAINT FK_UserReportAccess_Reports FOREIGN KEY (ReportId) REFERENCES dbo.Reports(ReportId)
    );
END
GO

IF OBJECT_ID(N'dbo.ReportExecutionLog', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ReportExecutionLog
    (
        LogId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ReportExecutionLog PRIMARY KEY,
        UserName nvarchar(200) NOT NULL,
        ApplicationId int NOT NULL,
        ReportId int NOT NULL,
        ReportUrl nvarchar(max) NOT NULL,
        ParametersJson nvarchar(max) NULL,
        ExecutedOn datetime NOT NULL CONSTRAINT DF_ReportExecutionLog_ExecutedOn DEFAULT (GETDATE())
    );
END
GO

IF OBJECT_ID(N'dbo.ApplicationDocuments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ApplicationDocuments
    (
        DocumentId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ApplicationDocuments PRIMARY KEY,
        ApplicationId int NOT NULL,
        DocumentName nvarchar(200) NOT NULL,
        DocumentDescription nvarchar(500) NULL,
        DocumentUrl nvarchar(1000) NOT NULL,
        DisplayOrder int NOT NULL CONSTRAINT DF_ApplicationDocuments_DisplayOrder DEFAULT (0),
        IsActive bit NOT NULL CONSTRAINT DF_ApplicationDocuments_IsActive DEFAULT (1),
        CONSTRAINT FK_ApplicationDocuments_Applications FOREIGN KEY (ApplicationId) REFERENCES dbo.Applications(ApplicationId)
    );
END
GO

IF OBJECT_ID(N'dbo.DocumentAccessLog', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DocumentAccessLog
    (
        LogId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DocumentAccessLog PRIMARY KEY,
        UserName nvarchar(200) NOT NULL,
        ApplicationId int NOT NULL,
        DocumentId int NOT NULL,
        DocumentUrl nvarchar(max) NOT NULL,
        OpenedOn datetime NOT NULL CONSTRAINT DF_DocumentAccessLog_OpenedOn DEFAULT (GETDATE())
    );
END
GO