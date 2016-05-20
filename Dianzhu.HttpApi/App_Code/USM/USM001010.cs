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
public class ResponseUSM001010 : BaseResponse
{
    public ResponseUSM001010(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001010 requestData = this.request.ReqData.ToObject<ReqDataUSM001010>();

        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();

        try
        {
            DZMembership member = p.GetUserByPhone(requestData.phone);

            string result = string.Empty;
            if (member != null)
            {
                result = "Y";
            }
            else
            {
                result = "N";
            }

            RespDataUSM001010 respData = new RespDataUSM001010();
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

