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
            bool remeberMe = savePass.Checked;
            FormsAuthentication.SetAuthCookie(tbxUserName.Text, remeberMe);
            if (Request.RawUrl.Contains("/m/"))
            {
                Response.Redirect("~/m/account/",true);
            }
            else {
                Response.Redirect("~/business/",true);
            }
        }
        else {
            lblMsg.Text = "登录失败,用户名/密码有误,请重试.";
           // PHSuit.Notification.Show(Page,"","登录失败",Request.RawUrl);
        }
    }

}