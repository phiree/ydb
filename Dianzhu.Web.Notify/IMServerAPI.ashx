<%@ WebHandler Language="C#" Class="IMServerAPI" %>


using System;
using System.Web;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using Ydb.InstantMessage.DomainModel.Reception;
public class IMServerAPI : IHttpHandler
{

    log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Web.Notify");

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string type = context.Request["type"];

            Ydb.InstantMessage.Application.IInstantMessage im = (Ydb.InstantMessage.Application.IInstantMessage)context.Application["im"];
            Ydb.InstantMessage.Application.IReceptionService receptionService = Bootstrap.Container.Resolve<Ydb.InstantMessage.Application.IReceptionService>();
            Ydb.Membership.Application.IDZMembershipService memberService= Bootstrap.Container.Resolve< IDZMembershipService>();
            switch (type.ToLower())
            {
                case "systemnotice":
                    string sysMsg = context.Request["body"];
                    string group = context.Request["group"];

                    break;
                case "ordernotice":
                    string strOrderId = context.Request["orderid"];
                    string strOrderTitle = context.Request["ordertitle"];
                    string strOrderStatus = context.Request["orderstatus"];
                    string strOrderType = context.Request["ordertype"];
                    string strOrderStatusFriendly = context.Request["orderstatusfriendly"];
                    string strUserId = context.Request["userid"];
                    string strToResource = context.Request["toresource"];

                    Guid orderId;
                    bool isGuid = Guid.TryParse(strOrderId, out orderId);
                    if (isGuid)
                    {
                        //发送给客户
                        im.SendNoticeOrderChangeStatus(strOrderTitle, strOrderStatus, strOrderType,
                            Guid.NewGuid(), "<订单更新>订单状态已变为:" + strOrderStatusFriendly, strUserId, strToResource, strOrderId);
                    }
                    else
                    {
                        log.Error("传入的orderid无效:'" + strOrderId + "'");
                    }
                    break;
                case "cslogin":
                    string strCSUserLoginId = context.Request["userId"];
                    Guid CSUserLoginId;
                    if (Guid.TryParse(strCSUserLoginId, out CSUserLoginId))
                    {
                        string areaCodeLoginCs=    memberService.GetUserById(strCSUserLoginId).UserCity;

                        receptionService.SendCSLoginMessageToDD(areaCodeLoginCs);
                    }
                    else
                    {
                        throw new Exception("传入的csId无效:'" + strCSUserLoginId + "'");
                    }
                    break;
                case "cslogoff":
                    string struserId = context.Request["userId"];
                    Guid userId;
                    if (Guid.TryParse(struserId, out userId))
                    {
                        string areaCodeLogoffCs = memberService.GetUserById(struserId).UserCity;
                        receptionService.SendCSLogoffMessageToDD(areaCodeLogoffCs);
                        IList<MemberDto>     meberList= memberService.GetUsersByIdList(    receptionService.GetOnlineUserList("YDBan_CustomerService"));
                        var csOnline = meberList.Select(x => new MemberArea(x.Id.ToString(), x.UserCity)).ToList();
                        receptionService.AssignCSLogoff(struserId,csOnline);
                    }

                    else
                    {
                        throw new Exception("传入的csId无效:" + struserId);
                    }
                    break;
                case "customer_change_city":
                    string paramCustomerId = context.Request["userid"];
                    string paramAreaCode= context.Request["areacode"];
                    Guid customerId;
                    if (Guid.TryParse(paramCustomerId, out customerId))
                    {

                        IList<MemberDto>     meberList= memberService.GetUsersByIdList(receptionService.GetOnlineUserList("YDBan_CustomerService"));
                        var csOnline = meberList.Select(x => new MemberArea(x.Id.ToString(), x.UserCity)).ToList();
                        receptionService.AssignCustomerChangeLocation(paramCustomerId,paramAreaCode,csOnline);

                        
                    }

                    else
                    {
                        throw new Exception("传入的csId无效:" + paramCustomerId);
                    }
                    break;
            }
        }
        catch (Exception ee)
        {
            log.Error(ee);
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