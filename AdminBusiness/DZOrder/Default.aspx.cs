using Dianzhu.BLL;
using Dianzhu.Model;
using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DZOrder_Default : BasePage
{
    BLLServiceOrder bllServeiceOrder = new BLLServiceOrder();
    BLLPayment bllPayment = new BLLPayment();

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
    protected void rptOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ServiceOrder order = (ServiceOrder)e.Item.DataItem;
        string payLinkDepositAmount = order.BuildPayLink(Dianzhu.Config.Config.GetAppSetting("PayUrl"), Dianzhu.Model.Enums.enum_PayTarget.Deposit);
        HyperLink hlDepositAmount = e.Item.FindControl("PayDepositAmount") as HyperLink;
        hlDepositAmount.Text = "订金付款链接：";
        hlDepositAmount.NavigateUrl = payLinkDepositAmount;

        string payLinkFinalPayment = order.BuildPayLink(Dianzhu.Config.Config.GetAppSetting("PayUrl"), Dianzhu.Model.Enums.enum_PayTarget.FinalPayment);
        HyperLink hlFinalPayment = e.Item.FindControl("PayFinalPayment") as HyperLink;
        hlFinalPayment.Text = "尾款付款链接：";
        hlFinalPayment.NavigateUrl = payLinkFinalPayment;
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
            case "confirmprice":
                TextBox t = e.Item.FindControl("txtConfirmPrice") as TextBox;
                decimal price = t.Text == string.Empty ? 0 : Convert.ToDecimal(t.Text);
                bllServeiceOrder.OrderFlow_BusinessNegotiate(order, price);
                break;
            case "confirmpricecustomer":
                bllServeiceOrder.OrderFlow_CustomerConfirmNegotiate(order);
                break;
            case "isendorder":
                bllServeiceOrder.OrderFlow_BusinessFinish(order);
                break;
            case "isendordercustomer":
                bllServeiceOrder.OrderFlow_CustomerFinish(order);
                break;
            default:
                return;
        }
        BindData();
    }
}