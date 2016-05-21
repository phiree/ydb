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
using Dianzhu.IDAL;
public partial class Business_Edit : BasePage
{
    public Business b = new Business();
    IDALBusiness dalBusiness = Installer.Container.Resolve<IDALBusiness>();
    
    BLLBusinessImage bllBi =Installer.Container.Resolve<BLLBusinessImage>();
    bool IsNew {get;set;}
    protected void Page_Load(object sender, EventArgs e)
    {
        string strBusinessId = Request["businessId"];
        Guid? businessId = null;
        if (!string.IsNullOrEmpty(strBusinessId))
        {
            businessId = new Guid(strBusinessId);
            IsNew = false;
            b = dalBusiness.FindById(businessId.Value);
            
        }
        else
        {
            Response.Redirect("/error.aspx?msg=访问地址有误,请检查.");
        }
        if (!IsPostBack)
        {
            LoadForm();
            //username.Text = ((BusinessUser)CurrentUser).UserName + "登陆了";
           
        }

    }
    private void LoadForm()
    {
        tbxName.Value = b.Name;
        //Longitude.Text = b.Longitude.ToString();
        //Latitude.Text = b.Latitude.ToString();
        tbxIntroduced.Value = b.Description;
        tbxAddress.Value = b.Address;
        tbxContactPhone.Value = b.Phone;
        tbxWebSite.Value = b.WebSite;
        hiAddrId.Value = b.RawAddressFromMapAPI;
        tbxBusinessYears.Value = b.WorkingYears.ToString();
        tbxContact.Value = b.Contact;
        selStaffAmount.Value = b.StaffAmount.ToString();
        selCardType.Value = ((int)b.ChargePersonIdCardType).ToString();
        tbxCardIdNo.Value = b.ChargePersonIdCardNo;
        //imgLicence.Src = imgLicencePath;
        //imgChargePerson.Src = imgChargePersonPath;
        BindShowImages();
        BindChargerIdCards();
        BindBusinessLicenses();
    }
    private void BindShowImages()
    {
        rpt_show.DataSource = b.BusinessShows;
        rpt_show.DataBind();
    }
    private void BindChargerIdCards()
    {
        rptChargePersonIdCards.DataSource = b.BusinessChargePersonIdCards;
        rptChargePersonIdCards.DataBind();
    }
    private void BindBusinessLicenses()
    {
        rptLicenseImages.DataSource = b.BusinessLicenses;
        rptLicenseImages.DataBind();
    }



    protected void rpt_show_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower().Trim() == "delete")
        {
            Guid imageId = new Guid(e.CommandArgument.ToString());
            bllBi.Delete(imageId);
             
           // Response.Redirect(Request.RawUrl);
            BindBusinessLicenses();
            BindChargerIdCards();
            BindShowImages();
        }
    }
    private void UpdateForm()
    {

        b.Name = tbxName.Value;
        //b.Longitude =Convert.ToDouble(Longitude.Text);
        //b.Latitude = Convert.ToDouble(Latitude.Text);
        b.Description = tbxIntroduced.Value;

        b.Phone = tbxContactPhone.Value;
        b.WebSite = tbxWebSite.Value;
        b.WorkingYears = int.Parse(tbxBusinessYears.Value);

        b.WorkingYears = int.Parse(tbxBusinessYears.Value);
        b.Contact = tbxContact.Value;
        b.StaffAmount = int.Parse(selStaffAmount.Value);
        b.ChargePersonIdCardType = (enum_IDCardType)int.Parse(selCardType.Value);
        b.ChargePersonIdCardNo = tbxCardIdNo.Value;
        BLLArea bllArea = Installer.Container.Resolve<BLLArea>();
        AddressParser addressParser = new AddressParser(hiAddrId.Value, bllArea);
        Area area;
        double latitude;
        double longtitude;
        addressParser.ParseAddress(out area, out latitude, out longtitude);
        b.RawAddressFromMapAPI = hiAddrId.Value;
        b.Latitude = latitude;
        b.Longitude = longtitude;
        b.AreaBelongTo = area;

        b.Address = tbxAddress.Value;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        //图片使用ajax上传,
        if (IsNew)
        {
            dalBusiness.Add(b);
        }
        else
        { dalBusiness.Update(b); }
        Response.Redirect("/business/detail.aspx?businessid=" + b.Id);
       // Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script language='javascript' defer>alert('提交成功！');window.document.location.href='" + Request.UrlReferrer.ToString() + "';</script>");

    }
}