using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
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
            LoadForm();
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
        
    }
    public void UpdateForm()
    {
        CurrentService.Name = tbxName.Text;
        CurrentService.Description = tbxDescription.Text;

        CurrentService.Business = ((BusinessUser)((BasePage)this.Page).CurrentUser).BelongTo;
        CurrentService.ServiceType = ServiceType;
        IList<ServicePropertyValue> values = new List<ServicePropertyValue>();
       
        CurrentService.PropertyValues = values;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        bllService.SaveOrUpdate(CurrentService);
        PHSuit.Notification.Show(Page, "", "保存成功", Request.RawUrl);
    }
}
