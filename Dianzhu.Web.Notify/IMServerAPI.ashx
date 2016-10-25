<%@ WebHandler Language="C#" Class="IMServerAPI" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.NotifyCenter;
public class IMServerAPI : IHttpHandler
{

    log4net.ILog log = log4net.LogManager.GetLogger("debug");
    IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            NHibernateUnitOfWork.UnitOfWork.Start();


            string type = context.Request["type"];

            Ydb.InstantMessage.Application.IInstantMessage im = (Ydb.InstantMessage.Application.IInstantMessage)context.Application["im"];

            Dianzhu.NotifyCenter.IMNotify imNotify = Bootstrap.Container.Resolve<IMNotify>();


            switch (type.ToLower())
            {
                case "systemnotice":
                    string sysMsg = context.Request["body"];
                    imNotify.SendSysNoitification(sysMsg);
                    break;
                case "ordernotice":
                    string strOrderId = context.Request["orderId"];

                    Guid orderId;
                    bool isGuid = Guid.TryParse(strOrderId, out orderId);
                    if (isGuid)
                    {
                        ServiceOrder order = bllOrder.GetOne(orderId);
                        //imNotify.SendOrderChangedNotify(order);
                        //发送给客户
                        im.SendOrderChangeStatusNotify(order.Title, order.OrderStatus.ToString(), order.Service.ServiceType.Name,
                            Guid.NewGuid(), "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus), order.Customer.Id.ToString(), "YDBan_User", order.Id.ToString());

                        //发送给商户
                        if (order.Business == null)
                        {
                            return;
                        }
                        if (order.Business.Owner == null)
                        {
                            return;
                        }
                        im.SendOrderChangeStatusNotify(order.Title, order.OrderStatus.ToString(), order.Service.ServiceType.Name,
                        Guid.NewGuid(), "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus), order.Business.Owner.Id.ToString(), "YDBan_Store", order.Id.ToString());

                        //发送给指派的员工
                        if (order.Staff == null)
                        {
                            return;
                        }
                        im.SendOrderChangeStatusNotify(order.Title, order.OrderStatus.ToString(), order.Service.ServiceType.Name,
                        Guid.NewGuid(), "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus), order.Staff.Id.ToString(), "YDBan_Staff", order.Id.ToString());
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + strOrderId + "'");
                    }
                    break;
                case "cslogin":
                    im.SendCSLoginMessageToDD(Guid.NewGuid(), string.Empty, Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"), "YDBan_DianDian", string.Empty);
                    //string strCSUserLoginId = context.Request["userId"];
                    //Guid CSUserLoginId;
                    //bool isCSUserLoginId = Guid.TryParse(strCSUserLoginId, out CSUserLoginId);
                    //if (isCSUserLoginId)
                    //{
                    //    imNotify.SendCSLoginMessageToDD(CSUserLoginId);
                    //}
                    //else
                    //{
                    //    log.Error("传入的csId无效:'" + strCSUserLoginId + "'");
                    //}
                    break;
                case "cslogoff":
                    im.SendCSLogoffMessageToDD(Guid.NewGuid(), string.Empty, Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"), "YDBan_DianDian", string.Empty);
                    //string struserId = context.Request["userId"];
                    //Guid userId;
                    //bool isGid = Guid.TryParse(struserId, out userId);
                    //if (isGid)
                    //{
                    //    imNotify.SendRessaginMessage(userId);
                    //}
                    //else
                    //{
                    //    log.Error("传入的csId无效:'" + struserId + "'");
                    //}
                    break;
            }
        }
        catch (Exception ee)
        {
            log.Error(ee);
        }
        finally
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}