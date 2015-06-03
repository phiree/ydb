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
            tbxBusinessYears.Value = b.WorkingYears.ToString();
            string imgLicencePath=string.Empty, imgChargePersonPath=string.Empty;
            if (b.ChargePersonIdCard != null)
            {
                imgChargePersonPath = Config.BusinessImagePath + b.ChargePersonIdCard.ImageName;
            } if (b.BusinessLicence != null)
            {
                imgLicencePath = "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(b.BusinessLicence.ImageName) + "&width=50&height=50&tt=2";
               // imgLicencePath = Config.BusinessImagePath + b.BusinessLicence.ImageName;
            }
            imgLicence.Src = imgLicencePath;
            imgChargePerson.Src = imgChargePersonPath;
            rpt_show.DataSource = b.BusinessShows;
            rpt_show.DataBind();
        }
      
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        BLLBusiness bll = new BLLBusiness();
         b.Name = tbxName.Value;
        //b.Longitude =Convert.ToDouble(Longitude.Text);
        //b.Latitude = Convert.ToDouble(Latitude.Text);
        b.Description = tbxIntroduced.Value;
         b.Address = tbxAddress.Value;
         b.Phone = tbxContactPhone.Value;
         b.Email=tbxEmail.Value ;
         b.WorkingYears = int.Parse(tbxBusinessYears.Value);  

        //upload pictures
         if (fuBusinessLicence.PostedFile.ContentLength != 0)
         {
             string licenceImageName = b.Id + ImageType.Business_Licence.ToString() + Path.GetExtension(fuBusinessLicence.FileName);
             fuBusinessLicence.SaveAs(Server.MapPath(Config.BusinessImagePath + "/original/") + licenceImageName);
             BusinessImage biLicence = new BusinessImage
             {
                 ImageType = ImageType.Business_Licence,
                 UploadTime = DateTime.Now,
                 ImageName = licenceImageName,
                 Size = fuBusinessLicence.PostedFile.ContentLength
             };
             b.BusinessLicence = biLicence;
         }

         if (fuChargePerson.PostedFile.ContentLength != 0)
         {
             string chargeIdImageName = b.Id + ImageType.Business_Licence.ToString() + Path.GetExtension(fuChargePerson.FileName);
             fuChargePerson.SaveAs(Server.MapPath(Config.BusinessImagePath + "/original/") + chargeIdImageName);
             BusinessImage biChargeId = new BusinessImage
             {
                 ImageType = ImageType.Business_ChargePersonIdCard,
                 UploadTime = DateTime.Now,
                 ImageName = chargeIdImageName,
                 Size = fuChargePerson.PostedFile.ContentLength
             };
             b.BusinessLicence = biChargeId;
         }


        List<BusinessImage> showImages = new List<BusinessImage>();
        string showImage1 = b.Id + ImageType.Business_Show.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(fuShow1.FileName);
        string showImage2 = b.Id + ImageType.Business_Show.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(fuShow2.FileName);
        string showImage3 = b.Id + ImageType.Business_Show.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(fuShow3.FileName);
        if (fuShow1.PostedFile.ContentLength != 0)
        {
            fuShow1.SaveAs(Server.MapPath(Config.BusinessImagePath + "/original/") + showImage1);
            BusinessImage biShow1 = new BusinessImage
            {
                ImageType = ImageType.Business_Show,
                UploadTime = DateTime.Now,
                ImageName = showImage1,
                Size = fuShow1.PostedFile.ContentLength
            };
            b.BusinessImages.Add(biShow1);
        }
        if (fuShow2.PostedFile.ContentLength != 0)
        {
            fuShow2.SaveAs(Server.MapPath(Config.BusinessImagePath + "/original/") + showImage2);
            BusinessImage biShow2 = new BusinessImage
            {
                ImageType = ImageType.Business_Show,
                UploadTime = DateTime.Now,
                ImageName = showImage2,
                Size = fuShow2.PostedFile.ContentLength
            };
            b.BusinessImages.Add(biShow2);
        }
        if (fuShow3.PostedFile.ContentLength != 0)
        {
            fuShow3.SaveAs(Server.MapPath(Config.BusinessImagePath + "/original/") + showImage3);
            BusinessImage biShow3 = new BusinessImage
            {
                ImageType = ImageType.Business_Show,
                UploadTime = DateTime.Now,
                ImageName = showImage3,
                Size = fuShow3.PostedFile.ContentLength
            };
            b.BusinessImages.Add(biShow3);
        }
        
        bll.Updte(b);
        Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script language='javascript' defer>alert('提交成功！');window.document.location.href='" + Request.UrlReferrer.ToString() + "';</script>");
    
    }
}