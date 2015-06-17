<%@ WebHandler Language="C#" Class="changepassword" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
public class changepassword : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        string oldpassword = context.Request["op"];
        string newpassword = context.Request["np"];
        string newpassword2 = context.Request["np2"];
      
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}