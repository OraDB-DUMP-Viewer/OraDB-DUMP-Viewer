@echo off
REM Generates odv_version_gen.h from .vbproj Version tag
REM Called by build_dll.bat and VS pre-build event

set "VBPROJ=%~dp0..\..\OraDB DUMP Viewer.vbproj"
set APP_VER=0.0.0

if exist "%VBPROJ%" (
    for /f "tokens=2 delims=<>" %%v in ('findstr "<Version>" "%VBPROJ%"') do set APP_VER=%%v
)

(echo /* Auto-generated from .vbproj - do not edit */
echo #define ODV_VERSION_STRING "%APP_VER%") > "%~dp0odv_version_gen.h"

echo Version: %APP_VER%
