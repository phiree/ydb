<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
public class changepassword : IHttpHandler {

    DZMembershipProvider dzp = Bootstrap.Container.Resolve<DZMembershipProvider>();
    BLLStaff bllStaff = new BLLStaff();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        string change_field = context.Request["changed_field"];
        string strId = context.Request["id"];
        string strChangedValue = context.Request["changed_value"];
        Staff staff = bllStaff.GetOne(new Guid(strId));
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
           bllStaff.SaveOrUpdate(staff);
        }
        
        context.Response.Write("{\"result\":\""+is_valid+"\",\"msg\":\""+errMsg+"\",\"data\":\""+staff.IsAssigned+"\"}");
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}