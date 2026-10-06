# VDI Setup

## Target environment

- Visual Studio 2017
- .NET Framework 4.7.2 Developer Pack
- ASP.NET MVC 5
- IIS or IIS Express
- SQL Server and SSMS for SQL mode

## Build steps

1. Open [../ReportPortal.sln](../ReportPortal.sln) in Visual Studio 2017.
2. If Visual Studio shows `Project Target Framework Not Installed` for `.NETFramework,Version=v4.7.2`, select `Download the targeting pack` or ask the VDI admin to install the .NET Framework 4.7.2 Developer Pack / Targeting Pack. Do not retarget to .NET Framework 4.6.1 unless the project target is formally changed.
3. Restore NuGet packages from [../src/ReportPortal.Web/packages.config](../src/ReportPortal.Web/packages.config).
4. Confirm [../src/ReportPortal.Web/Web.config](../src/ReportPortal.Web/Web.config) has `UseMockData=true` for local testing without SQL Server.
5. Build the solution.
6. Run the site with IIS Express.
7. Sign in with `DOMAIN\Harsh.V` to see ORMS and CADCRM.

## .NET Framework 4.7.2 targeting pack

The project intentionally targets .NET Framework 4.7.2. Visual Studio needs the Developer Pack or Targeting Pack to compile it. The runtime alone is not enough for building in Visual Studio.

If the VDI has internet access, use the Visual Studio prompt to download the targeting pack. If the VDI is offline, download `NDP472-DevPack-ENU.exe` on an internet-connected machine, copy it to the VDI using the approved client process, install it, then close and reopen Visual Studio.

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