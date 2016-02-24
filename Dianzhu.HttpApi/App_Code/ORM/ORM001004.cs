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
    protected override void BuildRespData()
    {
        ReqDataORM001004 requestData = this.request.ReqData.ToObject<ReqDataORM001004>();

        //todo:用户验证的复用.


        try
        {
            DZMembershipProvider p = new DZMembershipProvider();
            BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
            string raw_id = requestData.userID;
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
                string srvTarget = requestData.target;
                enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);

                int rowCount = bllServiceOrder.GetServiceOrderCount(new Guid(raw_id), searchType);
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
