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
public class ResponseORM005009 : BaseResponse
{
    public ResponseORM005009(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM005009 requestData = this.request.ReqData.ToObject<ReqDataORM005009>();

        bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
      
        string user_ID = requestData.userID;
        string order_ID = requestData.orderID;

        try
        {
            Guid userID, orderID;
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
                //todo:理赔还未处理

                RespDataORM005009 respData = new RespDataORM005009();
                respData.resultStatus = "WaitingPayWithRefund";

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


