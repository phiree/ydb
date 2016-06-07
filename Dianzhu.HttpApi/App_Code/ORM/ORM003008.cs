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
public class ResponseORM003008 : BaseResponse
{
    public ResponseORM003008(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM003008 requestData = this.request.ReqData.ToObject<ReqDataORM003008>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        
        string merchant_ID = requestData.merchantID;
        string order_ID = requestData.orderID;
        string negotiate_Amount = requestData.negotiateAmount;

        try
        {
            Guid merchantID, orderID;
            decimal negotiateAmount = 0;
            bool isUserId = Guid.TryParse(merchant_ID, out merchantID);
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
                negotiateAmount = decimal.Parse(negotiate_Amount);
            }
            catch (Exception e)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "请输入正确价格!";
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

                bllServiceOrder.OrderFlow_BusinessNegotiate(order, negotiateAmount);

                string resultStatus = order.OrderStatus.ToString();

                RespDataORM003008 respData = new RespDataORM003008();
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


