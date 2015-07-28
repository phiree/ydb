<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
public class changepassword : IHttpHandler {

    BLLBusiness bllBusiness = new BLLBusiness();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        string change_field = context.Request["changed_field"];
        string strBusinessId = context.Request["id"];
        string strChangedValue = context.Request["changed_value"];
        Business b = bllBusiness.GetOne(new Guid(strBusinessId));
        string errMsg = string.Empty;
        bool is_valid = true;
        switch (change_field)
        {
            case "phone":
                
                b.Phone = strChangedValue;
                Business b2 = bllBusiness.GetBusinessByPhone(strChangedValue);
                if (b2 != null)
                {
                    errMsg = "该手机号已被注册.";
                    is_valid = false;
                }
                 
                
                break;
            case "email":
                b.Email = strChangedValue;
                 Business b3 = bllBusiness.GetBusinessByEmail(strChangedValue);
                if (b3 != null)
                {
                    errMsg = "该邮箱已被注册.";
                    is_valid = false;
                }
                break;
            default:
                is_valid = false;
                break;
        }
        if (is_valid)
        {
            bllBusiness.Updte(b);
        }
        
        context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\"}");
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}