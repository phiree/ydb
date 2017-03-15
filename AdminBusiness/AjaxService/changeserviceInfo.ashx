<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;

     using Ydb.BusinessResource.Application;
    using Ydb.BusinessResource.DomainModel;
public class changepassword : IHttpHandler,System.Web.SessionState.IRequiresSessionState {


        IDZServiceService dzService= Bootstrap.Container.Resolve<IDZServiceService>();
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
            DZService service = dzService.GetOne2(new Guid(strId));
            string errMsg = string.Empty;
            bool is_valid = true;
            switch (change_field)
            {
                case "enabled":

                    service.Enabled = !service.Enabled;
                    break;

                default:
                    is_valid = false;
                    break;
            }
            if (is_valid)
            {
                var result= new  FluentValidation.Results.ValidationResult();
                dzService.SaveOrUpdate(service,out result);
            }

            context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+service.Enabled+"\"}");  };


    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}