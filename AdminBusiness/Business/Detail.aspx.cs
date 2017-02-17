using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.BusinessResource.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;


public partial class Business_Detail :BasePage
{

    IServiceOrderService bllOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
 

    public string AllOrderCount;
    public string DoneOrderCount;
    public string ServiceCount;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
         int totalRecords = 999;
         BindLicense();
         BindShow();
         BindCharge();
          
         IDZServiceService  dzService = Bootstrap.Container.Resolve<IDZServiceService>();
         AllOrderCount =  bllOrder.GetAllOrdersForBusiness(CurrentBusiness.Id).Count.ToString();
         DoneOrderCount = bllOrder.GetAllCompleteOrdersForBusiness(CurrentBusiness.Id).Count.ToString();
         ServiceCount = dzService.GetServiceByBusiness(CurrentBusiness.Id, 1, 999, out totalRecords).Count.ToString();
        }
    }
    private void BindLicense()
    {
        rptImageLicense.DataSource = CurrentBusiness.BusinessLicenses;
        rptImageLicense.DataBind();
    }
    private void BindShow()
    {
        rptShow.DataSource = CurrentBusiness.BusinessShows;
        rptShow.DataBind();
    }
    private void BindCharge()
    {
        rptCharge.DataSource = CurrentBusiness.BusinessChargePersonIdCards;
        rptCharge.DataBind();
    }
}