using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;
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
    //SharePoint功能暂时不需要
    IUserTypeSharePointService bllSharePoint = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
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
        ddlUserType.SelectedValue = sharePoint.UserType;
    }
 

    
    protected void btnSaveSharePoint_Click(object sender, EventArgs e)
    {
        try
        {
            var point = Convert.ToDecimal(tbxSharePoint.Text);
            UserTypeSharePointDto defaultSharePoint = bllSharePoint.GetSharePointInfo(ddlUserType.SelectedValue);
            if (defaultSharePoint == null)
            {
                bllSharePoint.Add(ddlUserType.SelectedValue, point);
            }
            else
            {
                bllSharePoint.Update(ddlUserType.SelectedValue, point);
            }
            lblMsg.Text = "保存成功";
            BindSharePoint();
        }
        catch (Exception ex)
        {
            PHSuit.Notification.Alert(Page, ex.Message);
        }
    }
}