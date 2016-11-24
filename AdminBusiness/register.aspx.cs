using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.Security;
using System.Text.RegularExpressions;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;

public partial class register : Dianzhu.Web.Common.BasePage//System.Web.UI.Page
{
     IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();
    }

    protected void regPsSubmit_OnClick(object sender, EventArgs e)
    {
        string userName = tbxUserName.Text.Trim();// tbx_MobilePhone.Text.Trim();

 
        string password = regPs.Text.Trim();
        string password2 = regPsConf.Text.Trim();
 
        RegisterResult registerResult =
         dzMembershipService.RegisterBusinessUser(userName, password, password2,Request.Url.Scheme+"://"+ Request.Url.Authority);

        if (!registerResult.RegisterSuccess)
        {
            Response.Redirect("error.aspx?msg=" + registerResult.RegisterErrMsg);
        }
        FormsAuthentication.SetAuthCookie(userName, true);
        Response.Redirect("register_suc.aspx?send="+registerResult.SendEmailSuccess, true);
    }


}