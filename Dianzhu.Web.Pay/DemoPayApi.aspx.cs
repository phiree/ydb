using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DemoPayApi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbxPwd.Text))
        {
            Response.Redirect("paysuc.aspx");
        }
        else
        {
            Response.Redirect("payfail.aspx");
        }
    }
}