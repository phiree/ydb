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
public class ResponseVCM001001 : BaseResponse
{
    public ResponseVCM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataVCM001001 requestData = this.request.ReqData.ToObject<ReqDataVCM001001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLCashTicket bllCashTicket = new BLLCashTicket();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.userPWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
                string srvTarget = requestData.vcsTarget;
                enum_CashTicketSearchType searchType = (enum_CashTicketSearchType)Enum.Parse(typeof(enum_CashTicketSearchType), srvTarget);

                int rowCount = bllCashTicket.GetCount(uid, searchType);
                RespDataVCM001001 respData = new RespDataVCM001001 { sum = rowCount.ToString() };
                this.RespData = JsonConvert.SerializeObject(respData);
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

public class ReqDataVCM001001
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string vcsTarget { get; set; }

}
public class RespDataVCM001001
{
    public string sum { get; set; }
}
