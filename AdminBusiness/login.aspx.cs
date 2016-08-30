using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Dianzhu.BLL;

public partial class login : Dianzhu.Web.Common.BasePage // System.Web.UI.Page
{
    DZMembershipProvider bllMembership = Bootstrap.Container.Resolve<DZMembershipProvider>();
    protected void Page_Load(object sender, EventArgs e)
    {
        //PHSuit.Logging.GetLog(Dianzhu.Config.Config.GetAppSetting("LoggerName")).Debug("login page");
        Config.log.Debug("loging");

        BrowserCheck.CheckVersion();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        HttpCookie CookieErrorTime = Request.Cookies["errorTime"];

        if (CookieErrorTime == null)
        {
            HttpCookie errorTime = new HttpCookie("errorTime");
            errorTime.Value = "0";
            errorTime.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(errorTime);
        }

        string errorMsg;
        bool isValid = bllMembership.ValidateUser(tbxUserName.Text, tbxPassword.Text,out errorMsg);
        if (isValid)
        {
            bool rememberMe = savePass.Checked;
            FormsAuthentication.SetAuthCookie(tbxUserName.Text, rememberMe);
            if (Request.RawUrl.Contains("/m/"))
            {
                Response.Redirect("~/m/account/", true);
            }
            else
            {

                CookieErrorTime.Value = "0";
                Response.Cookies.Add(CookieErrorTime);
                Response.Redirect("~/business/", true);
            }
        }
        else
        {

            CookieErrorTime.Value = (int.Parse( CookieErrorTime.Value ) + 1).ToString();
            Response.Cookies.Add(CookieErrorTime);

            lblMsg.Text = errorMsg;
            lblMsg.CssClass = "lblMsg lblMsgShow";

            // PHSuit.Notification.Show(Page,"","登录失败",Request.RawUrl);
        }
    }
}