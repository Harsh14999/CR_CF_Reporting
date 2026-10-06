USE [ReportPortalDb];
GO

DECLARE @UserName nvarchar(200) = N'DOMAIN\Harsh.V';

SELECT a.ApplicationCode, a.ApplicationName, a.Description
FROM dbo.Applications a
INNER JOIN dbo.UserApplicationAccess access ON access.ApplicationId = a.ApplicationId
WHERE access.UserName = @UserName AND access.IsActive = 1 AND a.IsActive = 1
ORDER BY a.DisplayOrder;

SELECT a.ApplicationCode, r.ReportName, r.ReportServerPath
FROM dbo.Reports r
INNER JOIN dbo.Applications a ON a.ApplicationId = r.ApplicationId
WHERE a.ApplicationCode IN (N'ORMS', N'CADCRM') AND r.IsActive = 1
ORDER BY a.DisplayOrder, r.DisplayOrder;

SELECT a.ApplicationCode, d.DocumentName, d.DocumentUrl
FROM dbo.ApplicationDocuments d
INNER JOIN dbo.Applications a ON a.ApplicationId = d.ApplicationId
WHERE a.ApplicationCode = N'CADCRM' AND d.IsActive = 1
ORDER BY d.DisplayOrder;

SELECT TOP 50 * FROM dbo.ReportExecutionLog ORDER BY ExecutedOn DESC;
SELECT TOP 50 * FROM dbo.DocumentAccessLog ORDER BY OpenedOn DESC;
GO