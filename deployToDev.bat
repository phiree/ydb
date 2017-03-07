@echo off
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:1. AmdinWeb网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 1.AdminWeb发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 1.AdminWeb发布已取消
:ENDNY

endlocal
 
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:2. HttpApi网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.HttpApi\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 2.HttpApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 2.HttpApi发布已取消
:ENDNY

endlocal


setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:3. AdminBusiness网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminBusiness\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 3.HttpApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 3.HttpApi发布已取消
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:4. NotifyServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Notify\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 4.NotifyServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 4.NotifyServer发布已取消
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:5. MediaServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0MediaServerWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 5.MediaServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 5.MediaServer发布已取消
:ENDNY

endlocal
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:6. AdminAgent网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminAgent\AdminAgent.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminAgent\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer发布已取消
:ENDNY

endlocal
 
 
 


 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:7 PayServer网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Pay\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer发布已取消
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:RestfulApi网站 (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.RestfulApi\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 8.RestfulApi发布已取消
:ENDNY

endlocal
 


 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:9.LogView  (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Ydb.Web.LogView\Ydb.Web.LogView.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Ydb.Web.LogView\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  LogView发布取消
:ENDNY

endlocal


setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:10.CSClient(确认是否已经将正确的编译版本发布到了本地)(Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
ncftpput -R   -u yf     -P 2121 dev.ydban.cn  /publish/csclient/   E:\Projects\dianzhu\publish\csclient\deploy\*
 
echo 10.CSClient发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  10.CSClient发布取消
:ENDNY

endlocal
 

setlocal
:PROMPT
SET /P AREYOUSURE=------------------------是否发布:11.DianDian(确认是否已经编译正确)(Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
::使用dev配置编译点点
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0DianzhuService.Diandian\DianzhuService.Diandian.csproj" /property:Configuration=dev
echo 11.DianDian编译成功

ncftpput -R   -u yf     -P 2121 dev.ydban.cn  /publish/diandian/   E:\Projects\dianzhu\publish\diandian\dev\*
 
echo 11.DianDian发布成功
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  11.DianDian发布取消
:ENDNY
endlocal
 



pause