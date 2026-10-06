# DIB Enterprise Report Portal

Internal ASP.NET MVC 5 report portal for existing Power BI Report Builder, SSRS, and Power BI Report Server RDL reports. The solution targets Visual Studio 2017, .NET Framework 4.6.1, IIS, SQL Server, Razor views, ADO.NET, and `packages.config` NuGet restore.

## Project layout

- `ReportPortal.sln` - Visual Studio 2017 solution.
- `src/ReportPortal.Web` - ASP.NET MVC 5 web application.
- `database` - SQL Server scripts to create and seed `ReportPortalDb`.
- `docs/VDI_Setup.md` - VDI build and offline NuGet notes.
- `docs/IIS_Deployment.md` - IIS publish and deployment notes.

## Run with mock data

Mock mode is enabled by default in [src/ReportPortal.Web/Web.config](src/ReportPortal.Web/Web.config):

```xml
<add key="UseMockData" value="true" />
```

Open [ReportPortal.sln](ReportPortal.sln) in Visual Studio 2017, restore NuGet packages, build, and run. Sign in with:

```text
DOMAIN\Harsh.V
```

Any non-empty Windows user ID can sign in, but only `DOMAIN\Harsh.V` has seeded mock access to ORMS and CADCRM.

The project targets .NET Framework 4.6.1 to match the locked-down client VDI. If Visual Studio prompts about retargeting, keep/select .NET Framework 4.6.1.

## Create the database in SSMS

On VDI:

1. Open SSMS.
2. Connect to SQL Server.
3. Run [database/001_CreateDatabase.sql](database/001_CreateDatabase.sql).
4. Run [database/002_CreateTables.sql](database/002_CreateTables.sql).
5. Run [database/003_Seed_InitialData.sql](database/003_Seed_InitialData.sql).
6. Run [database/004_CreateIndexes.sql](database/004_CreateIndexes.sql).
7. Run [database/005_TestQueries.sql](database/005_TestQueries.sql).
8. Update the `ReportPortalConnection` connection string in [src/ReportPortal.Web/Web.config](src/ReportPortal.Web/Web.config).
9. Set `UseMockData` to `false`.
10. Update `ReportServerBaseUrl`.
11. Open the solution in Visual Studio 2017.
12. Build and run.

## Configuration

Update these values in [src/ReportPortal.Web/Web.config](src/ReportPortal.Web/Web.config):

```xml
<add name="ReportPortalConnection" connectionString="Data Source=YOUR_SQL_SERVER;Initial Catalog=ReportPortalDb;Integrated Security=True" providerName="System.Data.SqlClient" />
<add key="ReportServerBaseUrl" value="http://your-report-server/ReportServer" />
<add key="DefaultOpenMode" value="NewTab" />
<add key="UseMockData" value="true" />
```

Set `UseMockData=false` to use SQL Server via ADO.NET repositories.

## Offline NuGet restore in VDI

Use standard MVC 5 packages only. If the VDI has no internet access:

1. Restore packages on a machine with internet access if possible.
2. Copy the restored `packages` folder with the solution if client policy allows.
3. Configure a local NuGet package source in Visual Studio 2017 if needed.

The required package list is in [src/ReportPortal.Web/packages.config](src/ReportPortal.Web/packages.config).

## Add a new application

Insert a row into `Applications` with a unique `ApplicationCode`, then grant users access through `UserApplicationAccess`. No controller or view changes are required.

## Add a new report

Insert a row into `Reports` for the application. `ReportServerPath` should be the report server path, for example `/ORMS/RptKRI`. The portal builds the launch URL as:

```text
{ReportServerBaseUrl}?{ReportServerPath}&rs:Command=Render
```

Report parameter support is available in `ReportUrlBuilder`; MVP launch sends no custom parameters.

## Add static documents

Insert rows into `ApplicationDocuments`. The dashboard shows `Open static document` for applications that have active documents.

Replace placeholder CADCRM URLs in `ApplicationDocuments.DocumentUrl` before production use:

```text
http://your-document-server/CADCRM/CADCRMPolicy.pdf
http://your-document-server/CADCRM/CADCRMUserGuide.pdf
```

## Grant user access

Insert rows into `UserApplicationAccess` using the Windows user ID entered on the login page, for example `DOMAIN\Harsh.V`. Application-level access is enough for MVP.

`UserReportAccess` exists for future report-level rules. If no row exists for a report, application access is enough. If rows exist, `CanView=1` grants report access.

## Logging

Report launches are inserted into `ReportExecutionLog` with user name, application ID, report ID, generated URL, optional parameter JSON, and execution time.

Document opens are inserted into `DocumentAccessLog` with user name, application ID, document ID, URL, and open time.

## Deploy to IIS

See [docs/IIS_Deployment.md](docs/IIS_Deployment.md) for publish and IIS setup details.