using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!BrowserCheck.CheckVersion())
        {
            Response.Write("你的浏览器IE版本小于或等于7");
            Response.End();
        }


    }
}
