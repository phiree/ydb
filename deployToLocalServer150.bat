rem �Ľ��ƻ�: a)��ʾ��Ŀ�б�,ͨ�������Ӧ��ŷ�����Ŀ,����������. b)����������Ŀ.
@echo off
echo --------------------------1.AdminWeb������ʼ--------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=1.ȷ������AmdinWeb��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminWeb\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly
echo 1.AdminWeb�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 1.AdminWeb������ȡ��
:ENDNY

endlocal
echo -�������-
echo.

echo ---------------------------2.HttpApi������ʼ---------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=2.ȷ������HttpApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.HttpApi\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.HttpApi\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 2.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 2.HttpApi������ȡ��
:ENDNY

endlocal
echo -�������-
echo.

echo ------------------------3.AdminBusiness������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=3.ȷ������HttpApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0AdminBusiness\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0AdminBusiness\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 3.HttpApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 3.HttpApi������ȡ��
:ENDNY

endlocal
echo -�������-
echo.

echo ------------------------4.NotifyServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=4.ȷ������NotifyServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Notify\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Notify\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 4.NotifyServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 4.NotifyServer������ȡ��
:ENDNY

endlocal
echo -�������-
echo.

echo ------------------------5.MediaServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=5.ȷ������MediaServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0MediaServerWeb\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0MediaServerWeb\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 5.MediaServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 5.MediaServer������ȡ��
:ENDNY

endlocal
echo -�������-
echo.


echo ------------------------6.PushServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=6.ȷ������PushServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.PushServer\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.PushServer\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 6.PushServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo 6.PushServer������ȡ��
:ENDNY

endlocal
echo -�������-
echo.


echo ------------------------7.PayServer������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=7.ȷ������PayServer��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.Pay\website.publishproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.Pay\App_Data\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 7.PayServer�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  PayServer������ȡ��
:ENDNY

endlocal
echo -�������-
echo.

echo ------------------------8.RestfulApi������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=8.ȷ������RestfulApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.Web.RestfulApi\Dianzhu.Web.RestfulApi.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Dianzhu.Web.RestfulApi\Properties\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 8.RestfulApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  RestfulApi������ȡ��
:ENDNY

endlocal
echo  -�������-
echo.

echo ------------------------9.LogView������ʼ------------------------
setlocal
:PROMPT
SET /P AREYOUSURE=8.ȷ������RestfulApi��վ (Y/N)?

IF /I "%AREYOUSURE%" NEQ "Y" GOTO ENDY
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Ydb.Web.LogView\Ydb.Web.LogView.csproj" /p:deployonbuild=true /p:publishprofile="%~dp0Ydb.Web.LogView\Properties\PublishProfiles\150.pubxml" /p:visualstudioversion=14.0 /consoleloggerparameters:ErrorsOnly 
echo 8.RestfulApi�����ɹ�
:ENDY

IF /I "%AREYOUSURE%" EQU "Y" GOTO ENDNY
echo  RestfulApi������ȡ��
:ENDNY

endlocal
echo -�������-
echo.
pause