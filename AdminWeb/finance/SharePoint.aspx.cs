using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL.Finance;
using Dianzhu.Model.Finance;
public partial class finance_SharePoint : BasePage
{
    
    private string strItemId;
    protected void Page_Load(object sender, EventArgs e)
    {
         strItemId = Request["itemid"];
        BindSharePoint();
        if (!string.IsNullOrEmpty(strItemId))
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

    }
    IBLLSharePoint bllSharePoint = Bootstrap.Container.Resolve<IBLLSharePoint>();
    private void BindSharePoint()
    {
        var list = bllSharePoint.GetAll();
        rptPoints.DataSource = list;
        rptPoints.DataBind();
    }

    private void LoadData()
    {
        var sharePoint = bllSharePoint.GetOne(new Guid(strItemId));
        tbxSharePoint.Text = sharePoint.Point.ToString("0.00");
        ddlUserType.SelectedValue =((int)  sharePoint.UserType).ToString();
    }
 

    
    protected void btnSaveSharePoint_Click(object sender, EventArgs e)
    {
        var usertype = (Dianzhu.Model.Enums.enum_UserType)(Convert.ToInt32(ddlUserType.SelectedValue));
        var point = Convert.ToDecimal(tbxSharePoint.Text);
        DefaultSharePoint defaultSharePoint = new DefaultSharePoint(point,usertype);
        bllSharePoint.Save(defaultSharePoint);
        lblMsg.Text = "保存成功";
        BindSharePoint();
    }
}