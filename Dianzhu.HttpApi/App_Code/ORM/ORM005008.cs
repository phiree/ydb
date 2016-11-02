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
public class ResponseORM005008 : BaseResponse
{
    public ResponseORM005008(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM005008 requestData = this.request.ReqData.ToObject<ReqDataORM005008>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
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

            MemberDto member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = memberService.GetUserById(userID.ToString());
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

                RespDataORM005008 respData = new RespDataORM005008();
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


