<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
public class changepassword : IHttpHandler {

    DZMembershipProvider dzp = Bootstrap.Container.Resolve<DZMembershipProvider>();
    BLLDZService bllService = new BLLDZService();
    public void ProcessRequest (HttpContext context) {
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
        
        context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+service.Enabled+"\"}");
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}