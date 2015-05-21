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
    Business businessModle = new Business();
    public string PageTile = "商家基本信息列表";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            data_rp.DataSource = bllBusiness.GetAll();
            data_rp.DataBind();
        
        }

    }

    protected void delbt_Command(object sender, CommandEventArgs e)
    {

        int id = int.Parse(e.CommandArgument.ToString());

        //NewsSql.Delete(id);
        Response.Redirect(Request.UrlReferrer.ToString());

    }
}