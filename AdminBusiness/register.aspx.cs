﻿using System;
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
        string mobilePhone = tbxUserName.Text.Trim();// tbx_MobilePhone.Text.Trim();
        string name = string.Empty;// tbxUserName.Text.Trim();
         string password=regPs.Text.Trim();
         string address = string.Empty;// tbxAddress.Text.Trim();
         string category = string.Empty;//  tbxCategory.Text.Trim();
         string cert = string.Empty;// tbxCertification.Text.Trim();
         string description = string.Empty;// tbxDescription.Text.Trim();
         double latitude = 0;// Convert.ToDouble(tbxLat.Text.Trim());
         double longtitude = 0;// Convert.ToDouble(tbxLon.Text.Trim());

       // Membership.CreateUser(tbxUserName.Text, regPs.Text);
       
        bllBusiness.Register(address, description, latitude, longtitude, name, mobilePhone, password);
        PHSuit.Notification.Show(Page, "", "注册成功", "register_suc.aspx");
    }
    
    
}