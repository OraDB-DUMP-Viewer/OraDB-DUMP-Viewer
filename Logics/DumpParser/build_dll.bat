@echo off
REM OraDB DUMP Viewer - DLL Build Script
REM Builds OraDB_DumpParser.dll using MSVC

set VCVARS=
for %%e in (Community Professional Enterprise) do (
    for %%v in (18 2022 2025 2027) do (
        if exist "C:\Program Files\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvarsall.bat" (
            set "VCVARS=C:\Program Files\Microsoft Visual Studio\%%v\%%e\VC\Auxiliary\Build\vcvarsall.bat"
        )
    )
)
if "%VCVARS%"=="" (
    echo ERROR: Visual Studio not found
    exit /b 1
)
echo Using: %VCVARS%
call "%VCVARS%" x64

set DEFS=/DWINDOWS /DWIN32 /DUTF8 /DODV_DLL_MODE /D_CRT_SECURE_NO_WARNINGS
set CFLAGS=/O2 /W3 /LD /MT /nologo /utf-8 /std:c11
set SRCS=odv_api.c odv_detect.c odv_expdp.c odv_exp.c odv_record.c odv_number.c odv_datetime.c odv_charset.c odv_xml.c odv_csv.c odv_sql.c
set OUTDIR=..\..\bin\Debug\net10.0-windows7.0

if not exist "%OUTDIR%" mkdir "%OUTDIR%"

echo Building OraDB_DumpParser.dll ...
cl %CFLAGS% %DEFS% /I "." /Fe"%OUTDIR%\OraDB_DumpParser.dll" %SRCS% /link /DLL

if %ERRORLEVEL% NEQ 0 (
    echo BUILD FAILED
    exit /b 1
)

echo BUILD SUCCEEDED
echo Output: %OUTDIR%\OraDB_DumpParser.dll

REM Cleanup intermediate files
del /q *.obj *.exp *.lib 2>nul
