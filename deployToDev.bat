@echo off
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:1. AmdinWeb��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 1.AdminWeb�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 1.AdminWeb������ȡ��
:ENDNY

endlocal
 
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:2. HttpApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.HttpApi\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 2.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 2.HttpApi������ȡ��
:ENDNY

endlocal


setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:3. AdminBusiness��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminBusiness\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 3.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 3.HttpApi������ȡ��
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:4. NotifyServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Notify\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 4.NotifyServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 4.NotifyServer������ȡ��
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:5. MediaServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0MediaServerWeb\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 5.MediaServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 5.MediaServer������ȡ��
:ENDNY

endlocal
 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:6. AdminAgent��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminAgent\AdminAgent.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminAgent\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer������ȡ��
:ENDNY

endlocal
 
 
 


 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:7 PayServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Pay\App_Data\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer������ȡ��
:ENDNY

endlocal
 

 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:RestfulApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.RestfulApi\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 8.RestfulApi������ȡ��
:ENDNY

endlocal
 


 
setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:9.LogView  (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Ydb.Web.LogView\Ydb.Web.LogView.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Ydb.Web.LogView\Properties\PublishProfiles\dev.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  LogView����ȡ��
:ENDNY

endlocal


setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:10.CSClient(ȷ���Ƿ��Ѿ�����ȷ�ı���汾�������˱���)(Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
ncftpput -R   -u yf     -P 2121 dev.ydban.cn  /publish/csclient/   E:\Projects\dianzhu\publish\csclient\deploy\*
 
echo 10.CSClient�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  10.CSClient����ȡ��
:ENDNY

endlocal
 

setlocal
:PROMPT
SET /P AREYOUSURE=------------------------�Ƿ񷢲�:11.DianDian(ȷ���Ƿ��Ѿ�������ȷ)(Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
::ʹ��dev���ñ�����
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0DianzhuService.Diandian\DianzhuService.Diandian.csproj" /property:Configuration=dev
echo 11.DianDian����ɹ�

ncftpput -R   -u yf     -P 2121 dev.ydban.cn  /publish/diandian/   E:\Projects\dianzhu\publish\diandian\dev\*
 
echo 11.DianDian�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  11.DianDian����ȡ��
:ENDNY
endlocal
 



pause