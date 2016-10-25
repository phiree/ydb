using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected override void OnInit(EventArgs e)
    {
      
        Response.Redirect("/Business/", true);
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}