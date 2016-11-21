using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.IDAL;
public partial class Business_Default : BasePage
{
    IDALBusiness dalBusiness = Bootstrap.Container.Resolve<IDALBusiness>();
    dzServiceService bllService = Bootstrap.Container.Resolve<dzServiceService>();
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

        var businessList = dalBusiness.GetBusinessListByOwner(CurrentUser.Id).Where(x=>x.Enabled);
        
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
        
        b.OwnerId   = CurrentUser.Id;
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
        dalBusiness.Add(b);
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        Response.Redirect("/business/detail.aspx?businessid="+b.Id);
    }
    protected void rptBusinessList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "delete")
        {
            string strBusinessId = e.CommandArgument.ToString();
            Guid businessId = new Guid(strBusinessId);
            Business b = dalBusiness.FindById(businessId);
            b.Enabled = false;
            dalBusiness.Update(b);
            BindBusinessList();
        }
    }
}