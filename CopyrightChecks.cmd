@rem Copyright Joseph W Donahue and Sharper Hacks LLC (US-WA)
@rem
@rem Licensed under the Apache License, Version 2.0 (the "License");
@rem you may not use this file except in compliance with the License.
@rem You may obtain a copy of the License at
@rem
@rem   http://www.apache.org/licenses/LICENSE-2.0
@rem
@rem Unless required by applicable law or agreed to in writing, software
@rem distributed under the License is distributed on an "AS IS" BASIS,
@rem WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
@rem See the License for the specific language governing permissions and
@rem limitations under the License.
@rem
@rem SharperHacks is a trademark of Sharper Hacks LLC (US-Wa), and may not be
@rem applied to distributions of derivative works, without the express written
@rem permission of a registered officer of Sharper Hacks LLC (US-WA).

@setlocal EnableExtensions
@set prompt=$G

@call :SetFQDP _outputDir=%~dp0\.BuildOutput
@call :SetFQDP _gitDir=%~dp0\.git
@call :SetFQDP _logDir=%~dp0\.Logs

@set _crl1="// Copyright Joseph W Donahue and Sharper Hacks LLC"
@set _crl2="// Licensed under the Apache License, Version 2.0"

@set _finalExitCode=0

@for /f %%I in ('dir /b /s *.csproj') do @call :CheckSources "%%~dpI"

@exit /b %_finalExitCode%

:CheckSources
@call :SetFQDP _currentDir=%~1
@rem @echo Running copyright checks in: %_currentDir%
@for %%A in ("%_currentDir%*.cs") do @Call :CheckFile "%%A"
@exit /b

:CheckFile
@rem @echo Checking file: %1
@findstr /nip /c:%_crl1% %1 > NUL 2>>&1
@if errorlevel 1 @call :ReportMissingCopyRight %1

@findstr /nip /c:%_crl2% %1 > NUL 2>>&1
@if errorlevel 1 @call :ReportMissingLicense %1

@exit /b

:ReportMissingCopyRight
@if /i "%~nx1" equ "assemblyinfo.cs" @exit /b
@if /i "%~nx1" equ "assemblyattributes.cs" @exit /b
@if /i "%~nx1" equ "globalsuppressions.cs" @exit /b
@if /i "%~nx1" equ "usings.cs" @exit /b
@if /i "%~nx1" equ "globalusings.cs" @exit /b
@set _finalExitCode=1
@echo Error: Missing copyright notice in file: %~1
@exit /b

:ReportMissingLicense
@if /i "%~nx1" equ "assemblyinfo.cs" @exit /b
@if /i "%~nx1" equ "assemblyattributes.cs" @exit /b
@if /i "%~nx1" equ "globalsuppressions.cs" @exit /b
@if /i "%~nx1" equ "usings.cs" @exit /b
@if /i "%~nx1" equ "globalusings.cs" @exit /b
@set _finalExitCode=1
@echo Error: Missing license in file: %~1
@exit /b

:SetFQDP
@set %1=%~f2
@exit /b %_ERROR_SUCCESS_%


