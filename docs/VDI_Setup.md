# VDI Setup

## Target environment

- Visual Studio 2017
- .NET Framework 4.6.1 targeting support
- ASP.NET MVC 5
- IIS or IIS Express
- SQL Server and SSMS for SQL mode

## Build steps

1. Open [../ReportPortal.sln](../ReportPortal.sln) in Visual Studio 2017.
2. If Visual Studio asks to retarget the project, keep/select .NET Framework 4.6.1.
3. Restore NuGet packages from [../src/ReportPortal.Web/packages.config](../src/ReportPortal.Web/packages.config).
4. Confirm [../src/ReportPortal.Web/Web.config](../src/ReportPortal.Web/Web.config) has `UseMockData=true` for local testing without SQL Server.
5. Build the solution.
6. Run the site with IIS Express.
7. Sign in with `DOMAIN\Harsh.V` to see ORMS and CADCRM.

## .NET Framework target

The project targets .NET Framework 4.6.1 to match locked-down client VDI machines where installing additional targeting packs is not possible. Do not manually change the target inside the VDI unless the required targeting pack is installed by IT.

## Offline NuGet restore

If the VDI cannot reach NuGet:

1. Restore on a machine with internet access.
2. Copy the `packages` folder beside `ReportPortal.sln`, if policy allows.
3. In Visual Studio 2017, add the copied folder as a local NuGet source.
4. Restore packages again from the local source.

Required packages are MVC 5 packages only: `Microsoft.AspNet.Mvc`, `Microsoft.AspNet.Razor`, `Microsoft.AspNet.WebPages`, and `Microsoft.Web.Infrastructure`.

## Switch to SQL Server

1. Run the SQL scripts in [../database](../database) in numeric order.
2. Update `ReportPortalConnection` in [../src/ReportPortal.Web/Web.config](../src/ReportPortal.Web/Web.config).
3. Set `UseMockData=false`.
4. Update `ReportServerBaseUrl`.
5. Build and run again.