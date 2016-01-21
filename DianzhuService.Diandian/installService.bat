c:

net stop Diandian
sc delete Diandian
c:
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
installUtil.exe E:\projects\output\diandian\DianzhuService.Diandian.exe
net start Diandian
pause

