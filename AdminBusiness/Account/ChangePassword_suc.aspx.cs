using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_ChangePassword : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        NeedBusiness = false;
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        NeedBusiness = false;
    }
}