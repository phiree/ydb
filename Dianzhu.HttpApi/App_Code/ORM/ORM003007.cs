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
public class ResponseORM003007 : BaseResponse
{
    public ResponseORM003007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM003007 requestData = this.request.ReqData.ToObject<ReqDataORM003007>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

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

            bool isOrderId = Guid.TryParse(requestData.orderID, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
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

                enum_OrderStatus status = (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), requestData.status);
                new OrderServiceFlow().ChangeStatus(order, status);

                if (order.OrderStatus != status)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "要变更的状态无效!";
                    return;
                }

                this.state_CODE = Dicts.StateCode[0];
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


