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
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    BLLServiceType bllServiceType = new BLLServiceType();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentServiceType = bllServiceType.GetOne(new Guid(Request.Params["typeid"]));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string pName = tbxPropertyName.Text.Trim();
        string pValues = tbxPropertyValues.Text.Trim();

        ServiceProperty property = bllServiceProperty.Create(pName, CurrentServiceType.Id, pValues);
       PHSuit.Notification.Show(Page, "", "保存成功", Request.Url.AbsolutePath);
    }
}