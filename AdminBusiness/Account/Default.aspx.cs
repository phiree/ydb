using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.UI.HtmlControls;
using Dianzhu.Model.Enums;
using System.IO;
public partial class Account_Edit : BasePage
{
    public Business b = new Business();
    BLLBusinessImage bllBi = new BLLBusinessImage();
    protected void Page_Load(object sender, EventArgs e)
    {
        b = ((BusinessUser)CurrentUser).BelongTo;
        if (!IsPostBack)
        {
             
            //username.Text = ((BusinessUser)CurrentUser).UserName + "登陆了";
            tbxName.Value = b.Name;
            //Longitude.Text = b.Longitude.ToString();
            //Latitude.Text = b.Latitude.ToString();
            tbxIntroduced.Value = b.Description;
            tbxAddress.Value = b.Address;
            tbxContactPhone.Value = b.Phone;
            tbxEmail.Value = b.Email;
            hiAddrId.Value = b.RawAddressFromMapAPI;
            tbxBusinessYears.Value = b.WorkingYears.ToString();
            tbxContact.Value = b.Contact;
            selStaffAmount.Value = b.StaffAmount.ToString();
            selCardType.Value = ((int)b.ChargePersonIdCardType).ToString();
            tbxCardIdNo.Value = b.ChargePersonIdCardNo; 

            string imgLicencePath=string.Empty, imgChargePersonPath=string.Empty;
            if (b.ChargePersonIdCard .Id!=Guid.Empty)
            {
                imgChargePerson.ImageUrl = "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(b.ChargePersonIdCard.ImageName) + "&width=90&height=90&tt=2";
                imgChargePerson.NavigateUrl = Config.BusinessImagePath + "original/" + b.ChargePersonIdCard.ImageName;
            }
            else
            {
                imgChargePerson.Visible = false;
            }
            if (b.BusinessLicence.Id != Guid.Empty)
            {
                imgBusinessImage.ImageUrl = "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(b.BusinessLicence.ImageName) + "&width=90&height=90&tt=2";
                //imgLicencePath = "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(b.BusinessLicence.ImageName) + "&width=90&height=90&tt=2";
                imgBusinessImage.NavigateUrl = Config.BusinessImagePath + "original/" + b.BusinessLicence.ImageName;
                // imgLicencePath = Config.BusinessImagePath + b.BusinessLicence.ImageName;
            }
            else {
                imgBusinessImage.Visible = false;
            }
            
            
            //imgLicence.Src = imgLicencePath;
            //imgChargePerson.Src = imgChargePersonPath;

            BindShowImages();
        }
      
    }
    private void BindShowImages()
    {
      
        rpt_show.DataSource = b.BusinessShows;
         rpt_show.DataBind();
    }

   protected void rpt_show_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower().Trim() == "delete")
        {
            Guid imageId = new Guid(e.CommandArgument.ToString());
            bllBi.Delete(imageId);
            PHSuit.Notification.Show(Page, "", "删除成功", Request.RawUrl);
            BindShowImages();
        }
    }

   protected void btnSave_Click(object sender, EventArgs e)
    {
        
        BLLBusiness bll = new BLLBusiness();
         b.Name = tbxName.Value;
        //b.Longitude =Convert.ToDouble(Longitude.Text);
        //b.Latitude = Convert.ToDouble(Latitude.Text);
        b.Description = tbxIntroduced.Value;
       
         b.Phone = tbxContactPhone.Value;
         b.Email=tbxEmail.Value ;
         b.WorkingYears = int.Parse(tbxBusinessYears.Value);
       
         b.WorkingYears =int.Parse(tbxBusinessYears.Value);
         b.Contact = tbxContact.Value;
         b.StaffAmount = int.Parse(selStaffAmount.Value);
         b.ChargePersonIdCardType = (enum_IDCardType)int.Parse(selCardType.Value);
         b.ChargePersonIdCardNo = tbxCardIdNo.Value;

         AddressParser addressParser = new AddressParser(hiAddrId.Value);
       Area area;
       double latitude;
       double longtitude;
       addressParser.ParseAddress(out area,out latitude,out longtitude);
       CurrentBusiness.RawAddressFromMapAPI = hiAddrId.Value;
       CurrentBusiness.Latitude = latitude;
       CurrentBusiness.Longitude = longtitude;
       CurrentBusiness.AreaBelongTo = area;
       
       b.Address = tbxAddress.Value;
        //图片使用ajax上传,
          
        
        bll.Updte(b);
        Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script language='javascript' defer>alert('提交成功！');window.document.location.href='" + Request.UrlReferrer.ToString() + "';</script>");
    
    }
}