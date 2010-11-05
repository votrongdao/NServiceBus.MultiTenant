@echo off
SET FRAMEWORK_PATH=C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319
SET PATH=%PATH%;%FRAMEWORK_PATH%;

echo Compiling
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release /t:Clean
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release /property:TargetFrameworkVersion=v3.5

echo Copying
copy .\src\proj\NServiceBus.MultiTenant\bin\Release\NServiceBus.MultiTenant.* output

echo Cleaning
msbuild /nologo /verbosity:quiet src/NServiceBus.MultiTenant.sln /p:Configuration=Release /t:Clean

echo Done