using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class Account_Default : BasePage
{

    public string PageTile = "商家基本信息";

    protected void Page_Load(object sender, EventArgs e)
    {
        Business b = ((BusinessUser)CurrentUser).BelongTo;
        username.Text = ((BusinessUser)CurrentUser).UserName+"登陆了";
        businessName.Text = b.Name;
        Longitude.Text = b.Longitude.ToString();
        Latitude.Text = b.Latitude.ToString();
        Description.Text = b.Description;
        Address.Text = b.Address;

    }
}