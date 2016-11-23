﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM001008 : BaseResponse
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpApi.ORM001008");

    public ResponseORM001008(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM001008 requestData = this.request.ReqData.ToObject<ReqDataORM001008>();

        //todo:用户验证的复用.


        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();
        PushService bllPushService = Bootstrap.Container.Resolve<PushService>();

        BLLServiceOrderRemind bllServiceOrderRemind = Bootstrap.Container.Resolve<BLLServiceOrderRemind>();
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = Bootstrap.Container.Resolve<BLLServiceOrderStateChangeHis>();


        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        IDZMembershipService memberServcie = Bootstrap.Container.Resolve<IDZMembershipService>();

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

            MemberDto member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberServcie).ValidateUser(uid, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = memberServcie.GetUserById(uid.ToString());
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

                DZService service = dzServiceService.GetOne(svcID);
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
                    log.Error("该订单不是已推送的服务单！该订单Id:" + order.Id + "该订单当前状态:" + order.OrderStatus);
                    return;
                }

                bllPushService.SelectServiceAndCreate(order, service);
                //foreach (ServiceOrderDetail detail in order.Details)
                //{
                //    detail.Selected = detail.OriginalService == service;
                //}
                bllServiceOrder.Update(order);
                IList<ServiceOrderPushedService> pushServiceList = bllPushService.GetPushedServicesForOrder(order);
                RespDataORM_orderObj orderObj = new RespDataORM_orderObj();
                MemberDto customer = memberServcie.GetUserById(order.CustomerId);
                IList<DZTag> tagsList = new List<DZTag>();//标签
                if (pushServiceList.Count > 0)
                {

                    orderObj.Adap(order, memberServcie, pushServiceList[0]);

                    if (order.Details.Count > 0)
                    {
                        tagsList = dzServiceService.GetServiceTags(order.Details[0].OriginalService);
                    }
                    else
                    {
                        tagsList = dzServiceService.GetServiceTags(pushServiceList[0].OriginalService);
                    }
                }
                else
                {
                    orderObj.Adap(order,memberServcie, null);

                    if (order.Details.Count > 0)
                    {
                        tagsList = dzServiceService.GetServiceTags(order.Details[0].OriginalService);
                    }
                }

                orderObj.svcObj.SetTag(orderObj.svcObj, tagsList);

                ServiceOrderStateChangeHis orderHis = bllServiceOrderStateChangeHis.GetOrderHis(order);
                orderObj.SetOrderStatusObj(orderObj, orderHis);

                RespDataORM001008 respData = new RespDataORM001008();
                respData.orderObj = orderObj;

                //增加订单提醒
                ServiceOrderRemind remind = new ServiceOrderRemind(orderObj.svcObj.name,orderObj.storeObj.alias+"提供"+orderObj.svcObj.type, order.Details[0].TargetTime,true, order.Id,new Guid( order.CustomerId));
                bllServiceOrderRemind.Save(remind);

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



