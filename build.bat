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

call %nuget% install src\WebApiContrib.Formatting.CollectionJson.Client\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed
call %nuget% install src\WebApiContrib.Formatting.CollectionJson.Server\packages.config -OutputDirectory %cd%\src\packages -NonInteractive -NoCache -Verbosity Detailed

call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.CollectionJson\WebApiContrib.CollectionJson.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.Formatting.CollectionJson.Client\WebApiContrib.Formatting.CollectionJson.Client.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
call %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\WebApiContrib.Formatting.CollectionJson.Server\WebApiContrib.Formatting.CollectionJson.Server.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
call %nuget% pack "src\WebApiContrib.CollectionJson\WebApiContrib.CollectionJson.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
call %nuget% pack "src\WebApiContrib.Formatting.CollectionJson.Client\WebApiContrib.Formatting.CollectionJson.Client.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
call %nuget% pack "src\WebApiContrib.Formatting.CollectionJson.Server\WebApiContrib.Formatting.CollectionJson.Server.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"

pause