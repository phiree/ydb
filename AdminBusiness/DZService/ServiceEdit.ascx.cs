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
public partial class DZService_ServiceEdit : System.Web.UI.UserControl
{

    private Guid ServiceId = Guid.Empty;
    BLLDZService bllService = new BLLDZService();
    BLLServiceType bllServiceType = new BLLServiceType();
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    BLLServicePropertyValue bllServicePropertyValue = new BLLServicePropertyValue();
    public IList<ServiceProperty> TypeProperties = new List<ServiceProperty>();
    private bool IsNew { get { return ServiceId == Guid.Empty; } }

    public DZService CurrentService = new DZService();//当前的服务 对象.
    ServiceType _servicetype;
    public ServiceType ServiceType
    {
        get
        {
            if (_servicetype == null)
            {
                Guid typeId = new Guid(hiTypeId.Value);
                _servicetype = bllServiceType.GetOne(typeId);
            }
            return _servicetype;
        }
        set { _servicetype = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            ServiceId = new Guid(paramId);
            CurrentService = bllService.GetOne(ServiceId);
            ServiceType = CurrentService.ServiceType;
        }
        
        if (!IsPostBack)
        {
            LoadInit();
            if (!IsNew)
            {
                LoadForm();
            }
        }
    }
    public void LoadInit()
    {
        
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
        hiBusinessAreaCode.Value = CurrentService.BusinessAreaCode;
        tbxMinPrice.Text = CurrentService.MinPrice.ToString();
        tbxUnitPrice.Text = CurrentService.UnitPrice.ToString();
        rblChargeUnit.SelectedValue =((int) CurrentService.ChargeUnit).ToString();
        tbxOrderDelay.Text = CurrentService.OrderDelay.ToString();
        tbxServiceTimeBegin.Text = CurrentService.ServiceTimeBegin.ToString();
        tbxServiceTimeEnd.Text = CurrentService.ServiceTimeEnd.ToString();
        tbxMaxOrdersPerDay.Text = CurrentService.MaxOrdersPerDay.ToString();
        tbxMaxOrdersPerHour.Text = CurrentService.MaxOrdersPerHour.ToString();
        rblServiceMode.SelectedValue=((int)CurrentService.ServiceMode).ToString();
        cblIsForBusiness.Checked = CurrentService.IsForBusiness;
        cbxIsCompensationAdvance.Checked = CurrentService.IsCompensationAdvance;
        cbxIsCertificated.Checked = CurrentService.IsCertificated;
    }
    public void UpdateForm()
    {
        CurrentService.Name = tbxName.Text;
        CurrentService.Description = tbxDescription.Text;

        CurrentService.Business = ((BusinessUser)((BasePage)this.Page).CurrentUser).BelongTo;
        CurrentService.ServiceType = ServiceType;
        IList<ServicePropertyValue> values = new List<ServicePropertyValue>();
       
        CurrentService.PropertyValues = values;
        CurrentService.BusinessAreaCode = hiBusinessAreaCode.Value;
        CurrentService.ChargeUnit = (ChargeUnit)(Convert.ToInt16(rblChargeUnit.SelectedValue));
        CurrentService.IsCertificated = cbxIsCertificated.Checked;
        CurrentService.IsCompensationAdvance = cbxIsCompensationAdvance.Checked;
        CurrentService.IsForBusiness = cblIsForBusiness.Checked;
        CurrentService.MaxOrdersPerDay = Convert.ToInt32(tbxMaxOrdersPerDay.Text);
        CurrentService.MaxOrdersPerHour = Convert.ToInt32(tbxMaxOrdersPerHour.Text);
        CurrentService.MinPrice = Convert.ToDecimal(tbxMinPrice.Text);
        CurrentService.OrderDelay = Convert.ToInt32(tbxOrderDelay.Text);
        CurrentService.ServiceMode =(ServiceMode)(Convert.ToInt32( rblServiceMode.SelectedValue));
        CurrentService.ServiceTimeBegin = tbxServiceTimeBegin.Text;
        CurrentService.ServiceTimeEnd = tbxServiceTimeEnd.Text;
        CurrentService.UnitPrice = Convert.ToInt32(tbxUnitPrice.Text);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        bllService.SaveOrUpdate(CurrentService);
        PHSuit.Notification.Show(Page, "", "保存成功", Request.RawUrl);
    }
}
