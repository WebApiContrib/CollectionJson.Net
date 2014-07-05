@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

REM NuGet Package Restore

call %nuget% install src\WebApiContrib.Formatting.CollectionJson.Client\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed
call %nuget% install src\WebApiContrib.Formatting.CollectionJson.Server\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed

call %nuget% install test\WebApiContrib.CollectionJson.Tests\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed
call %nuget% install test\WebApiContrib.Formatting.CollectionJson.Client.Tests\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed
call %nuget% install test\WebApiContrib.Formatting.CollectionJson.Server.Tests\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed

REM Compilation

call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.CollectionJson\WebApiContrib.CollectionJson.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.Formatting.CollectionJson.Client\WebApiContrib.Formatting.CollectionJson.Client.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.Formatting.CollectionJson.Server\WebApiContrib.Formatting.CollectionJson.Server.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild test\WebApiContrib.CollectionJson.Tests\WebApiContrib.CollectionJson.Tests.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild test\WebApiContrib.Formatting.CollectionJson.Client.Tests\WebApiContrib.Formatting.CollectionJson.Client.Tests.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild test\WebApiContrib.Formatting.CollectionJson.Server.Tests\WebApiContrib.Formatting.CollectionJson.Server.Tests.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

REM Test Execution

%GallioEcho% test\WebApiContrib.CollectionJson.Tests\bin\%config%\WebApiContrib.CollectionJson.Tests.dll
%GallioEcho% test\WebApiContrib.Formatting.CollectionJson.Client.Tests\bin\%config%\WebApiContrib.Formatting.CollectionJson.Client.Tests.dll
%GallioEcho% test\WebApiContrib.Formatting.CollectionJson.Server.Tests\bin\%config%\WebApiContrib.Formatting.CollectionJson.Server.Tests.dll

REM NuGet Package Creation

mkdir Build
call %nuget% pack "src\WebApiContrib.CollectionJson\WebApiContrib.CollectionJson.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
call %nuget% pack "src\WebApiContrib.Formatting.CollectionJson.Client\WebApiContrib.Formatting.CollectionJson.Client.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
call %nuget% pack "src\WebApiContrib.Formatting.CollectionJson.Server\WebApiContrib.Formatting.CollectionJson.Server.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"

pause
