@echo off
echo --------------------------1.AdminWeb������ʼ--------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=1.ȷ������AmdinWeb��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\AdminWeb\App_Data\PublishProfiles\Report_AdminWeb_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 1.AdminWeb�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 1.AdminWeb������ȡ��
:ENDNY

endlocal
echo ---------------------------1.AdminWeb��������--------------------------
echo.

echo ---------------------------2.HttpApi������ʼ---------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=2.ȷ������HttpApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.HttpApi\App_Data\PublishProfiles\Report_HttpApi_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 2.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 2.HttpApi������ȡ��
:ENDNY

endlocal
echo ---------------------------2.HttpApi��������---------------------------
echo.

echo ------------------------3.AdminBusiness������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=3.ȷ������HttpApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\AdminBusiness\App_Data\PublishProfiles\Report_AdminBusiness_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 3.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 3.HttpApi������ȡ��
:ENDNY

endlocal
echo ------------------------3.AdminBusiness��������------------------------
echo.

echo ------------------------4.NotifyServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=4.ȷ������NotifyServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.Notify\App_Data\PublishProfiles\Report_Notify_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 4.NotifyServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 4.NotifyServer������ȡ��
:ENDNY

endlocal
echo ------------------------4.NotifyServer��������------------------------
echo.

echo ------------------------5.MediaServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=5.ȷ������MediaServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\MediaServerWeb\App_Data\PublishProfiles\Report_MediaServer_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 5.MediaServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 5.MediaServer������ȡ��
:ENDNY

endlocal
echo ------------------------5.MediaServer��������------------------------
echo.


echo ------------------------6.PushServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=6.ȷ������PushServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.PushServer\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.PushServer\App_Data\PublishProfiles\Report_PushServer_dev_ydan_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 6.PushServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 6.PushServer������ȡ��
:ENDNY

endlocal
echo ------------------------6.PushServer��������------------------------
echo.


echo ------------------------7.PayServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=7.ȷ������PayServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.Pay\App_Data\PublishProfiles\Report_Pay_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 7.PayServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 7.PayServer������ȡ��
:ENDNY

endlocal
echo ------------------------7.PayServer��������------------------------
echo.

echo ------------------------8.RestfulApi������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=8.ȷ������RestfulApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "E:\projects\dz\Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="E:\projects\dz\Dianzhu.Web.RestfulApi\Properties\PublishProfiles\Report_RestfulApi_dev_ydban_cn.pubxml" /p:visualstudioversion=14.0 /p:AllowUntrustedCertificate=True /p:username="deployer" /p:password="deployer2016"
echo 8.RestfulApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 8.RestfulApi������ȡ��
:ENDNY

endlocal
echo ------------------------8.RestfulApi��������------------------------
echo.

pause