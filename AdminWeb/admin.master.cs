using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("login.aspx?returnurl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri), true);
        }
        else
        { 
        base.OnInit(e);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
