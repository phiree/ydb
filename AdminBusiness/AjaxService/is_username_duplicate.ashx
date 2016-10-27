<%@ WebHandler Language="C#" Class="is_username_duplicate" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
public class is_username_duplicate : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {

        context.Response.ContentType = "text/plain";
        string result = "false";
        string username = context.Request["tbxUserName"];
        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
      //  MembershipUser mu=  Membership.GetUser(username);
         MemberDto memberDto=   memberService.GetUserByName(username);
        if (memberDto == null)
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