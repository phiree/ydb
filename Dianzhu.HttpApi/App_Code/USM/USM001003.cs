using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL.Validator;
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
        string raw_id = requestData.userID;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.pWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            DZMembership memberOriginal = new DZMembership();
            member.CopyTo(memberOriginal);
            RespDataUSM001003 memberUpdateResult = new RespDataUSM001003(raw_id);
            if (requestData.alias != null)
            {
                member.NickName = requestData.alias;
                memberUpdateResult.alias = "Y";
            }
             
            if (requestData.email != null)
            {
                member.Email = requestData.email;
                memberUpdateResult.email = "Y";
            }
             
            if (requestData.phone != null)
            {
                member.Phone = requestData.phone;
                memberUpdateResult.phone = "Y";
            }
            
            if (requestData.password != null)
            {
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.password, "MD5");
                memberUpdateResult.password = "Y";
            }
             
            if (requestData.address != null)
            {
                member.Address = requestData.address;
                memberUpdateResult.address = "Y";
            }
             

            ValidatorDZMembership vd_member = new ValidatorDZMembership();


            FluentValidation.Results.ValidationResult result = vd_member.Validate(member);
            foreach (FluentValidation.Results.ValidationFailure f in result.Errors)
            {
                switch (f.PropertyName.ToLower())
                {
                        //只有不为null的菜需要
                    case "alias":
                        if (memberUpdateResult.alias != null)
                        {
                            memberUpdateResult.alias = "N";
                            member.NickName = memberOriginal.NickName;
                        }
                        break;
                    case "email":
                        if(memberUpdateResult.email!=null)
                        {
                        memberUpdateResult.email = "N";
                        member.Email = memberOriginal.Email;
                        }break;
                    case "phone": 
                        if(memberUpdateResult.phone!=null)
                        {
                            memberUpdateResult.phone = "N"; member.Phone = memberOriginal.Phone;
                        }
                        break;
                    case "password":
                        if(memberUpdateResult.password!=null)
                        {
                        memberUpdateResult.password = "N";
                        member.Password = memberOriginal.Password;
                        }break;
                    case "address": 
                        if(memberUpdateResult.address!=null)
                        {
                        memberUpdateResult.address = "N";
                        member.Address = memberOriginal.Address;
                        } break;
                    default: break;
                }
                

            }

           

            p.UpdateDZMembership(member);
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = memberUpdateResult;
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message+e.InnerException==null?string.Empty:e.InnerException.Message;

        }

    }
    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
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
    public string userID { get; set; }
    public string pWord { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string password { get; set; } //new password
    public string address { get; set; }
}
public class RespDataUSM001003
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string password { get; set; } //new password
    public string address { get; set; }
    public RespDataUSM001003(string uid)
    {
        //todo: 如果修改成功,则为"Y" 否则为"N"
        this.userID = uid;
        this.alias = null;
        this.email =null;
        this.phone = null;
        this.password = null;
        this.address = null;
    }
}