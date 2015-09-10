using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class ResponseUSM001005 : BaseResponse
{
    public ResponseUSM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM requestData = request.ReqData.ToObject<ReqDataUSM>();
        DZMembershipProvider p = new DZMembershipProvider();
        string userName = requestData.phone ?? requestData.email;
        bool result = p.ValidateUser(userName, requestData.pWord);
        if (!result)
        {
            this.state_CODE = Dicts.StateCode[9];
            this.err_Msg = "用户名或者密码有误"; return;
        }
       
        this.state_CODE = Dicts.StateCode[0];
        DZMembership member = p.GetUserByName(userName);
        RespDataUSM_userObj userObj = new RespDataUSM_userObj().Adapt(member);
        RespDataUSM resp = new RespDataUSM();
        resp.userObj = userObj;
        this.RespData =resp ;


    }
}


 