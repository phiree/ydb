using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        bool isValid = Membership.ValidateUser(tbxUserName.Text, tbxPassword.Text);
        if (isValid)
        {
            bool rememberMe = savePass.Checked;
            FormsAuthentication.SetAuthCookie(tbxUserName.Text, rememberMe);
            if (Request.RawUrl.Contains("/m/"))
            {
                Response.Redirect("~/m/account/",true);
            }
            else {
                Response.Redirect("~/business/",true);
            }
        }
        else {
            lblMsg.Text = "用户名或密码有误";
            lblMsg.CssClass = "show";
           // PHSuit.Notification.Show(Page,"","登录失败",Request.RawUrl);
        }
    }

}