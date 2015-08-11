using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class Business_Default : BasePage
{
    BLLBusiness bllBusiness = new BLLBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBusinessList();
        }
    }
    protected void BindBusinessList()
    {

        var businessList = bllBusiness.GetBusinessListByOwner(CurrentUser.Id).Where(x=>x.Enabled);
        
        rptBusinessList.DataSource = businessList;
      //  rptBusinessList.ItemCommand+=new RepeaterCommandEventHandler(rptBusinessList_ItemCommand);
        rptBusinessList.DataBind();

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Business b = new Business();
        b.Name = tbxName.Value;
        b.Owner = CurrentUser;
        b.Description = tbxDescription.Value;
        bllBusiness.SaveOrUpdate(b);

        Response.Redirect("/business/createsuc.aspx?businessid="+b.Id);
    }
    protected void rptBusinessList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "delete")
        {
            string strBusinessId = e.CommandArgument.ToString();
            Guid businessId = new Guid(strBusinessId);
            Business b = bllBusiness.GetOne(businessId);
            b.Enabled = false;
            bllBusiness.SaveOrUpdate(b);
            BindBusinessList();
        }
    }
}