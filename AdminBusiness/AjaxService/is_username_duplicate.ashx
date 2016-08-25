<%@ WebHandler Language="C#" Class="is_username_duplicate" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;


public class is_username_duplicate : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        //权限判断
        if (context.Session["UserName"]==null)
        {
            context.Response.Write("{\"result\":\""+false+"\",\"msg\":\"unlogin\"}");
            return;
        }

        Action ac = () => { 
        context.Response.ContentType = "text/plain";
        string result = "false";
        string username = context.Request["tbxUserName"];
        MembershipUser mu=  Membership.GetUser(username);
        if (mu == null)
        {
            result = "true";
        }
        context.Response.Write(result);
            };
            NHibernateUnitOfWork.With.Transaction(ac);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}