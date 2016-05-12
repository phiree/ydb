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
public class ResponseORM005010 : BaseResponse
{
    public ResponseORM005010(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM005010 requestData = this.request.ReqData.ToObject<ReqDataORM005010>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
      
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

                RespDataORM_refundStatusObj refundStatusObj = new RespDataORM_refundStatusObj
                {
                    refundStatusID = "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    orderID = "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    context = "弄坏我花瓶，赔我钱",
                    amount = "50",
                    resourcesUrl = "http://imgsrc.baidu.com/forum/w=580/sign=04e1e17ac5fdfc03e578e3b0e43e87a9/1967c5177f3e67090520527b3dc79f3df9dc5577.jpg",
                    date = "201506162223",
                    orderStatus = "AskPayWithRefund",
                    target= "user"
                };

                IList<RespDataORM_refundStatusObj> refundStatusList = new List<RespDataORM_refundStatusObj>();
                refundStatusList.Add(refundStatusObj);
                refundStatusList.Add(refundStatusObj);
                refundStatusList.Add(refundStatusObj);

                RespDataORM005010 respData = new RespDataORM005010();
                respData.arrayData = refundStatusList;

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


