using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.Security;
public partial class register : System.Web.UI.Page
{
    BLLBusiness bllBusiness = new BLLBusiness();
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void regPsSubmit_OnClick(object sender, EventArgs e)
    {
        //string mobilePhone = tbx_MobilePhone.Text.Trim();
        //string name = tbx_Name.Text.Trim();
        //string password=tbx_Password.Text.Trim();
        //string address=tbxAddress.Text.Trim();
        //string category = tbxCategory.Text.Trim();
        //string cert=tbxCertification.Text.Trim();
        //string description=tbxDescription.Text.Trim();
        //double latitude=Convert.ToDouble( tbxLat.Text.Trim());
        //double longtitude = Convert.ToDouble(tbxLon.Text.Trim());
        
       // Membership.CreateUser(
       // bllBusiness.Register(address, description, latitude, longtitude, name, mobilePhone, password);
    }
    
    
}