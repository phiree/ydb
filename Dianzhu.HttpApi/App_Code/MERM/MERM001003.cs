using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.BLL.Validator;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;

public class ResponseMERM001003 : BaseResponse
{
    public ResponseMERM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataMERM001003 requestData = this.request.ReqData.ToObject<ReqDataMERM001003>();

       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        string raw_id = requestData.userID;

        try
        {
            MemberDto member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = memberService.GetUserById(new Guid(raw_id).ToString());
            }
            MemberDto memberOriginal = new MemberDto();
            member.CopyTo(memberOriginal);
            ReqDataMERM001003 memberUpdateResult = new ReqDataMERM001003();
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
                //todo: 密码不能传给前端
             
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
                            memberUpdateResult.phone = "N";

                            member.Phone = memberOriginal.Phone;
                        }
                        break;
                    
                    
                    default: break;
                }
                

            }

            if (requestData.email != null )
            {
                memberUpdateResult.email = "N";
                member.Email = memberOriginal.Email;
                memberService.ChangeEmail(raw_id, memberOriginal.Email);

            }
            if (requestData.phone != null )
            {
                memberUpdateResult.phone = "N";
                memberService.ChangeEmail(raw_id, memberOriginal.Phone); 
            }
 
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = memberUpdateResult;
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            PHSuit.ExceptionLoger.ExceptionLog(Log, e);

        }

    }
    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}