using Dianzhu.BLL;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DZOrder_Default : BasePage
{
    BLLServiceOrder bllServeiceOrder = new BLLServiceOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        int totalRecord;
        int currentPageIndex = 1;
        string paramPage = Request.Params["page"];
        if (!string.IsNullOrEmpty(paramPage))
        {
            currentPageIndex = int.Parse(paramPage);
        }
        rpOrderList.DataSource = bllServeiceOrder.GetListForBusiness(CurrentBusiness, currentPageIndex,pager.PageSize,out totalRecord);
     
        pager.RecordCount = Convert.ToInt32(totalRecord);
        rpOrderList.DataBind();
    }
    protected void rptOrderList_Command(object sender, RepeaterCommandEventArgs e)
    {
        Guid orderId = new Guid(e.CommandArgument.ToString());

        ServiceOrder order = bllServeiceOrder.GetOne(orderId);
        switch (e.CommandName.ToLower())
        {
            case "confirmorder":
                bllServeiceOrder.OrderFlow_BusinessConfirm(order);
                break;
        }
        BindData();
    }
}