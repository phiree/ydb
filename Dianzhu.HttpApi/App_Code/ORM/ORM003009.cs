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
public class ResponseORM003009 : BaseResponse
{
    public ResponseORM003009(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM003009 requestData = this.request.ReqData.ToObject<ReqDataORM003009>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLServiceOrderAppraise bllServiceOrderAppraise = new BLLServiceOrderAppraise();

        string user_ID = requestData.userID;
        string order_ID = requestData.orderID;
        string appraise_Value = requestData.appraiseValue;
        string appraiseDocs = requestData.appraiseDocs;

        try
        {
            Guid userID, orderID;
            decimal appraiseValue = 0;
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
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            try
            {
                appraiseValue = decimal.Parse(appraise_Value);
            }
            catch (Exception e)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "请输入正确价格!";
                return;
            }

            if (appraiseValue < 0 || appraiseValue > 5)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "评价值应在(0-5)之间!";
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
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单!";
                    return;
                }

                bllServiceOrder.OrderFlow_CustomerAppraise(order);

                ServiceOrderAppraise appraise = new ServiceOrderAppraise(order, member, appraiseValue, appraiseDocs);
                bllServiceOrderAppraise.Save(appraise);

                string resultStatus = order.OrderStatus.ToString();

                RespDataORM003009 respData = new RespDataORM003009();
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


