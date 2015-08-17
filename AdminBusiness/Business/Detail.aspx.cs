using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Business_Detail :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
         BindLicense();
         BindShow();
         BindCharge();
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