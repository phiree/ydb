﻿<%@ Application Language="C#" %>
<%@ Import Namespace="Castle.Windsor" %>
<%@ Import Namespace="Castle.Windsor.Installer" %>
<%@ Import Namespace="Dianzhu.Model" %>
<%@ Import Namespace="Dianzhu.BLL" %>
<%@ Import Namespace="Ydb.Finance.Application" %>
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
        IOrderShareService orderShare = Bootstrap.Container.Resolve<IOrderShareService>();
        //  NHibernateUnitOfWork.UnitOfWork.Start();


        Action a = () => {
            IList<Dianzhu.Model.ServiceOrder> ordersForShare= bllOrder.GetOrdersForShare();
            log.Debug("批量分账开始,需要分账的订单数量:" + ordersForShare.Count);
            foreach (ServiceOrder order in ordersForShare)
            {
                orderShare.ShareOrder(setOrderShareParam(order));
                bllOrder.OrderShared(order);
            }
        };
        NHibernateUnitOfWork.With.Transaction(a);
        log.Debug("批量分账结束");
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
        orderShareParam.ServiceTypeID = order.Details[0].OriginalService.ServiceType.Id.ToString();
        orderShareParam.BusinessUserId = order.Business.OwnerId.ToString();
        orderShareParam.RelatedObjectId = order.Id.ToString();
        orderShareParam.SerialNo = order.SerialNo;
        orderShareParam.Amount = order.NegotiateAmount;

        BalanceUserParam balanceAgent = new BalanceUserParam();
        Dianzhu.BLL.Agent.IAgentService agentService = Bootstrap.Container.Resolve<Dianzhu.BLL.Agent.IAgentService>();
        var area = order.Details[0].OriginalService.Business.AreaBelongTo;
        var agent = agentService.GetAreaAgent(area);
        if (agent != null)
        {
            balanceAgent.AccountId = agent.Id.ToString();
            balanceAgent.UserType = "agent";
            orderShareParam.BalanceUser.Add(balanceAgent);
        }

        orderShareParam.BalanceUser.Add(new BalanceUserParam { AccountId=order.CustomerServiceId.ToString(),UserType = "customerservice"});

        return orderShareParam;
    }


</script>
