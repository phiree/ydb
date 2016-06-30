<%@ WebHandler Language="C#" Class="IMServerAPI" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.NotifyCenter;
public class IMServerAPI : IHttpHandler {

    log4net.ILog log = log4net.LogManager.GetLogger("debug");
    IBLLServiceOrder bllOrder =Bootstrap.Container.Resolve<IBLLServiceOrder>();

    public void ProcessRequest(HttpContext context)
    {
        Action ac = () => {

            string type = context.Request["type"];
            Dianzhu.CSClient.IInstantMessage.InstantMessage im
            = (Dianzhu.CSClient.IInstantMessage.InstantMessage)context.Application["im"];
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
                        imNotify.SendOrderChangedNotify(order);
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + strOrderId + "'");
                    }
                    break;
                case "cslogoff":
                    string struserId = context.Request["userId"];
                    Guid userId;
                    bool isGid = Guid.TryParse(struserId, out userId);
                    if (isGid)
                    {
                        imNotify.SendRessaginMessage(userId);
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + struserId + "'");
                    }
                    break;
                case "customlogoff":
                    string strcustomId = context.Request["userId"];
                    Guid customId;
                    bool isGidcustom = Guid.TryParse(strcustomId, out customId);
                    if (isGidcustom)
                    {
                        imNotify.SendCustomLogoffMessage(customId);
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + strcustomId + "'");
                    }
                    break;
                case "customlogin":
                    string strlogincustomId = context.Request["userId"];
                    Guid logincustomId;
                    bool isGidlogincustom = Guid.TryParse(strlogincustomId, out logincustomId);
                    if (isGidlogincustom)
                    {
                        imNotify.SendCustomLoginMessage(logincustomId);
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + strlogincustomId + "'");
                    }
                    break;
            }

            //get cslist, using xmpp

            //get order or create new order
            // return cs,and order 
        };
        NHibernateUnitOfWork.With.Transaction(ac);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}