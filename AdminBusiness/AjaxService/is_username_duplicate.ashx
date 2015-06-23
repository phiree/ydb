<%@ WebHandler Language="C#" Class="is_username_duplicate" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
using Dianzhu.BLL;
using Dianzhu.Model;
public class is_username_duplicate : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string result = "Y";
        string username = context.Request["username"];
      MembershipUser mu=  Membership.GetUser(username);
      if (mu == null)
      {
          result = "N";
      }
      context.Response.Write(result);
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}