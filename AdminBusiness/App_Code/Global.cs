using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
/// <summary>
/// Global 的摘要说明
/// </summary>
public class Global:HttpApplication, IContainerAccessor
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
        get { return container;      }
    }
    void Application_Start(object sender, EventArgs e)
    {
        Bootstrap.Boot();
        container = Bootstrap.Container;

        //在应用程序启动时运行的代码
        PHSuit.Logging.Config("Dianzhu.AdminBusiness");

        

    }

 


    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("dz");
        Exception exc = Server.GetLastError();
        //Server.ClearError();
        log.Error(exc.Message);
        if (exc.InnerException != null)
        {
            log.Error(exc.InnerException.Message);
        }
#if DEBUG
    
#endif
        HttpContext.Current.Response.Redirect("/error.aspx?msg="
            + HttpContext.Current.Server.UrlEncode(exc.Message + "----" + exc.InnerException.Message));

        // Handle HTTP errors


    }

    /*
    void Application_BeginRequest(object sender, EventArgs e)
    {
        if (!Request.CurrentExecutionFilePath.EndsWith(".aspx"))
        {
            return;
        }
        NHibernateUnitOfWork.UnitOfWork.Start();

    }

    void Application_EndRequest(object sender, EventArgs e)
    {
        if (!Request.CurrentExecutionFilePath.EndsWith(".aspx"))
        {
            return;
        }
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }*/

}