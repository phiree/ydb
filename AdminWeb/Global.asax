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
        System.Timers.Timer timerOrderShare = new System.Timers.Timer(60*1000);
        timerOrderShare.Elapsed += new System.Timers.ElapsedEventHandler(timerOrderShare_Elapsed);
        
        timerOrderShare.Start();
        PHSuit.HttpHelper._SetupRefreshJob(888);
        // timerOrderShare_Elapsed(null, null);
        // var container = Installer.Container;
    }
    void timerOrderShare_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {

        Dianzhu.BLL.IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<Dianzhu.BLL.IBLLServiceOrder>();
        Dianzhu.BLL.Finance.IOrderShare orderShare = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IOrderShare>();
       
        Action a = () => {
             IList<Dianzhu.Model.ServiceOrder> ordersForShare= bllOrder.GetOrdersForShare();
        log.Debug("批量分账开始,需要分账的订单数量:" + ordersForShare.Count);
            foreach (ServiceOrder order in ordersForShare)
            {
                orderShare.ShareOrder(order);
                bllOrder.OrderFlow_Shared(order);
            }
        };
           NHibernateUnitOfWork.With.Transaction(a);
        log.Debug("批量分账结束");
    }

</script>
