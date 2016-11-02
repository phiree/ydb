<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
using Ydb.Membership.Application;
using Ydb.Common.Application;
public class changepassword : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

    public void ProcessRequest (HttpContext context) {
        //权限判断
        if (!AjaxAuth.authAjaxUser(context)){
            context.Response.StatusCode = 400;
            context.Response.Clear();
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
            return;
        }

    
            context.Response.ContentType = "application/json";

            string change_field = context.Request["changed_field"];
            string strBusinessId = context.Request["id"];
            string strChangedValue = context.Request["changed_value"];
             
            string errMsg = string.Empty;
            bool is_valid = true;
            ActionResult result = new ActionResult();
            switch (change_field)
            {
                case "phone":
                      result = memberService.ChangePhone(strBusinessId, strChangedValue);

                    if (!result.IsSuccess)
                    {
                        errMsg =result.ErrMsg;
                        is_valid = false;
                    }

                    break;
                case "email":
                      result = memberService.ChangeEmail(strBusinessId, strChangedValue);
                     if (!result.IsSuccess)
                    {
                        errMsg =result.ErrMsg;
                        is_valid = false;
                    }

                    break;
                default:
                    is_valid = false;
                    break;
            }
            

            context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\"}");
       
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}