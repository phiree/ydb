using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Business_Detail :BasePage
{
    Dianzhu.BLL.IBLLServiceOrder bllOrder = Installer.Container.Resolve<Dianzhu.BLL.IBLLServiceOrder>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
         int totalRecords = 999;
         BindLicense();
         BindShow();
         BindCharge();
      //   Dianzhu.BLL.BLLServiceOrder bllOrder= new Dianzhu.BLL.BLLServiceOrder();
         Dianzhu.BLL.BLLDZService bllService = new Dianzhu.BLL.BLLDZService();
         liAllOrderCount.Text=  bllOrder.GetAllOrdersForBusiness(CurrentBusiness.Id).Count.ToString();
         liDoneOrderCount.Text = bllOrder.GetAllCompleteOrdersForBusiness(CurrentBusiness.Id).Count.ToString();
         liServiceCount.Text = bllService.GetServiceByBusiness(CurrentBusiness.Id, 1, 999, out totalRecords).Count.ToString();
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