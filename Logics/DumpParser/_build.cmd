@echo off
chcp 65001 >nul
call "C:\Program Files\Microsoft Visual Studio\18\Community\VC\Auxiliary\Build\vcvarsall.bat" x64 >nul 2>&1
cd /d "%~dp0"
cl /O2 /W3 /LD /MT /nologo /utf-8 /std:c11 /DWINDOWS /DWIN32 /DUTF8 /DODV_DLL_MODE /D_CRT_SECURE_NO_WARNINGS /I "." /Fe"OraDB_DumpParser.dll" odv_api.c odv_detect.c odv_expdp.c odv_exp.c odv_record.c odv_number.c odv_datetime.c odv_charset.c odv_xml.c odv_csv.c odv_sql.c /link /DLL
if %ERRORLEVEL% NEQ 0 (
    echo FAILED
    exit /b 1
)
echo OK
del /q *.obj *.exp *.lib 2>nul
