@echo off
SET FRAMEWORK_PATH=C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319
SET PATH=%PATH%;%FRAMEWORK_PATH%;

if exist output ( rmdir /s /q output )
mkdir output

echo Compiling
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release /t:Clean
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release

echo Copying
copy .\src\proj\NServiceBus.MultiTenant\bin\Release\NServiceBus.MultiTenant.* output

echo Cleaning
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release /t:Clean

echo Done