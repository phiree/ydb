using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
/// <summary>
/// Global 的摘要说明
/// </summary>
public class Global : HttpApplication, IContainerAccessor
{
    public Global()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    static IWindsorContainer container;

    public IWindsorContainer Container
    {
        get { return container; }
    }
    void Application_Start(object sender, EventArgs e)
    {
        Bootstrap.Boot();
        container = Bootstrap.Container;

        //在应用程序启动时运行的代码
        Ydb.Common.LoggingConfiguration.Config();// PHSuit.Logging.Config("Dianzhu.AdminBusiness");



    }




    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        throw Server.GetLastError();
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminBusiness");
        Exception exc = Server.GetLastError();
        //Server.ClearError();
        PHSuit.ExceptionLoger.ExceptionLog(log, exc);
#if DEBUG
    
#endif
        HttpContext.Current.Response.Redirect("/error.aspx?msg="
            + HttpContext.Current.Server.UrlEncode(exc.Message + "----" + exc.InnerException.Message));



    }



}