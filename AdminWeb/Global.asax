<%@ Application Language="C#" %>
<%@ Import Namespace="Castle.Windsor" %>
<%@ Import Namespace="Castle.Windsor.Installer" %>
<%@ Import Namespace="Dianzhu.Model" %>
<%@ Import Namespace="Dianzhu.BLL" %>
<script runat="server">
    public static IWindsorContainer container;
    //  Dianzhu.IDAL.IUnitOfWork uow = Bootstrap.Container.Resolve<Dianzhu.IDAL.IUnitOfWork>();
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminWeb");
    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        PHSuit.Logging.Config("Dianzhu.AdminWeb");
        //InitializeWindsor();
        Bootstrap.Boot();
        System.Timers.Timer timerOrderShare = new System.Timers.Timer(60000);
        timerOrderShare.Elapsed += new System.Timers.ElapsedEventHandler(timerOrderShare_Elapsed);
        timerOrderShare.Start();
        // timerOrderShare_Elapsed(null, null);
        // var container = Installer.Container;
    }
    void timerOrderShare_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {

        Dianzhu.BLL.IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<Dianzhu.BLL.IBLLServiceOrder>();
        Dianzhu.BLL.Finance.IOrderShare orderShare = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IOrderShare>();
        IList<Dianzhu.Model.ServiceOrder> ordersForShare= bllOrder.GetOrdersForShare();
        log.Debug("批量分账开始,需要分账的订单数量:" + ordersForShare.Count);
        Action a = () => { 
        foreach (ServiceOrder order in ordersForShare)
        {
            orderShare.ShareOrder(order);
            bllOrder.OrderFlow_Shared(order);
        }
        };
     //   NHibernateUnitOfWork.With.Transaction(a);
        log.Debug("批量分账结束");
    }
    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码


    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。


    }
    void Application_BeginRequest(Object source, EventArgs e)
    {
        // HttpApplication app = (HttpApplication)source;
        // PHSuit.FirstRequestInitialisation.Initialise(app.Context);

        //   NHibernateUnitOfWork.UnitOfWork.Start();
    }
    void Application_EndRequest(object sender, EventArgs e)
    {
        // NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }


</script>
