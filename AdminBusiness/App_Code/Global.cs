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
        get { return container; }
    }
    void Application_Start(object sender, EventArgs e)
    {
        Bootstrap.Boot();
        container = Bootstrap.Container;

        //在应用程序启动时运行的代码
        PHSuit.Logging.Config("Dianzhu.AdminBusiness");

        System.Timers.Timer timer_ticket_assigner = new System.Timers.Timer();
        timer_ticket_assigner.Interval = 1000 * 60 * 60;
        timer_ticket_assigner.Elapsed += new System.Timers.ElapsedEventHandler(timer_ticket_assigner_Elapsed);
        timer_ticket_assigner.Start();

    }

    void timer_ticket_assigner_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        Dianzhu.BLL.CashTicketAssigner_Task cc = Bootstrap.Container.Resolve<Dianzhu.BLL.CashTicketAssigner_Task>();
        cc.Assign();
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
        //HttpContext.Current.Response.Redirect("/error.aspx?msg=" 
        //    + HttpContext.Current.Server.UrlEncode(exc.Message+"----"+ exc.InnerException.Message));

        // Handle HTTP errors


    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Start();
    }
    void Application_EndRequest(object sender, EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }
}