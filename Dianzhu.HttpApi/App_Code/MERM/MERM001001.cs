﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;

public class ResponseMERM001001 : BaseResponse
{
    public ResponseMERM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM requestData = request.ReqData.ToObject<ReqDataUSM>();
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        MembershipCreateStatus createStatus;
        DZMembership newMember = p.CreateUser(string.Empty,
             requestData.phone,
             requestData.email,
             requestData.pWord,
             out createStatus,
             Dianzhu.Model.Enums.enum_UserType.business);
        if (createStatus == MembershipCreateStatus.DuplicateUserName)
        {
            this.state_CODE = Dicts.StateCode[3];
            this.err_Msg = "该用户名已经注册";
            return;
        }
        this.state_CODE = Dicts.StateCode[0];
        RespDataMERM_merObj merObj = new RespDataMERM_merObj().Adapt(newMember);
        RespDataMERM resp = new RespDataMERM();
        resp.merObj = merObj;
        this.RespData = resp;


    }
}