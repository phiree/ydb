@echo off
echo --------------------------1.AdminWeb发布开始--------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=1.确定发布AmdinWeb网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\AdminWeb\App_Data\PublishProfiles\Report_AdminWeb_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.HttpApi\App_Data\PublishProfiles\Report_HttpApi_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\AdminBusiness\App_Data\PublishProfiles\Report_AdminBusiness_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.Notify\App_Data\PublishProfiles\Report_Notify_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\MediaServerWeb\App_Data\PublishProfiles\Report_MediaServer_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.PushServer\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.PushServer\App_Data\PublishProfiles\Report_PushServer_dev_ydan_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.Pay\App_Data\PublishProfiles\Report_Pay_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
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
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.RestfulApi\Properties\PublishProfiles\Report_RestfulApi_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 8.RestfulApi发布已取消
:ENDNY

endlocal
echo ------------------------8.RestfulApi发布结束------------------------
echo.

pause