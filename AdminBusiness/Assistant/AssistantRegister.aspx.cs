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
using Ydb.Common.Application;
using Ydb.BusinessResource.Application;

public partial class Assistant_AssistantRegister : Dianzhu.Web.Common.BasePage
{

     IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();
    }

    protected void assRegPsSubmit_OnClick(object sender, EventArgs e)
    {
        string userName = tbxUserName.Text.Trim(); 
        string password = regPs.Text.Trim();
        string password2 = regPsConf.Text.Trim();
        RegisterResult result =memberService.RegisterCustomerService(userName,password,password2, Request.Url.Scheme+"://"+Request.Url.Authority );

        if (!result.RegisterSuccess  )
        {
            Response.Redirect("error.aspx?msg=" + result.RegisterErrMsg);
        }
        bool sendSuccess = true;
        FormsAuthentication.SetAuthCookie(userName, true);
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        Response.Redirect("/register_suc.aspx?send=" + sendSuccess + "&customerService=true" , true);
    }

}