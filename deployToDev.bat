@echo off
echo --------------------------1.AdminWeb发布开始--------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=1.确定发布AmdinWeb网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 1.AdminWeb发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 1.AdminWeb发布已取消
:ENDNY

endlocal
echo ---------------------------1.AdminWeb发布结束--------------------------
echo.

echo ---------------------------2.HttpApi发布开始---------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=2.确定发布HttpApi网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.HttpApi\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 2.HttpApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 2.HttpApi发布已取消
:ENDNY

endlocal
echo ---------------------------2.HttpApi发布结束---------------------------
echo.

echo ------------------------3.AdminBusiness发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=3.确定发布HttpApi网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminBusiness\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 3.HttpApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 3.HttpApi发布已取消
:ENDNY

endlocal
echo ------------------------3.AdminBusiness发布结束------------------------
echo.

echo ------------------------4.NotifyServer发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=4.确定发布NotifyServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Notify\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 4.NotifyServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 4.NotifyServer发布已取消
:ENDNY

endlocal
echo ------------------------4.NotifyServer发布结束------------------------
echo.

echo ------------------------5.MediaServer发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=5.确定发布MediaServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0MediaServerWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 5.MediaServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 5.MediaServer发布已取消
:ENDNY

endlocal
echo ------------------------5.MediaServer发布结束------------------------
echo.


echo ------------------------6.PushServer发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=6.确定发布PushServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.PushServer\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.PushServer\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 6.PushServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 6.PushServer发布已取消
:ENDNY

endlocal
echo ------------------------6.PushServer发布结束------------------------
echo.


echo ------------------------7.PayServer发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=7.确定发布PayServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Pay\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer发布已取消
:ENDNY

endlocal
echo ------------------------7.PayServer发布结束------------------------
echo.

echo ------------------------8.RestfulApi发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=8.确定发布RestfulApi网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.RestfulApi\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 8.RestfulApi发布已取消
:ENDNY

endlocal
echo ------------------------8.RestfulApi发布结束------------------------
echo.


echo ------------------------9.LogView发布开始------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=9.LogView发布开始 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Ydb.Web.LogView\Ydb.Web.LogView.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Ydb.Web.LogView\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  LogView发布取消
:ENDNY

endlocal
echo -发布完成-


pause