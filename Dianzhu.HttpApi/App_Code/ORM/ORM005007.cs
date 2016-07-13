using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM005007 : BaseResponse
{
    public ResponseORM005007(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM005007 requestData = this.request.ReqData.ToObject<ReqDataORM005007>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        string merchant_ID = requestData.merchantID;
        RespDataORM_refundObj refundObj = requestData.refundObj;
        string refundAction = requestData.refundAction;

        try
        {
            Guid merchantID, orderID;
            bool isUserId = Guid.TryParse(merchant_ID, out merchantID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(refundObj.orderID, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "orderId格式有误";
                return;
            }

            enum_refundAction action;
            bool isAction = Enum.TryParse(refundAction, out action);
            if (!isAction)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "动作枚举格式有误";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(merchantID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(merchantID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "用户不存在!";
                    return;
                }
            }
            try
            {
                if(member.UserType!= enum_UserType.business)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "该账号不是商户";
                    return;
                }

                ServiceOrder order = bllServiceOrder.GetOne(orderID);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在";
                    return;
                }

                if (order.Details[0].OriginalService.Business.Owner.Id != member.Id)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "该订单不属于该用商户";
                    return;
                }

                enum_OrderStatus status;
                switch (action)
                {
                    case enum_refundAction.refund:
                        bllServiceOrder.OrderFlow_BusinessIsRefund(order, member);
                        status = order.OrderStatus;
                        break;
                    case enum_refundAction.reject:
                        bllServiceOrder.OrderFlow_BusinessRejectRefund(order,member);
                        status = order.OrderStatus;
                        break;
                    case enum_refundAction.askPay:
                        decimal refundAmount;
                        if (!decimal.TryParse(refundObj.amount, out refundAmount))
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "理赔金额有误";
                            return;
                        }

                        if (refundAmount <= 0)
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "赔偿金额必须大于零";
                            return;
                        }

                        //20160623_longphui_modify
                        string[] resourcesUrls = refundObj.resourcesUrl.Split(',');

                        bllServiceOrder.OrderFlow_BusinessAskPayWithRefund(order, refundObj.context, refundAmount, resourcesUrls.ToList(), member);

                        status = order.OrderStatus;
                        break;
                    default:
                        this.state_CODE = Dicts.StateCode[4];
                        this.err_Msg = "暂未支持该类型";
                        return;
                }

                RespDataORM005007 respData = new RespDataORM005007();
                respData.resultStatus = status.ToString();

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


