using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        BLLDZService bllService = new BLLDZService();
        gvServiceList.DataSource = bllService.GetAll();
        gvServiceList.DataBind();
    }
    protected void gvServiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex=Convert.ToInt32(e.CommandArgument);
        string strServiceId = gvServiceList.DataKeys[rowIndex].Value.ToString();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        bllServiceOrder.CreateOrder(CurrentUser.Id, new Guid(strServiceId),string.Empty,1);
        lblResult.Text = "成功预订" + strServiceId;
    }
}