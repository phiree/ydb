<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
 
public class changepassword : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    
        
    BLLDZService bllService = Bootstrap.Container.Resolve<BLLDZService>();
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
        DZService service = bllService.GetOne(new Guid(strId));
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
            bllService.SaveOrUpdate(service,out result);
        }
       
        context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+service.Enabled+"\"}");  };

            NHibernateUnitOfWork.With.Transaction(ac);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}