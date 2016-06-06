using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Dianzhu.Api.Model;
/// <summary>
/// 用户设备认证
/// </summary>
public class ResponseUSM001009 : BaseResponse
{
    public ResponseUSM001009(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001009 requestData = this.request.ReqData.ToObject<ReqDataUSM001009>();

        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        string newPWord = requestData.newPWord;

        try
        {
            DZMembership member = p.GetUserByPhone(requestData.phone);

            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "该电话未注册!";
                return;
            }

            p.ChangePassword(member.UserName, member.PlainPassword, newPWord);

            string result = string.Empty;
            if (member.PlainPassword == newPWord)
            {
                result = "Y";
            }
            else
            {
                result = "N";
            }

            RespDataUSM001009 respData = new RespDataUSM001009();
            respData.result = result;
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = respData;
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }
    }
}

