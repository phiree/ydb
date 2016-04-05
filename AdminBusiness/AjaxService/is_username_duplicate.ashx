<%@ WebHandler Language="C#" Class="is_username_duplicate" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
 
 
public class is_username_duplicate : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string result = "false";
        string username = context.Request["tbxUserName"];
      MembershipUser mu=  Membership.GetUser(username);
      if (mu == null)
      {
          result = "true";
      }
      context.Response.Write(result);
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}