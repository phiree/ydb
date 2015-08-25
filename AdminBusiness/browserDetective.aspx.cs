using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class browserDetective : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BrowserCheck.CheckVersion(false))
        {
            if (string.IsNullOrEmpty(Request.Params["returnUrl"]))
            {
                Response.Redirect(Server.UrlDecode(Request.Params["returnUrl"]));
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
    }
}