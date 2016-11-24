using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

public partial class business_detail : BasePage
{
    Business b;
    IBusinessService bllBusiness = Bootstrap.Container.Resolve<IBusinessService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
          
            b = bllBusiness.GetOne(new Guid(paramId));
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        DetailsView1.DataSource = new List<Business> { b };
        DetailsView1.DataBind();
        BindUI(b);
    }
    private void BindUI(Business b)
    {
        btnApprove.Text = b.IsApplyApproved ? "已认证. 点击取消认证" : "未认证通过. 点击通过认证.";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if(b.IsApplyApproved)
            bllBusiness.ApprovedDisable(b.Id);
        else
        {
            bllBusiness.ApprovedEnable(b.Id);
        }
        
        PHSuit.Notification.Show(Page, "", "操作成功", Request.RawUrl);
    }
}