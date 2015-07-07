using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class ResponseUSM001003 : BaseResponse
{
    public ResponseUSM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001003 requestData = this.request.ReqData.ToObject<ReqDataUSM001003>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {this.state_CODE=Dicts.StateCode[8];
                this.err_Msg="用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.userPWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            DZMembership memberForSerialize = new DZMembership();
             member.CopyTo(memberForSerialize);
            if (requestData.alias != null)
            {
                member.NickName = requestData.alias;
            }
            else {
                memberForSerialize.NickName = null;
            }
            if (requestData.email != null)
            {
                member.Email = requestData.email;
            }
            else
            {
                memberForSerialize.Email = null;
            }
            if (requestData.phone != null)
            {
                member.Phone = requestData.phone;
            }
            else
            {
                memberForSerialize.Phone = null;
            }
            if (requestData.password != null)
            {
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.password, "MD5");
            }
            memberForSerialize.Password = null;
            if (requestData.address != null)
            {
                member.Address = requestData.address;
            }
            else
            {
                memberForSerialize.Address = null;
            }
            p.UpdateDZMembership(member);
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = JsonConvert.SerializeObject(new RespDataUSM001003().Adapt(memberForSerialize), new JsonSerializerSettings {  DefaultValueHandling= DefaultValueHandling.Ignore});
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;

        }

    }
    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling= NullValueHandling.Ignore});
    }
}

public class ReqDataUSM001003
{
    //todo:初始化为不可能传递进来值,序列化之后对比,用以判断是否传递了该值.
    public ReqDataUSM001003()
    {
        //alias = "nosuchalias#$#";
        //email = "a@nosuch.email";
        //phone = "nosuchphone#$#";
        //password = "nosuchpassword#$#";
        //address = "nosuchaddress#$#";
    }
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string password { get; set; } //new password
    public string address { get; set; }
}
public class RespDataUSM001003
{
    public string uid { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string password { get; set; } //new password
    public string address { get; set; }
    public RespDataUSM001003 Adapt(DZMembership member)
    {
        //todo: 如果修改成功,则为"Y" 否则为"N"
        this.uid = member.Id.ToString().Replace("-", string.Empty);
        this.alias = member.NickName;
        this.email = member.Email;
        //this.password = this.password;
        this.address = member.Address;
        return this;
    }
}