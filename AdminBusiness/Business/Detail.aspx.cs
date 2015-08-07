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
        }
    }
    private void BindLicense()
    {
        rptImageLicense.DataSource = CurrentBusiness.BusinessLicenses;
        rptImageLicense.DataBind();
    }
}