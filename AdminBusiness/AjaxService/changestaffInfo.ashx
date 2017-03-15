<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;

 using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
public class changepassword : IHttpHandler,System.Web.SessionState.IRequiresSessionState {


    IStaffService staffService=Bootstrap.Container.Resolve<IStaffService>();
    public void ProcessRequest (HttpContext context) {
        //权限判断
        if (!AjaxAuth.authAjaxUser(context)){
            context.Response.StatusCode = 400;
            context.Response.Clear();
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
            return;
        }

        Action ac = () => {
            context.Response.ContentType = "application/json";

            string change_field = context.Request["changed_field"];
            string strId = context.Request["id"];
            string strChangedValue = context.Request["changed_value"];
            Staff staff = staffService.GetOne(new Guid(strId));
            string errMsg = string.Empty;
            bool is_valid = true;
            switch (change_field)
            {
                case "assign":

                    staff.IsAssigned = !staff.IsAssigned;
                    break;

                default:
                    is_valid = false;
                    break;
            }
            if (is_valid)
            {
                var result= new  FluentValidation.Results.ValidationResult();
                staffService.Update(staff);
            }

            context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+staff.IsAssigned+"\"}");
        };
       
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}