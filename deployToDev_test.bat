:: Hide Command and Set Scope
@echo off
setlocal EnableExtensions

:: Customize Window
title My Menu

:: Menu Options
:: Specify as many as you want, but they must be sequential from 1 with no gaps
:: Step 1. List the Application Names
set "App[1]=One"
set "App[2]=Two"
set "App[3]=Three"
set "App[4]=Four"
set "App[5]=Five"
set "App[6]=All Apps"

:: Display the Menu
set "Message="
:Menu
cls
echo.%Message%
echo.
echo.  Menu Title
echo.
set "x=0"
:MenuLoop
set /a "x+=1"
if defined App[%x%] (
    call echo   %x%. %%App[%x%]%%
    goto MenuLoop
)
echo.

:: Prompt User for Choice
:Prompt
set "Input=1,2,3,4,5,6"
set /p "Input=Select what app(默认选择全部）:"

:: Validate Input [Remove Special Characters]
if not defined Input goto Prompt
set "Input=%Input:"=%"
set "Input=%Input:^=%"
set "Input=%Input:<=%"
set "Input=%Input:>=%"
set "Input=%Input:&=%"
set "Input=%Input:|=%"
set "Input=%Input:(=%"
set "Input=%Input:)=%"
:: Equals are not allowed in variable names
set "Input=%Input:^==%"
call :Validate %Input%

:: Process Input
call :Process %Input%
goto End


:Validate
set "Next=%2"
if not defined App[%1] (
    set "Message=Invalid Input: %1"
    goto Menu
)
if defined Next shift & goto Validate
goto :eof


:Process
set "Next=%2"
call set "App=%%App[%1]%%"

:: Run Installations
:: Specify all of the installations for each app.
:: Step 2. Match on the application names and perform the installation for each
if "%App%" EQU "One" (
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "D:\Code\dianzhu\src\AdminWeb\website.publishproj" /clp:ErrorsOnly /noconsolelogger /p:deployonbuild=true /p:publishprofile="D:\Code\dianzhu\src\AdminWeb\App_Data\PublishProfiles\localpc.pubxml" /p:visualstudioversion=14.0  
 echo 1
)

if "%App%" EQU "Two" (
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "D:\Code\dianzhu\src\AdminWeb\website.publishproj" /clp:ErrorsOnly /noconsolelogger /p:deployonbuild=true /p:publishprofile="D:\Code\dianzhu\src\AdminWeb\App_Data\PublishProfiles\localpc.pubxml" /p:visualstudioversion=14.0  
  echo 2
)

 

if "%App%" EQU "Three" 
(
:: build csclient and upload to server
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0Dianzhu.CSClient\Dianzhu.CSClient.csproj"  /target:publish
 echo 3
)

if "%App%" EQU "Four" echo Run Install for App Four here
if "%App%" EQU "Five" echo Run Install for App Five here
if "%App%" EQU "All Apps" (
    echo Run Install for All Apps here
)
 
:: Prevent the command from being processed twice if listed twice.
set "App[%1]="
if defined Next shift & goto Process
goto :eof


:End
endlocal
pause >nul