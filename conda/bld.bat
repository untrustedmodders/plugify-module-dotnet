@echo off
REM bld.bat - For Windows builds

REM Create the target directories
if not exist "%PREFIX%\bin" mkdir "%PREFIX%\bin"
if not exist "%PREFIX%\api" mkdir "%PREFIX%\api"
if not exist "%PREFIX%\dotnet" mkdir "%PREFIX%\dotnet"
if not exist "%PREFIX%" mkdir "%PREFIX%"

REM Copy the DLL and module file
copy bin\plugify-module-dotnet.dll "%PREFIX%\bin\" || exit 1
xcopy api "%PREFIX%\api" /E /Y /I
xcopy dotnet "%PREFIX%\dotnet" /E /Y /I
copy plugify-module-dotnet.pmodule "%PREFIX%\" || exit 1

REM Create activation scripts
if not exist "%PREFIX%\etc\conda\activate.d" mkdir "%PREFIX%\etc\conda\activate.d"
if not exist "%PREFIX%\etc\conda\deactivate.d" mkdir "%PREFIX%\etc\conda\deactivate.d"

REM Create activation script
echo @echo off > "%PREFIX%\etc\conda\activate.d\plugify-module-dotnet.bat"
echo set "PLUGIFY_NET_MODULE_PATH=%%CONDA_PREFIX%%;%%PLUGIFY_NET_MODULE_PATH%%" >> "%PREFIX%\etc\conda\activate.d\plugify-module-dotnet.bat"

REM Create deactivation script  
echo @echo off > "%PREFIX%\etc\conda\deactivate.d\plugify-module-dotnet.bat"
echo set "PLUGIFY_NET_MODULE_PATH=%%PLUGIFY_NET_MODULE_PATH:%%CONDA_PREFIX%%;=%%" >> "%PREFIX%\etc\conda\deactivate.d\plugify-module-dotnet.bat"

exit 0
