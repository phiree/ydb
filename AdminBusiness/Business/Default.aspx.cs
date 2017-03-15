using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Application;
public partial class Business_Default : BasePage
{
   
    IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
    IServiceTypeService typeService = Bootstrap.Container.Resolve<IServiceTypeService>();
    IDZServiceService dzService= Bootstrap.Container.Resolve<IDZServiceService>();
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

        var businessList = businessService.GetBusinessListByOwner(CurrentUser.Id).Where(x=>x.Enabled);
        
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
          IList<ServiceType> serviceTypes= dzService.GetServiceTypeListByBusiness(b.Id);
            rpt.DataSource = serviceTypes;
            rpt.DataBind();
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        
        Business b = new Business();
       string Name = tbxName.Value;
        
       Guid OwnerId   = CurrentUser.Id;
        string email = string.Empty;
        string website = string.Empty;
        if (tbxWebSite.Value.Contains("@"))
        {
            email = tbxWebSite.Value;
        }
        else
        {
            website = tbxWebSite.Value;
        }
        string RawAddressFromMapAPI = hiAddrId.Value;
        string　Phone = tbxContactPhone.Value;
        string　Address = tbxAddress.Value;
        
       string Description = tbxDescription.Value;
       
       
       ActionResult<Business> result= businessService.Add(Name, Phone, email, OwnerId, string.Empty, string.Empty, RawAddressFromMapAPI, string.Empty, 0, 0);
       
        Response.Redirect("/business/detail.aspx?businessid="+result.ResultObject.Id);
    }
    protected void rptBusinessList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "delete")
        {
            string strBusinessId = e.CommandArgument.ToString();
            Guid businessId = new Guid(strBusinessId);
          businessService.Disable(businessId);
            
            BindBusinessList();
        }
    }
}