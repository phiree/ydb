<%@ Application Language="C#" %>
<%@ Import Namespace="Castle.Windsor" %>
<%@ Import Namespace="Castle.Windsor.Installer" %>
 
 
<%@ Import Namespace="Ydb.BusinessResource.Application" %>
<%@ Import Namespace="Ydb.BusinessResource.DomainModel" %>
<%@ Import Namespace="Ydb.Finance.Application" %>
<%@ Import Namespace="Ydb.Common.Application" %>
<%@ Import Namespace="Ydb.Order.Application" %>
<%@ Import Namespace="Ydb.Order.DomainModel" %>
<script runat="server">
    public static IWindsorContainer container;
    //  Dianzhu.IDAL.IUnitOfWork uow = Bootstrap.Container.Resolve<Dianzhu.IDAL.IUnitOfWork>();
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminWeb");
    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        //PHSuit.Logging.Config("Dianzhu.AdminWeb");
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

        IServiceOrderService bllOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
        IOrderShareService orderShare = Bootstrap.Container.Resolve<IOrderShareService>();
        //  NHibernateUnitOfWork.UnitOfWork.Start();

        int c = 0;
        Action a = () => {
            IList< ServiceOrder> ordersForShare= bllOrder.GetOrdersForShare();
            c = ordersForShare.Count;
            if (c > 0)
            {
                log.Debug("批量分账开始,需要分账的订单数量:" + ordersForShare.Count);
            }
            foreach (ServiceOrder order in ordersForShare)
            {
                orderShare.ShareOrder(setOrderShareParam(order));
                bllOrder.OrderShared(order.Id);
            }
        };
        NHibernateUnitOfWork.With.Transaction(a);
        if (c > 0)
        {
            log.Debug("批量分账结束");
        }
        //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

    }

    /// <summary>
    /// 根据订单信息生成该订单的分账参数
    /// </summary>
    /// <param name="order" type="Dianzhu.Model.ServiceOrder">订单信息</param>
    /// <returns type="Ydb.Finance.Application.OrderShareParam">该订单的分账参数</returns>
    OrderShareParam setOrderShareParam(ServiceOrder order)
    {
        OrderShareParam orderShareParam = new OrderShareParam();

        Ydb.Membership.Application.IDZMembershipService memberService = Bootstrap.Container.Resolve<Ydb.Membership.Application.IDZMembershipService>();
        IDZServiceService dzService = Bootstrap.Container.Resolve<IDZServiceService>();
        DZService service = dzService.GetOne2(new Guid(order.Details[0].OriginalServiceId));
        orderShareParam.ServiceTypeID = service.ServiceType.Id.ToString();//  order.Details[0].ServiceSnapShot.ServiceType.Id.ToString();
        orderShareParam.BusinessUserId = order.BusinessId.ToString();
        orderShareParam.RelatedObjectId = order.Id.ToString();
        orderShareParam.SerialNo = order.SerialNo;
        orderShareParam.Amount = order.NegotiateAmount;
        orderShareParam.BalanceUser = new List<BalanceUserParam>();
        BalanceUserParam balanceAgent = new BalanceUserParam();

        var agent = memberService.GetAreaAgent(service.Business.AreaBelongTo);
        if (agent.ResultObject != null)
        {
            balanceAgent.AccountId = agent.ResultObject.Id.ToString();
            balanceAgent.UserType = "agent";
            orderShareParam.BalanceUser.Add(balanceAgent);
        }

        orderShareParam.BalanceUser.Add(new BalanceUserParam { AccountId=order.CustomerServiceId.ToString(),UserType = "customerservice"});

        return orderShareParam;
    }


</script>
