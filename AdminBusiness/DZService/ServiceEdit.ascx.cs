using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using PHSuit;
using FluentValidation.Results;
using System.Web.UI.HtmlControls;
public partial class DZService_ServiceEdit : System.Web.UI.UserControl
{

    public string merchantID {
        get {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

    private Guid ServiceId = Guid.Empty;
    BLLDZService bllService = new BLLDZService();
    BLLServiceType bllServiceType = new BLLServiceType();
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    BLLServicePropertyValue bllServicePropertyValue = new BLLServicePropertyValue();
    BLLDZTag bllTag = new BLLDZTag();
    public IList<ServiceProperty> TypeProperties = new List<ServiceProperty>();
    private bool IsNew { get { return ServiceId == Guid.Empty; } }

    public DZService CurrentService = new DZService();//当前的服务 对象.
    ServiceType ServiceType;
    public string otherServiceLocation;

    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["serviceid"];
       
        if (!string.IsNullOrEmpty(paramId))
        {
            ServiceId = new Guid(paramId);
            CurrentService = bllService.GetOne(ServiceId);
            ServiceType = CurrentService.ServiceType;
            dzTag.ServiceId = paramId;
            
        }
        
        if (!IsPostBack)
        {
            LoadInit();
            if (!IsNew)
            {
                LoadForm();
                tbxTag.Visible = false;
            }
            else
            {
                dzTag.Visible = false;
                
            }
        }

        //获取该店铺其他服务
        if (CurrentService.Id != new Guid())
        {
            int total;
            otherServiceLocation = "[";
            /*
            IList<DZService> areaCodes = bllService.GetOtherServiceByBusiness(CurrentService.Business.Id, CurrentService.Id, 0, 9999, out total);
            foreach (DZService a in areaCodes)
            {
                otherServiceLocation += a.BusinessAreaCode + ",";
            }
            otherServiceLocation = otherServiceLocation.TrimEnd(',');*/
            otherServiceLocation += "]";
        }
    }
    public void LoadInit()
    {
        LoadServicePeriod();
    }

    void rptProperties_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ServiceProperty p = e.Item.DataItem as ServiceProperty;
            RadioButtonList rbl = e.Item.FindControl("rblValues") as RadioButtonList;
            rbl.DataSource = p.Values;
            rbl.DataTextField = "PropertyValue";
            rbl.DataValueField = "Id";
            rbl.DataBind();
            ServicePropertyValue selectedValue = CurrentService.PropertyValues.SingleOrDefault(x => x.ServiceProperty == p);
            if (selectedValue != null)
            {
                rbl.SelectedValue = selectedValue.Id.ToString();
            }
        }
    }
    public void LoadForm()
    {
        tbxDescription.Text = CurrentService.Description;
        tbxName.Text = CurrentService.Name;
        if (!IsNew)
        {
            lblSelectedType.Text = CurrentService.ServiceType.ToString();
        }
        
        cbxEnable.Checked = CurrentService.Enabled;
        hiBusinessAreaCode.Value = CurrentService.BusinessAreaCode;
        tbxMinPrice.Text = CurrentService.MinPrice.ToString("#.#");
        tbxUnitPrice.Text = CurrentService.UnitPrice.ToString("#");
//        rblChargeUnit.SelectedValue =((int) CurrentService.ChargeUnit).ToString();
        rblChargeUnit.Value =((int) CurrentService.ChargeUnit).ToString();
        tbxOrderDelay.Text = CurrentService.OrderDelay.ToString();
//        tbxOrderDelay.Value = CurrentService.OrderDelay.ToString();
        tbxServiceTimeBegin.Text = CurrentService.ServiceTimeBegin.ToString();
        tbxServiceTimeEnd.Text = CurrentService.ServiceTimeEnd.ToString();
        tbxMaxOrdersPerDay.Text = CurrentService.MaxOrdersPerDay.ToString();
        tbxMaxOrdersPerHour.Text = CurrentService.MaxOrdersPerHour.ToString();
        rblServiceMode.SelectedValue=((int)CurrentService.ServiceMode).ToString();
        cblIsForBusiness.Checked = CurrentService.IsForBusiness;
        cbxIsCompensationAdvance.Checked = CurrentService.IsCompensationAdvance;
        cbxIsCertificated.Checked = CurrentService.IsCertificated;
        LoadPayType();
        //rblPayType.SelectedValue = ((int)CurrentService.PayType).ToString(); 
        hiTypeId.Value = CurrentService.ServiceType.Id.ToString();
        
    }
    private void LoadServicePeriod()
    {
        IList<ServiceOpenTime> opentimes = CurrentService.OpenTimes.OrderBy(x=>x.DayOfWeek).ToList();
        List<ServiceOpenTime> cc = new List<ServiceOpenTime>();
        ServiceOpenTime lastsot=null;
        foreach (ServiceOpenTime sot in opentimes)
        {
            if (sot.DayOfWeek == DayOfWeek.Sunday)
            {
                lastsot = sot;
                continue;
             }
            cc.Add(sot);
        }
        cc.Add(lastsot);
        rptOpenTimes.DataSource = cc;
        rptOpenTimes.ItemDataBound += new RepeaterItemEventHandler(rptOpenTimes_ItemDataBound);
        rptOpenTimes.DataBind();

    }

    void rptOpenTimes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item|| e.Item.ItemType== ListItemType.AlternatingItem)
        {
            ServiceOpenTime sot = e.Item.DataItem as ServiceOpenTime;
            if (sot == null) {
                Config.log.Error("错误:OpenTime为空. 原因:旧版本遗留数据.新版程序会在创建服务时自动初始化OpenTime");
                return; }
            HtmlInputCheckBox cbx = e.Item.FindControl("cbxChecked") as HtmlInputCheckBox;
            cbx.Checked = sot.Enabled;
            Repeater rpt = e.Item.FindControl("rptTimesOneDay") as Repeater;
            rpt.DataSource = sot.OpenTimeForDay.OrderBy(x=>x.PeriodStart).ToList();
            rpt.DataBind();
        }
    }
    private void LoadPayType()
    { 

        foreach(ListItem item in rblPayType.Items)
        {
            enum_PayType v =(enum_PayType) Convert.ToInt32( item.Value);
            if ((v | CurrentService.AllowedPayType) == CurrentService.AllowedPayType)
            {
                item.Selected = true;
            }
        }
    }
    public void UpdateForm()
    {
        CurrentService.Name = tbxName.Text;
        CurrentService.Description = tbxDescription.Text;
        CurrentService.Enabled = cbxEnable.Checked;
        CurrentService.Business = (((BasePage)this.Page).CurrentBusiness);
        Guid gid = new Guid(hiTypeId.Value);
        ServiceType = bllServiceType.GetOne(gid);
        CurrentService.ServiceType = ServiceType;
        IList<ServicePropertyValue> values = new List<ServicePropertyValue>();
       
        CurrentService.PropertyValues = values;
        CurrentService.BusinessAreaCode = hiBusinessAreaCode.Value;
        CurrentService.ChargeUnit = (enum_ChargeUnit)(Convert.ToInt32(rblChargeUnit.Value));
//        CurrentService.ChargeUnit = (enum_ChargeUnit)(Convert.ToInt32(rblChargeUnit.SelectedValue));
        CurrentService.IsCertificated = cbxIsCertificated.Checked;
        CurrentService.IsCompensationAdvance = cbxIsCompensationAdvance.Checked;
        CurrentService.IsForBusiness = cblIsForBusiness.Checked;
        CurrentService.MaxOrdersPerDay = Convert.ToInt32(tbxMaxOrdersPerDay.Text);
        CurrentService.MaxOrdersPerHour = Convert.ToInt32(tbxMaxOrdersPerHour.Text);
        CurrentService.MinPrice = Convert.ToDecimal(tbxMinPrice.Text);
        CurrentService.OrderDelay = Convert.ToInt32(tbxOrderDelay.Text);
//        CurrentService.OrderDelay = int.Parse(tbxOrderDelay.Value);
        CurrentService.ServiceMode =(enum_ServiceMode)(Convert.ToInt32( rblServiceMode.SelectedValue));
        CurrentService.ServiceTimeBegin = tbxServiceTimeBegin.Text;
        CurrentService.ServiceTimeEnd = tbxServiceTimeEnd.Text;
        CurrentService.UnitPrice =int.Parse(tbxUnitPrice.Text, System.Globalization.NumberStyles.AllowDecimalPoint);
        UpdatePayType();
        UpdateServiceTime();
        
     //   CurrentService.PayType=(PayType)(Convert.ToInt32(rblPayType.SelectedValue));
    }
    private void UpdateAfterSaved()
    {
        if(IsNew)
        {
            string[] tags = tbxTag.Text.Split(' ');
            foreach(string tag in tags)
            {
                if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                { continue; }
        bllTag.AddTag(tag, CurrentService.Id.ToString(), CurrentService.Business.Id.ToString(),
            CurrentService.ServiceType.Id.ToString());
            }
        }
    }
    private void UpdatePayType()
    {
        enum_PayType pt = enum_PayType.None;
        foreach (ListItem item in rblPayType.Items)
        {
            if (item.Selected)
            {
                pt |= (enum_PayType)(Convert.ToInt32(item.Value));
            }
        }
        CurrentService.AllowedPayType = pt;
    }

    private void UpdateServiceTime()
    {
        CurrentService.OpenTimes.Clear();
        foreach (RepeaterItem li in rptOpenTimes.Items)
        {
            HtmlInputHidden spDayOfWeek = li.FindControl("hiDayOfWeek") as HtmlInputHidden;
            DayOfWeek dow = (DayOfWeek)(Enum.Parse(typeof(DayOfWeek),spDayOfWeek.Value) );
             
            bool enabled = ((HtmlInputCheckBox)li.FindControl("cbxChecked")).Checked;
            ServiceOpenTime sot = new ServiceOpenTime();
            sot.DayOfWeek = dow;
            sot.Enabled = enabled;
            sot.OpenTimeForDay.Clear();
            Repeater rptTimesOneDay = li.FindControl("rptTimesOneDay") as Repeater;
            foreach (RepeaterItem rpi in rptTimesOneDay.Items)
            {
                HtmlInputControl tbxTimeBegin = rpi.FindControl("tbxTimeBegin") as HtmlInputControl;
                HtmlInputControl tbxTimeEnd = rpi.FindControl("tbxTimeEnd") as HtmlInputControl;
                ServiceOpenTimeForDay sotd = new ServiceOpenTimeForDay();
                sotd.TimeStart = tbxTimeBegin.Value;
                sotd.TimeEnd = tbxTimeEnd.Value;
                sot.OpenTimeForDay.Add(sotd);
            }
            CurrentService.OpenTimes.Add(sot);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        ValidationResult result;
        bllService.SaveOrUpdate(CurrentService, out result);
        
        if (result.IsValid)
        {
            UpdateAfterSaved();
            string redirectUrl=PHSuit.StringHelper.BuildUrlWithParameters(Request,"serviceid",CurrentService.Id.ToString());
             Response.Redirect("/dzservice/default.aspx?&businessid="+Request["businessid"]);
//             PHSuit.Notification.Alert(Page, "保存成功", redirectUrl);
          //   Response.Redirect(redirectUrl);//PHSuit.Notification.Show(Page, "", "保存成功", Request.RawUrl);
        }
        else
        {
            string err = string.Empty;
            foreach (ValidationFailure f in result.Errors)
            {
                err += f.ErrorMessage + ";";
            }
            PHSuit.Notification.Alert(Page, err);
            // PHSuit.Notification.Show(Page, "保存失败", err, Request.RawUrl);
        }
    }
   
}
