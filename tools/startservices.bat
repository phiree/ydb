Rem �ű�����: ��DidanDian����,��֪ͨ��վ
rem ʹ��ǰ��: ��appcmd.exe����·������path.

net start diandian
rem appcmd stop apppool /apppool.name:8039_dianzhu_notify
rem appcmd stop apppool /apppool.name:8039_dianzhu_notify
rem appcmd start apppool /apppool.name:8039_dianzhu_notify

rem appcmd stop site /site.name:8039_dianzhu_notify
rem appcmd start site /site.name:8039_dianzhu_notify
start http://localhost:8039
pause