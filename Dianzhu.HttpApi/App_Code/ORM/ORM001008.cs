using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM001008 : BaseResponse
{
    public ResponseORM001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001008 requestData = this.request.ReqData.ToObject<ReqDataORM001008>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLDZService bllDZService = new BLLDZService();
        PushService bllPushService = new PushService();
        BLLServiceOrderRemind bllServiceOrderRemind = new BLLServiceOrderRemind();
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = new BLLServiceOrderStateChangeHis();

        try
        {
            string raw_id = requestData.userID;
            string order_id = requestData.orderID;
            string svc_id = requestData.svcID;

            Guid uid, orderID,svcID;
            bool isUserId = Guid.TryParse(raw_id, out uid);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isSvcId = Guid.TryParse(svc_id, out svcID);
            if (!isSvcId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcId格式有误";
                return;
            }

            DZMembership member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(uid, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(uid);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该用户！";
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(orderID);

                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在！";
                    return;
                }

                DZService service = bllDZService.GetOne(svcID);
                if (service == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该服务不存在！";
                    return;
                }

                if (order.OrderStatus != enum_OrderStatus.DraftPushed)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不是已推送的服务单！";
                    return;
                }

                bllPushService.SelectServiceAndCreate(order, service);
                //foreach (ServiceOrderDetail detail in order.Details)
                //{
                //    detail.Selected = detail.OriginalService == service;
                //}
                bllServiceOrder.SaveOrUpdate(order);
                IList<ServiceOrderPushedService> pushServiceList = bllPushService.GetPushedServicesForOrder(order);
                RespDataORM_orderObj orderObj = new RespDataORM_orderObj();
                IList<DZTag> tagsList = new List<DZTag>();//标签
                if (pushServiceList.Count > 0)
                {
                    orderObj.Adap(order, pushServiceList[0]);

                    if (order.Details.Count > 0)
                    {
                        tagsList = bllDZService.GetServiceTags(order.Details[0].OriginalService);
                    }
                    else
                    {
                        tagsList = bllDZService.GetServiceTags(pushServiceList[0].OriginalService);
                    }
                }
                else
                {
                    orderObj.Adap(order, null);

                    if (order.Details.Count > 0)
                    {
                        tagsList = bllDZService.GetServiceTags(order.Details[0].OriginalService);
                    }
                }

                orderObj.svcObj.SetTag(orderObj.svcObj, tagsList);

                ServiceOrderStateChangeHis orderHis = bllServiceOrderStateChangeHis.GetOrderHis(order);
                orderObj.SetOrderStatusObj(orderObj, orderHis);

                RespDataORM001008 respData = new RespDataORM001008();
                respData.orderObj = orderObj;

                //增加订单提醒
                ServiceOrderRemind remind = new ServiceOrderRemind(orderObj.svcObj.name,orderObj.storeObj.alias+"提供"+orderObj.svcObj.type, order.Details[0].TargetTime,true, order.Id, order.Customer.Id);
                bllServiceOrderRemind.SaveOrUpdate(remind);

                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;                
            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }

}



