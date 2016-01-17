@ECHO OFF
SETLOCAL
SET NUGET=src\.nuget\nuget.exe
SET MSBUILD="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MsBuild.exe"

%MSBUILD% src\MayanDate\MayanDate.csproj /t:Clean,Build /p:Configuration="Release 4.0"
IF ERRORLEVEL 1 EXIT /b 1

%MSBUILD% src\MayanDate\MayanDate.csproj /t:Build /p:Configuration="Release 4.5"
IF ERRORLEVEL 1 EXIT /b 1

FOR %%G IN (packaging\nuget\*.nuspec) DO (
  %NUGET% pack %%G -Symbols -Output packaging\nuget
  IF ERRORLEVEL 1 EXIT /b 1
)
