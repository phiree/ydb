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
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM005007 requestData = this.request.ReqData.ToObject<ReqDataORM005007>();

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
            bool isAction = Enum.TryParse<enum_refundAction>(refundAction, out action);
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
                //todo:理赔还未处理
                ServiceOrder order = bllServiceOrder.GetOne(orderID);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在";
                    return;
                }

                enum_OrderStatus status;
                switch (action)
                {
                    case enum_refundAction.refund:
                        bllServiceOrder.OrderFlow_RefundSuccess(order);
                        status = order.OrderStatus;
                        break;
                    case enum_refundAction.reject:
                        status = bllServiceOrder.GetOrderStatusPrevious(order, enum_OrderStatus.Refund);
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


