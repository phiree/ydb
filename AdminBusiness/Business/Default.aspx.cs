using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class Business_Default :BasePage
{
    BLLBusiness bllBusiness = new BLLBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindBusinessList();
    }
    protected void BindBusinessList()
    {

        var businessList = bllBusiness.GetBusinessListByOwner(CurrentUser.Id);
        rptBusinessList.DataSource = businessList;
        rptBusinessList.DataBind();

    }
}