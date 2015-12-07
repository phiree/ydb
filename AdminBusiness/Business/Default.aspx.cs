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
    BLLDZService bllService = new BLLDZService();
    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();
         

        hiCreateID.Value = string.IsNullOrEmpty(CurrentUser.Phone) ? "false" : "true";
           
       
        if (!IsPostBack)
        {
            BindBusinessList();
        }

    }
    protected void BindBusinessList()
    {

        var businessList = bllBusiness.GetBusinessListByOwner(CurrentUser.Id).Where(x=>x.Enabled);
        
        rptBusinessList.DataSource = businessList;
        // rptBusinessList.ItemCommand+=new RepeaterCommandEventHandler(rptBusinessList_ItemCommand);
        rptBusinessList.ItemDataBound += RptBusinessList_ItemDataBound;
        rptBusinessList.DataBind();

    }

    private void RptBusinessList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if(e.Item.ItemType== ListItemType.Item|| e.Item.ItemType== ListItemType.AlternatingItem)
        {
            Business b = (Business)e.Item.DataItem;
            Repeater rpt = e.Item.FindControl("rptServiceType") as Repeater;
          IList<ServiceType> serviceTypes=  bllService.GetServiceTypeListByBusiness(b.Id);
            rpt.DataSource = serviceTypes;
            rpt.DataBind();
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        
        Business b = new Business();
        b.Name = tbxName.Value;
        
        b.Owner = CurrentUser;
        if (tbxWebSite.Value.Contains("@"))
        {
            b.Email = tbxWebSite.Value;
        }
        else
        {
            b.WebSite = tbxWebSite.Value;
        }
        b.RawAddressFromMapAPI = hiAddrId.Value;
        b.Phone = tbxContactPhone.Value;
        b.Address = tbxAddress.Value;
        
        b.Description = tbxDescription.Value;
        b.CreatedTime = DateTime.Now;
        bllBusiness.SaveOrUpdate(b);

        Response.Redirect("/business/detail.aspx?businessid="+b.Id);
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