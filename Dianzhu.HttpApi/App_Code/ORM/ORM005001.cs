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
    IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
    public ResponseORM005001(BaseRequest request) : base(request) { }
   
    protected override void BuildRespData()
    {
        ReqDataORM005001 requestData = this.request.ReqData.ToObject<ReqDataORM005001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();

        BLLServiceOrderAppraise bllServiceOrderAppraise = new BLLServiceOrderAppraise();

       
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

            if (amount <= 0)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "提交价格需为大于零的数";
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
                enum_OrderStatus oldStatus = order.OrderStatus;
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在";
                    return;
                }

                bool isNeesRefund = false;
                if (order.OrderStatus == enum_OrderStatus.Begin ||
                     order.OrderStatus == enum_OrderStatus.isEnd ||
                      order.OrderStatus == enum_OrderStatus.Ended)
                {
                    isNeesRefund = false;
                }
                else if (order.OrderStatus == enum_OrderStatus.Finished ||
                          order.OrderStatus == enum_OrderStatus.Appraised)
                {
                    isNeesRefund = true;
                }
                else
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单状态无法提交理赔";
                    return;
                }

                if (!bllServiceOrder.OrderFlow_CustomerRefund(order,isNeesRefund, amount))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "提交理赔失败";
                    return;
                }

                Claims claims = new Claims(order, oldStatus, member);
                claims.AddDetailsFromClaims(claims, context, amount, resourcesUrl, enum_ChatTarget.user, member);
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


