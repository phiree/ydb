using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class Account_Default : System.Web.UI.Page
{
    BLLBusiness bllBusiness = new BLLBusiness();
    public string PageTile = "商家基本信息列表";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}