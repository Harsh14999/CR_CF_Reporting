# IIS Deployment

## Prerequisites

- IIS with ASP.NET 4.x enabled.
- .NET Framework 4.7.2 installed on the server.
- SQL Server database created with the scripts in [../database](../database).
- A Windows identity or app pool identity that can read the site files and connect to SQL Server.

## Publish from Visual Studio 2017

1. Open [../ReportPortal.sln](../ReportPortal.sln).
2. Right-click `ReportPortal.Web` and choose Publish.
3. Use Folder publish for a controlled deployment package.
4. Copy the published files to the IIS site folder.
5. Update `Web.config` on the server:
   - Set `UseMockData=false`.
   - Set `ReportPortalConnection` to the production SQL Server.
   - Set `ReportServerBaseUrl` to the target Report Server URL.
6. In IIS, create or update the site/application to point to the published folder.
7. Use an application pool targeting .NET CLR v4.0 with Integrated pipeline mode.
8. Browse the site and sign in with an authorized Windows user ID.

## Operational checks

- The dashboard should show only applications granted in `UserApplicationAccess`.
- Opening a report should redirect to the configured report server and insert a row into `ReportExecutionLog`.
- Opening a static document should redirect to the configured URL and insert a row into `DocumentAccessLog`.
- If a user has no assigned applications, the dashboard should display a friendly empty message.