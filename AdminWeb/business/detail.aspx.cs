using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class business_detail : System.Web.UI.Page
{
    Business b;
    BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
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
        if (b.IsApplyApproved)
        {
            btnApprove.Text = "已认证. 点击取消认证";
        }
        else
        {
            btnApprove.Text = "未认证通过. 点击通过认证.";
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        b.IsApplyApproved = !b.IsApplyApproved;
        bllBusiness.Update(b);
        PHSuit.Notification.Show(Page, "", "操作成功", Request.RawUrl);
    }
}