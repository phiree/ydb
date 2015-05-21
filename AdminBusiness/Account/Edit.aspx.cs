using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;

public partial class Account_Edit : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Business b = ((BusinessUser)CurrentUser).BelongTo;
            username.Text = ((BusinessUser)CurrentUser).UserName + "登陆了";
            businessName.Text = b.Name;
            Longitude.Text = b.Longitude.ToString();
            Latitude.Text = b.Latitude.ToString();
            Description.Text = b.Description;
            Address.Text = b.Address;
        }
      
    }

    protected void dataSub(object sender, EventArgs e)
    {
        Business b = ((BusinessUser)CurrentUser).BelongTo;
        BLLBusiness bll = new BLLBusiness();
        b.Name = businessName.Text;
        b.Longitude =Convert.ToDouble(Longitude.Text);
        b.Latitude = Convert.ToDouble(Latitude.Text);
        b.Description = Description.Text;
        b.Address = Address.Text;

        bll.Updte(b);
        Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script language='javascript' defer>alert('提交成功！');window.document.location.href='" + Request.UrlReferrer.ToString() + "';</script>");
    
    }
}