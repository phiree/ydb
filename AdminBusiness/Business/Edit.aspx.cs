using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.UI.HtmlControls;
using Ydb.Common;
using System.IO;
using Dianzhu.IDAL;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Application;
public partial class Business_Edit : BasePage
{
    public Business b = new Business();
    public VMBusiness vmBusiness = new VMBusiness();
    
    IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
    IBusinessImageService imageService= Bootstrap.Container.Resolve<IBusinessImageService>();
   
    bool IsNew {get;set;}
    protected void Page_Load(object sender, EventArgs e)
    {
        string strBusinessId = Request["businessId"];
        Guid? businessId = null;
        if (!string.IsNullOrEmpty(strBusinessId))
        {
            businessId = new Guid(strBusinessId);
            IsNew = false;
            b = businessService.GetOne(businessId.Value);
            vmBusiness = new VMBusiness().Adap(b);
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
            if (!b.BusinessImages.Any(x => x.Id == imageId))
            { return; }
            var removed = b.BusinessImages.Single(x => x.Id == imageId);
            b.BusinessImages.Remove(removed);

            //NHibernateUnitOfWork.UnitOfWork.Current.Refresh(b);
        //    bllBi.Delete(imageId)
             
           // Response.Redirect(Request.RawUrl);
            BindBusinessLicenses();
            BindChargerIdCards();
            BindShowImages();
        }
    }
    private void UpdateForm()
    {
       
        vmBusiness.Name = tbxName.Value;
        //vmBusiness.Longitude =Convert.ToDouble(Longitude.Text);
        //vmBusiness.Latitude = Convert.ToDouble(Latitude.Text);
        vmBusiness.Description = tbxIntroduced.Value;

        vmBusiness.Phone = tbxContactPhone.Value;
        vmBusiness.WebSite = tbxWebSite.Value;
        vmBusiness.WorkingYears = int.Parse(tbxBusinessYears.Value);

        vmBusiness.WorkingYears = int.Parse(tbxBusinessYears.Value);
        vmBusiness.Contact = tbxContact.Value;
        vmBusiness.StaffAmount = int.Parse(selStaffAmount.Value);
        vmBusiness.ChargePersonIdCardType = (enum_IDCardType)int.Parse(selCardType.Value);
        vmBusiness.ChargePersonIdCardNo = tbxCardIdNo.Value;
       // BLLArea bllArea = Bootstrap.Container.Resolve<BLLArea>();
        IAreaService areaService = Bootstrap.Container.Resolve<IAreaService>();
        AddressParser addressParser = new AddressParser(hiAddrId.Value, areaService);
        Area area;
        double latitude;
        double longtitude;
        addressParser.ParseAddress(out area, out latitude, out longtitude);
        vmBusiness.RawAddressFromMapApi = hiAddrId.Value;
        vmBusiness.Latitude = latitude;
        vmBusiness.Longtitude = longtitude;
       //todo: 應該在領域內解析
        // vmBusiness.AreaBelongTo = area;

        vmBusiness.Address = tbxAddress.Value;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        //图片使用ajax上传,
        if (IsNew)
        {
     ActionResult<Business> addResult=       businessService.Add(vmBusiness.Name, vmBusiness.Phone, vmBusiness.Email, CurrentUser.Id,
                vmBusiness.Latitude.ToString(), vmBusiness.Longtitude.ToString(), vmBusiness.RawAddressFromMapApi,
                vmBusiness.Contact, vmBusiness.WorkingYears, vmBusiness.StaffAmount);
         
        }
        else
        {
             
            businessService.Update(b); }
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        Response.Redirect("/business/detail.aspx?businessid=" + b.Id);
       // Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script language='javascript' defer>alert('提交成功！');window.document.location.href='" + Request.UrlReferrer.ToString() + "';</script>");

    }
}