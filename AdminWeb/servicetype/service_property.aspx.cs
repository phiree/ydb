using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
/// <summary>
/// 分类属性定义.
/// </summary>
public partial class servicetype_service_property :BasePage
{
    public ServiceType CurrentServiceType = new ServiceType();
    ServiceProperty CurrentServiceProperty = new ServiceProperty();
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    BLLServiceType bllServiceType = new BLLServiceType();
    Guid propertyId = Guid.Empty;
    private bool IsNew {
        get { return propertyId == Guid.Empty; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramPropertyId = Request.Params["propertyId"];
        if (!string.IsNullOrEmpty(paramPropertyId))
        {
            propertyId = new Guid(paramPropertyId);
            CurrentServiceProperty = bllServiceProperty.GetOne(propertyId);
            CurrentServiceType = CurrentServiceProperty.ServiceType;

        }
        else {
            CurrentServiceType = bllServiceType.GetOne(new Guid(Request.Params["typeid"]));
        }
        if (!IsPostBack)
        {
            gvProperties.DataSource = CurrentServiceType.Properties;
            gvProperties.DataBind();
            LoadForm();
        }
        btnDelete.Visible = !IsNew;
    }
    private void LoadForm()
    {
        tbxPropertyName.Text = CurrentServiceProperty.Name;
        tbxPropertyValues.Text = CurrentServiceProperty.GetValuesStringFormat();
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string pName = tbxPropertyName.Text.Trim();
        string pValues = tbxPropertyValues.Text.Trim();
        
            ServiceProperty property = bllServiceProperty.SaveOrUpdate(propertyId, pName, CurrentServiceType.Id, pValues);
            PHSuit.Notification.Show(Page, "", "保存成功", "service_property.aspx?propertyid="+property.Id);
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bllServiceProperty.Delete(CurrentServiceProperty);
        PHSuit.Notification.Show(Page, "", "删除成功", "service_property.aspx?typeId="+CurrentServiceType.Id);
        
    }
}