@echo off
REM build.bat - For Windows builds

if not exist "%PREFIX%\bin" mkdir "%PREFIX%\bin"
if not exist "%PREFIX%\api" mkdir "%PREFIX%\api"
if not exist "%PREFIX%\dotnet" mkdir "%PREFIX%\dotnet"

copy bin\plugify-module-dotnet.dll "%PREFIX%\bin\" || exit 1
xcopy api "%PREFIX%\api" /E /Y /I || exit 1
xcopy dotnet "%PREFIX%\dotnet" /E /Y /I || exit 1
copy plugify-module-dotnet.pmodule "%PREFIX%\" || exit 1

REM Create activation scripts
if not exist "%PREFIX%\etc\conda\activate.d" mkdir "%PREFIX%\etc\conda\activate.d"
if not exist "%PREFIX%\etc\conda\deactivate.d" mkdir "%PREFIX%\etc\conda\deactivate.d"

echo @echo off > "%PREFIX%\etc\conda\activate.d\plugify-module-dotnet.bat"
echo set "PLUGIFY_NET_MODULE_PATH=%%CONDA_PREFIX%%;%%PLUGIFY_NET_MODULE_PATH%%" >> "%PREFIX%\etc\conda\activate.d\plugify-module-dotnet.bat"

echo @echo off > "%PREFIX%\etc\conda\deactivate.d\plugify-module-dotnet.bat"
echo set "PLUGIFY_NET_MODULE_PATH=%%PLUGIFY_NET_MODULE_PATH:%%CONDA_PREFIX%%;=%%" >> "%PREFIX%\etc\conda\deactivate.d\plugify-module-dotnet.bat"

exit 0
