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
public class ResponseORM005001 : BaseResponse
{
    public ResponseORM005001(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM005001 requestData = this.request.ReqData.ToObject<ReqDataORM005001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();

        BLLServiceOrderAppraise bllServiceOrderAppraise = new BLLServiceOrderAppraise();

        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLClaims bllClaims = new BLLClaims();


        string user_ID = requestData.userID;

        if (requestData.refundObj == null)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "理赔对象不能为空!";
            return;
        }

        RespDataORM_refundObj refundObj = requestData.refundObj;
        string order_ID = refundObj.orderID;
        string amount_str = refundObj.amount;
        string context = refundObj.context;
        string amountStr = refundObj.amount;
        string resourcesUrl = refundObj.resourcesUrl;

        try
        {
            Guid userID, orderID;
            decimal amount = 0;
            bool isUserId = Guid.TryParse(user_ID, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_ID, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "orderId格式有误";
                return;
            }

            bool isAmount = decimal.TryParse(amountStr, out amount);
            if (!isAmount)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "提交价格的格式有误";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(userID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "用户不存在!";
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOrderByIdAndCustomer(orderID, member);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在";
                    return;
                }

                bllServiceOrder.OrderFlow_CustomerRefund(order);

                Claims claims = new Claims(order, context, amount, resourcesUrl, order.OrderStatus, enum_ChatTarget.cer, string.Empty);
                bllClaims.Save(claims);

                RespDataORM005001 respData = new RespDataORM005001();
                respData.resultStatus = order.OrderStatus.ToString();

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


