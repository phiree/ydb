Rem 脚本功能: 打开DidanDian服务,打开通知网站
rem 使用前提: 将appcmd.exe所在路径加入path.

net start diandian
rem appcmd stop apppool /apppool.name:8039_dianzhu_notify
rem appcmd stop apppool /apppool.name:8039_dianzhu_notify
rem appcmd start apppool /apppool.name:8039_dianzhu_notify

rem appcmd stop site /site.name:8039_dianzhu_notify
rem appcmd start site /site.name:8039_dianzhu_notify
start http://localhost:8039
pause