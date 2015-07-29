<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
public class changepassword : IHttpHandler {

    DZMembershipProvider dzp = new DZMembershipProvider();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        string change_field = context.Request["changed_field"];
        string strBusinessId = context.Request["id"];
        string strChangedValue = context.Request["changed_value"];
        DZMembership member = dzp.GetUserById(new Guid(strBusinessId));
        string errMsg = string.Empty;
        bool is_valid = true;
        switch (change_field)
        {
            case "phone":

                member.Phone = strChangedValue;
                var member2 = dzp.GetUserByPhone(strChangedValue);
                if (member2 != null)
                {
                    errMsg = "该手机号已被注册.";
                    is_valid = false;
                }
                 
                
                break;
            case "email":
                member.Email = strChangedValue;
                var b3 = dzp.GetUserByEmail(strChangedValue);
                 if (b3 != null)
                 {
                     errMsg = "该邮箱已被注册.";
                     is_valid = false;
                 }
                 else
                 {
                     member.IsRegisterValidated = false;
                     member.RegisterValidateCode = Guid.NewGuid().ToString();
                 }
                break;
            default:
                is_valid = false;
                break;
        }
        if (is_valid)
        {
            dzp.UpdateDZMembership(member);
        }
        
        context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\"}");
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}