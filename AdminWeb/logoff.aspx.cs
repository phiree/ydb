using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

public partial class loginoff : System.Web.UI.Page
{
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        var ctx = HttpContext.Current.GetOwinContext();
        var authManager = ctx.Authentication;

        authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
      
    }

     
}