using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM003010 : BaseResponse
{
    public ResponseORM003010(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM003010 requestData = this.request.ReqData.ToObject<ReqDataORM003010>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        BLLServiceOrderStateChangeHis bllServiceOrderHis = Bootstrap.Container.Resolve<BLLServiceOrderStateChangeHis>();
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();
        BLLPaymentLog bllPaymentLog = Bootstrap.Container.Resolve<BLLPaymentLog>();
        string raw_id = requestData.userID;

        try
        {
            Guid userId,orderId;
            bool isUserId = Guid.TryParse(raw_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(requestData.orderID, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            MemberDto member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = memberService.GetUserById(userId.ToString());
            }
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "用户不存在";
                return;
            }

            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(new Guid(requestData.orderID));
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的orderID";
                    return;
                }

                if(order.OrderStatus!= enum_OrderStatus.Ended)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "该订单不是支付尾款的状态";
                    return;
                }

                bllServiceOrder.OrderFlow_OrderFinished(order);

                Payment payment = bllPayment.ApplyPay(order, enum_PayTarget.FinalPayment);

                if (payment == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有支付项";
                    return;
                }

                //保存记录
                PaymentLog paymentLog = new PaymentLog();
                paymentLog.PaylogType = enum_PaylogType.None;
                paymentLog.PayType = enum_PayType.Offline;
                paymentLog.PayAmount = payment.Amount;
                paymentLog.PaymentId = payment.Id;
                bllPaymentLog.Save(paymentLog);
                
                //更新支付项              
                payment.Status = enum_PaymentStatus.Trade_Success;
                payment.PayType = enum_PayType.Offline;
                payment.PayApi = enum_PayAPI.None;
                payment.Memo = "线下支付";
                bllPayment.Update(payment);

                string resultStatus = order.OrderStatus.ToString();

                RespDataORM003010 respData = new RespDataORM003010();
                respData.resultStatus = resultStatus;

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

