using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Ydb.Finance.Application;
public partial class servicetype_Edit : BasePage
{
    private Guid TypeId=Guid.Empty;
    BLLServiceType bllServiceType = Bootstrap.Container.Resolve<Dianzhu.BLL.BLLServiceType>();
    IServiceTypePointService bllPoint = Bootstrap.Container.Resolve<IServiceTypePointService>();
    private bool IsNew {
        get {
            return TypeId == Guid.Empty;
        }
    }
    ServiceType CurrentServiceType = new ServiceType();
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            CurrentServiceType = bllServiceType.GetOne(new Guid(paramId));
            
        }
       
        if (!IsPostBack)
        {
          //  LoadInitData();
            LoadForm();
        }
    }
    private void LoadInitData()
    {
        rbl_Parent.DataSource = bllServiceType.GetAll();
         
        rbl_Parent.DataBind();
    }

    
    private void LoadForm()
    {
        tbxName.Text = CurrentServiceType.Name;
        lblParentName.Text = CurrentServiceType.Parent==null?"无":CurrentServiceType.Parent.Name;
        lblPoint.Text = bllPoint.GetPoint(CurrentServiceType.Id.ToString()).ToString("0.00");
    }
    private void UpdateForm()
    {
        CurrentServiceType.Name = tbxName.Text;
        string selectId = rbl_Parent.SelectedValue;
        if (!string.IsNullOrEmpty(selectId))
        {
            ServiceType parentType = bllServiceType.GetOne(new Guid(selectId));
            CurrentServiceType.Parent = parentType;
            CurrentServiceType.DeepLevel = parentType.DeepLevel + 1;
        }
    }

    private void Save()
    {
        
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();
        if (IsNew)
        {
            bllServiceType.Save(CurrentServiceType);
        }
        else
        { 
        bllServiceType.Update(CurrentServiceType);
        }
        PHSuit.Notification.Show(Page, "", "保存成功", "default.aspx");//Request.Url.AbsolutePath
    }
}