using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class ResponseUSM001004 : BaseResponse
{
    public ResponseUSM001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001004 requestData = this.request.ReqData.ToObject<ReqDataUSM001004>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE ="用户不存在,可能是传入的uid有误";
                return;
            }
            BLLDeviceBind bllDeviceBind = new BLLDeviceBind();
            //验证用户的密码

            try
            {
                bllDeviceBind.UpdateDeviceBindStatus(member, requestData.appToken, requestData.appName);
                this.state_CODE = Dicts.StateCode[0];
                
            }
            catch (Exception ex)
            { 
                this.state_CODE=ex.Message;   
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

public class ReqDataUSM001004
{
    public string uid { get; set; }
    public string appName { get; set; }
    public string appToken { get; set; }
 
}
 