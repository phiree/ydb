copy /Y connectionstrings_common.config ..\adminbusiness\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\adminweb\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.httpapi\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.web.notify\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.web.pay\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.csclient\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.test\config\connectionstrings.config
copy /Y connectionstrings_common.config ..\dianzhu.Web.RestfulApi\config\connectionstrings.config

copy /Y appSettings_common.config ..\adminbusiness\config\appSettings.config
copy /Y appSettings_common.config ..\adminweb\config\appSettings.config
copy /Y appSettings_common.config ..\dianzhu.httpapi\config\appSettings.config
copy /Y appSettings_common.config ..\dianzhu.web.notify\config\appSettings.config
copy /Y appSettings_common.config ..\dianzhu.web.pay\config\appSettings.config
copy /Y appSettings_common.config ..\dianzhu.csclient\config\appSettings.config
copy /Y appSettings_common.config ..\dianzhu.test\config\appSettings.config
copy /Y appSettings_common.config ..\Dianzhu.DemoClient\config\appSettings.config
copy /Y appSettings_common.config ..\DianzhuService.Diandian\config\appSettings.config

copy /Y appSettings_common.config ..\Ydb.Membership.Tests\config\appSettings.config
copy /Y appSettings_common.config ..\Ydb.Finance.Tests\config\appSettings.config
copy /Y appSettings_common.config ..\Ydb.InstantMessage.Tests\config\appSettings.config


copy /Y appSettings_restful.config ..\dianzhu.Web.RestfulApi\config\appSettings.config
pause