using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 订单信息总数获取.
/// </summary>
public class ResponseORM001004 : BaseResponse
{
    public ResponseORM001004(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM001004 requestData = this.request.ReqData.ToObject<ReqDataORM001004>();

        //todo:用户验证的复用.


        try
        {
            DZMembershipProvider p = new DZMembershipProvider();
           
            string user_id = requestData.userID;
            string srvTarget = requestData.target;

            Guid userId;
            bool isUserId = Guid.TryParse(user_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    this.state_CODE = Dicts.StateCode[2];
                    this.err_Msg = "用户id或密码有误";
                    return;
                }
            }
            try
            {                
                enum_OrderSearchType searchType;
                bool isSearchType = Enum.TryParse(srvTarget, out searchType);
                if (!isSearchType)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "订单状态格式有误";
                    return;
                }

                int rowCount = bllServiceOrder.GetServiceOrderCount(userId, searchType);
                RespDataORM001004 respData = new RespDataORM001004 { sum = rowCount.ToString() };
                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;

        }

    }
    public override string BuildJsonResponse()
    {

        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
