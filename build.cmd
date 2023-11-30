@rem Copyright (c) Joseph W Donahue and Sharper Hacks LLC
@rem Licensed under the terms of the MIT license (https://opensource.org/licenses/MIT).
@rem Contact: coders@sharperhacks.com

@setlocal EnableExtensions EnableDelayedExpansion
@set prompt=$_$G

@if /i "%1" equ "/?" (@goto ShowHelp)
@if /i "%1" equ "-?" (@goto ShowHelp)
@if /i "%1" equ "-h" (@goto ShowHelp)
@if /i "%1" equ "/h" (@goto ShowHelp)
@if /i "%1" equ "--help" (@goto ShowHelp)

@set _buildType=Debug
@set _solution=StringExtensions.sln
@set _buildVerbosity=minimal
@set _testVerbosity=minimal
@call :SetVersionVariables

@pushd %~dp0

@call :ProcessArgs %*

@if defined _exit (@goto :CleanupAndExit)

@call :DotNet restore
@if %ErrorLevel% neq 0 (@call :HandleError restore %ErrorLevel% & goto :CleanupAndExit)

@call :DotNet msbuild -p:Configuration=%_buildType% -p:TreatWarningsAsErrors="true" -verbosity:%_buildVerbosity%
@if %ErrorLevel% neq 0 (@call :HandleError msbuild %ErrorLevel% & goto :CleanupAndExit)

@if "%_skipTest%" equ "1" goto :CleanupAndExit
@call :DotNet test --no-restore --no-build --configuration %_buildType% --verbosity %_testVerbosity%
@if %ErrorLevel% neq 0 (@call :HandleError test %ErrorLevel% & goto :CleanupAndExit)

@if "%_skipPac%" equ "1" goto :CleanupAndExit
@call :DotNet pack --no-restore --no-build --include-symbols --include-source --configuration %_buildType% /p:PackageVersion="%_versionPrefix%%_versionSuffix%"
@if %ErrorLevel% neq 0 (@call :HandleError pack %ErrorLevel%)

@goto :CleanupAndExit

:CleanAll
    @shift
    @REM @call :RemoveDirectory .BuildOutput
    @call :RemoveDirectory .Logs\CheckInTest
    @for /f %%I in ('dir /b /s *.csproj') do @call :RemoveDirectory "%%~dpI\bin" && @call :RemoveDirectory "%%~dpI\obj"
    @exit /b

:CleanupAndExit
    @popd
    @exit /b

:DotNet
    DotNet %* "%_solution%"
    @exit /b

:HandleError
    @echo DotNet %1 failed with error code: %2
    @exit /b %2

:HandleVariable
@if "" equ "%~2" set _validArg=0 & exit /b 1
@set "%%I=%%J"
@set _validArg=
@echo "%%I=%%J" + %%K
@exit /b 0

:ProcessArgs
    @set _validArg=
    @if /i "%~1" equ "Debug" (@set _validArg=1 & @set _buildType=Debug)

    @if /i "%~1" equ "clean" (
        @set _validArg=1
        @if /i "%~2" equ "Release" (set _buildType=Release)
        @call :DotNet clean --configuration !_buildType!
        @if !ErrorLevel! neq 0 (
            @call :HandleError clean !ErrorLevel!
            @goto :SetErrorCodeAndExit !ErrorLevel!
        )
    )
    @if /i "%~1" equ "clean-all" (
        @set _validArg=1
        @call :CleanAll
        @if "%~2" equ "" (set _exit=1 & exit /b 0)
    )
    @if /i "%~1" equ "clean-only" (
        @set _validArg=1
        @call :CleanAll
        set _exit=1
        exit /b 0
    )

    @if /i "%~1" equ "Release" (@set _validArg=1 & @set "_buildType=Release")
    @if /i "%~1" equ "NoPack" (@set _validArg=1 & @set "_skipPac=1")
    @if /i "%~1" equ "NoTest" (@set _validArg=1) & @set "_skipTest=1")

    @for /f "tokens=1,2 delims==" %%I in ("%~1") do @call :HandleVariable %%I %%J

    @if /i "%~1" equ "buildVerbosity" (@set _validArg=1 & @set _buildVerbosity=%2 & @shift & goto ProcessArgs)
    @if /i "%~1" equ "testVerbosity" (@set _validArg=1 & @set _testVerbosity=%2 & @shift & goto ProcessArgs)
    
    @if /i "%~1" equ "versionPrefix" (@set _validArg=1 & @set _versionPrefix=%2 & @shift & goto ProcessArgs)
    @if /i "%~1" equ "versionSuffix" (@set _validArg=1 & @set _versionSuffix=%2 & @shift & goto ProcessArgs)

    @if not defined _validArg (
        @echo Invalid argument found on command line: %1
        @set _exit=1
        @exit /b 160
    )

    @shift

    @if "" equ "%1" (@exit /b)
    @goto :ProcessArgs

:RemoveDirectory
    @echo Removing %1
    @rd /s /q %1 > NUL 2>&1

:SetErrorCodeAndExit
    @exit /b %1

:SetVersionVariables
    @if not defined _versionPrefix set _versionPrefix=0.0.0
    @if not defined _versionSuffix set _versionSuffix=-a.dev.%USERNAME%
    @exit /b 0

:ShowHelp
    @type %~dpn0.cmd.help.txt
    @exit /b 0
