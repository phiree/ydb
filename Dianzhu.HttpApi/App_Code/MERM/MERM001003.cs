﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
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

        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        string raw_id = requestData.userID;

        try
        {
            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = p.GetUserById(new Guid(raw_id));
            }
            DZMembership memberOriginal = new DZMembership();
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
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.password, "MD5");
                memberUpdateResult.password = "Y";
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
                    
                    default: break;
                }
                

            }

            if (requestData.email != null && p.GetUserByEmail(requestData.email) != null)
            {
                memberUpdateResult.email = "N";
                member.Email = memberOriginal.Email;

            }
            if (requestData.phone != null && p.GetUserByPhone(requestData.phone) != null)
            {
                memberUpdateResult.phone = "N";
                member.Phone = memberOriginal.Phone;
            }

            p.UpdateDZMembership(member);

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