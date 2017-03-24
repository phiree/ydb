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
 
public partial class login : System.Web.UI.Page
{
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        ValidateResult validateResult = memberService.Login(tbxUserName.Text, tbxPwd.Text);
        
   
        if (validateResult.IsValidated)
        {
            
            if (validateResult.ValidatedMember.UserType != "admin")
            {
                lblMsg.Text = "登录失败:not admin";
                return;

            }
            var identity = new ClaimsIdentity(new[]
                            {
                                    new Claim(ClaimTypes.Name, validateResult.ValidatedMember.DisplayName),
                                    new Claim(ClaimTypes.Email, validateResult.ValidatedMember.Email),
                                    new Claim(ClaimTypes.NameIdentifier,validateResult.ValidatedMember.Id.ToString()),
                                    new Claim(ClaimTypes.MobilePhone,validateResult.ValidatedMember.Phone),
                                    new Claim(ClaimTypes.StateOrProvince,validateResult.ValidatedMember.AreaId),

                                },
                            "ApplicationCookie");

            var ctx = HttpContext.Current.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignIn(identity);
          

            string returnurl = Request["returnurl"];
            if (returnurl == null) { returnurl = "/"; }
            Response.Redirect(returnurl);
        }
        else
        {
            lblMsg.Text = "登录失败: 验证无效";
        }
    }
}