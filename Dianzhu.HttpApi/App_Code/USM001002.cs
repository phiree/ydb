using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ResponseUSM001002 : BaseResponse
{
    public ResponseUSM001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001001 requestData = request.ReqData.ToObject<ReqDataUSM001001>();
        DZMembershipProvider p = new DZMembershipProvider();
        MembershipCreateStatus createStatus;
        DZMembership newMember = p.CreateUser(string.Empty,
             requestData.userPhone,
             requestData.userEmail,
             requestData.userPWord,
             out createStatus);
        if (createStatus == MembershipCreateStatus.DuplicateUserName)
        {
            this.state_CODE = Dicts.StateCode[3];
            this.err_Msg = "该用户名已经注册";
            return;
        }
        RespDataUSM001001_UserObj userObj = new RespDataUSM001001_UserObj().Adapt(newMember);
        RespDataUSM001001 resp = new RespDataUSM001001();
        resp.userObj = userObj;
        this.RespData = resp;


    }
}