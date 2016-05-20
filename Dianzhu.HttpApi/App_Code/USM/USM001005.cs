using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;

public class ResponseUSM001005 : BaseResponse
{
    public ResponseUSM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM requestData = request.ReqData.ToObject<ReqDataUSM>();
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        DZMembership member;
        bool validated;

        Guid userId;
        bool isGuid = Guid.TryParse(requestData.email, out userId);
        if (isGuid)
        {
            validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
        }
        else
        {
            string userName = requestData.phone ?? requestData.email;

            validated = new Account(p).ValidateUser(userName, requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }

            if(member.UserType!= Dianzhu.Model.Enums.enum_UserType.customer)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "该用户不是普通用户，登录失败";
                return;
            }
        }
        
        this.state_CODE = Dicts.StateCode[0];
        
        RespDataUSM_userObj userObj = new RespDataUSM_userObj().Adapt(member);
        RespDataUSM resp = new RespDataUSM();
        resp.userObj = userObj;
        this.RespData =resp ;
    }
}


 