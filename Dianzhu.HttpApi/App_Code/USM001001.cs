using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class ResponseUSM001001 : BaseResponse
{
    public ResponseUSM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001001 requestData = request.ReqData.ToObject<ReqDataUSM001001>();
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
        RespDataUSM001001_UserObj userObj = new RespDataUSM001001_UserObj().Adapt(member);
        RespDataUSM001001 resp = new RespDataUSM001001();
        resp.userObj = userObj;
        this.RespData =resp ;


    }
}

public class ReqDataUSM001001//001002公用.
{
    public string email { get; set; }
    public string phone { get; set; }
    public string pWord { get; set; }

}
public class RespDataUSM001001//001002公用.
{
    public RespDataUSM001001_UserObj userObj { get; set; }
}
public class RespDataUSM001001_UserObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgUrl { get; set; }
    public string address { get; set; }
    public RespDataUSM001001_UserObj Adapt(DZMembership membership)
    {
        this.userID = membership.Id.ToString() ;
        this.alias = membership.UserName??"";
        this.email = membership.Email??"";
        this.phone = membership.Phone??"";
        this.imgUrl = "";
        this.address = "";
        return this;

    }
}