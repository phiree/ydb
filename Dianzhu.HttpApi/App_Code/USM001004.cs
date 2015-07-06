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
    protected override void BuildResponse()
    {
        ReqDataUSM001004 requestData = this.request.ReqData.ToObject<ReqDataUSM001004>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
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
}

public class ReqDataUSM001004
{
    public string uid { get; set; }
    public string appName { get; set; }
    public string appToken { get; set; }
 
}
 