@ECHO OFF
SET BLD=%1
SET REV=%2
SET SHA=%3
IF [%BLD%]==[] SET BLD=0
IF [%REV%]==[] SET REV=0
IF [%SHA%]==[] SET SHA=DNN
ECHO Version %BLD%.%REV%-%SHA%
REM Место расположения утилит сборки
SET MSBUILD1="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
SET MSBUILD2="C:\Program Files (x86)\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
SET MSBUILD3="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
SET NUGET="C:\NuGet\nuget.exe"
REM Настраиваемые параметры конфигурации сборки
REM Debug - тестовая сборка 
REM Release - продуктовая сборка 
SET CONFIGURATION=Debug
SET SITE="Default Web Site"
REM 
SET OPTIONS0=/p:Configuration=%CONFIGURATION% /p:BuildVersion=%BLD% /p:RevisionNumber=%REV% /p:RevisionId=%SHA% /verbosity:minimal
SET OPTIONS1=%OPTIONS0% /p:DeployOnBuild=True /p:CreatePackageOnPublish=True /p:WebPublishMethod=Package /p:LastUsedBuildConfiguration=%CONFIGURATION% /p:PackageAsSingleFile=True
REM См. также https://www.codeproject.com/Articles/1184858/Deploying-a-Web-App-from-a-Command-Line-using-MSBu
REM Динамическое определение места установленного компилятора
IF EXIST %MSBUILD1% (
  SET MSBUILD=%MSBUILD1%
) ELSE IF EXIST %MSBUILD2% (
  SET MSBUILD=%MSBUILD2%
) ELSE IF EXIST %MSBUILD3% (
  SET MSBUILD=%MSBUILD3%
) ELSE GOTO :ERROR1
IF NOT EXIST %NUGET% GOTO :ERROR2
ECHO nuget restore
FOR /R %%S IN (*.SLN) DO %NUGET% restore "%%S" -NonInteractive -Verbosity quiet 
FOR /R %%S IN (*.SLN) DO %MSBUILD% %%S %OPTIONS0%
ECHO Build complete
GOTO :DONE

:ERROR1                     
ECHO ! Microsoft Build does not found !
GOTO :DONE

:ERROR2
ECHO ! NuGet does not found !
GOTO :DONE

:DONE
REM Последняя строка сценария


