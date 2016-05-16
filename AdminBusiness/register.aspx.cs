﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.Security;
using System.Text.RegularExpressions;
public partial class register : System.Web.UI.Page
{
    BLLBusiness bllBusiness = new BLLBusiness();
    DZMembershipProvider dz = new DZMembershipProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();
    }

    protected void regPsSubmit_OnClick(object sender, EventArgs e)
    {
        string userName = tbxUserName.Text.Trim();// tbx_MobilePhone.Text.Trim();

        string name = string.Empty;// tbxUserName.Text.Trim();
        string password = regPs.Text.Trim();
        string password2 = regPsConf.Text.Trim();

        string address = string.Empty;// tbxAddress.Text.Trim();
        string category = string.Empty;//  tbxCategory.Text.Trim();
        string cert = string.Empty;// tbxCertification.Text.Trim();
        string description = string.Empty;// tbxDescription.Text.Trim();
        double latitude = 0;// Convert.ToDouble(tbxLat.Text.Trim());
        double longtitude = 0;// Convert.ToDouble(tbxLon.Text.Trim());

        // Membership.CreateUser(tbxUserName.Text, regPs.Text);

       
          //  bllBusiness.Register(address, description, latitude, longtitude, name, userName, password);
        MembershipCreateStatus createStatus;
        DZMembership newUser =
           dz.CreateUser(userName, null, null, password, out createStatus, Dianzhu.Model.Enums.enum_UserType.business);
        //如果是电子邮箱,则发送验证邮件
        if (createStatus != MembershipCreateStatus.Success)
        {
            Response.Redirect("error.aspx?msg=" + createStatus);
        }
        bool sendSuccess = true;
        if(Regex.IsMatch(userName,@".+@.+\..+")){
            string verifyUrl = "http://" + Request.Url.Authority + "/verify.aspx";
            verifyUrl += "?userId=" + newUser.Id + "&verifyCode=" + newUser.RegisterValidateCode;
            if (!Request.IsLocal)
            {
                sendSuccess = dz.SendValidationMail(userName, verifyUrl);
            }
            
             
        }

       
        //PHSuit.Notification.Show(Page, "", "注册成功", "register_suc.aspx");
        FormsAuthentication.SetAuthCookie(userName, true);
        Response.Redirect("register_suc.aspx?send="+sendSuccess, true);
    }


}