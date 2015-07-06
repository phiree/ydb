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
    protected override void BuildResponse()
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
        RespDataUSM001001 respModel = new RespDataUSM001001().Adapt(newMember);
        this.RespData = JsonConvert.SerializeObject(respModel);


    }
}

public class ReqDataUSM001001//001002公用.
{
    public string userEmail { get; set; }
    public string userPhone { get; set; }
    public string userPWord { get; set; }

}
public class RespDataUSM001001//001002公用.
{
    public string uid { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgurl { get; set; }
    public string address { get; set; }
    public RespDataUSM001001 Adapt(DZMembership membership)
    {
        this.uid = membership.Id.ToString().Replace("-", string.Empty).ToUpper();
        this.alias = membership.UserName;
        this.email = membership.Email;
        this.phone = membership.Phone;
        this.imgurl = "";
        this.address = "";
        return this;

    }
}