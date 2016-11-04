using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
public partial class register : BasePage
{
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        System.Web.Security.MembershipCreateStatus createStatus;
     RegisterResult result=   memberService.RegisterMember(tbxUserName.Text,  tbxPwd.Text,tbxPwd.Text, rblUserType.Text,Request.Url.Scheme+"://"+Request.Url.Authority);
        lblMsg.Text = result.RegisterSuccess.ToString();
    }
}