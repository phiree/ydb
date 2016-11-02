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
public class ResponsePY001008 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponsePY001008(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataPY001008 requestData = this.request.ReqData.ToObject<ReqDataPY001008>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();

        string raw_id = requestData.userID;
        string order_id = requestData.orderID;

        try
        {
            Guid userId, orderId;
            bool isUserId = Guid.TryParse(requestData.userID, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }
            
            if (request.NeedAuthenticate)
            {
                MemberDto member;
                bool validated = new Account(memberService).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }

            ServiceOrder order = bllServiceOrder.GetOne(new Guid(order_id));
            if (order == null)
            {
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "没有对应的订单,请检查传入的orderID";
                return;
            }

            try
            {
                Payment payment = bllPayment.GetPaymentForWaitPay(order);
                if (payment == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单目前没有支付项！";
                    return;
                }
                RespDataPY001008 respData = new RespDataPY001008().Adapt(payment);

                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
            }
            catch (Exception ex)
            {
                ilog.Error(ex.Message);
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


