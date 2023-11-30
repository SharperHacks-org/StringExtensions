@setlocal EnableExtensions
@set prompt=$G

@set _logDir=.Logs\CheckInTest

@if not exist "%_logDir%" (md %_logDir%)

@call :CopyrightChecks "%_logDir%\copyrightchecks.log.txt"
@if %ERRORLEVEL% neq 0 (exit /b)

@call :BuildIt clean-all "%_logDir%\clean.log.txt"
@call :BuildIt release "%_logDir%\releaseBuild.log.txt"
@call :BuildIt debug "%_logDir%\debugBuild.log.txt"

@exit /b

:CopyrightChecks
call CopyrightChecks > %1 2>&1
@if %ERRORLEVEL% neq 0 (@echo Copyright checks failed. See %1 for details.) else (@echo Succeeded)
@exit /b

:BuildIt
call build %1 > %2 2>&1
@if %ERRORLEVEL% neq 0 (@call :HandleError %1 %2) else (@echo Succeeded)
@exit /b

:HandleError
@echo build %1 Failed. See %2 for details.
@exit /b




