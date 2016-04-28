using Dianzhu.BLL;
using Dianzhu.Model;
using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DZOrder_Detail : System.Web.UI.Page
{
    BLLServiceOrder bllServeiceOrder = new BLLServiceOrder();
    BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = new BLLServiceOrderStateChangeHis();
    BLLPayment bllPayment = new BLLPayment();
    public ServiceOrder CurrentOrder;

    public string merchantID
    {
        get
        {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        CurrentOrder = bllServeiceOrder.GetOne(new Guid(Request["orderId"]));
        BingData();
        BindDoneStatusData();
    }

    //    private void BindData()
    //        {
    //            int totalRecord;
    //            int currentPageIndex = 1;
    //            string paramPage = Request.Params["page"];
    //            if (!string.IsNullOrEmpty(paramPage))
    //            {
    //                currentPageIndex = int.Parse(paramPage);
    //            }
    //            rpOrderList.DataSource = bllServeiceOrder.GetListForBusiness(CurrentBusiness, currentPageIndex,pager.PageSize,out totalRecord).OrderByDescending(x=>x.LatestOrderUpdated);
    //
    //            pager.RecordCount = Convert.ToInt32(totalRecord);
    //            rpOrderList.DataBind();
    //        }

    protected void BindDoneStatusData()
    {
        IList<ServiceOrderStateChangeHis> statusList = bllServiceOrderStateChangeHis.GetOrderHisList(CurrentOrder);
        rptOrderDoneStatus.DataSource = statusList;
        rptOrderDoneStatus.DataBind();
    }

    protected void btnOrderStatusChange_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (btn.CommandName)
        {
            case "ConfirmOrder":
                bllServeiceOrder.OrderFlow_BusinessConfirm(CurrentOrder);
                break;
            case "ConfirmPrice":
                // 如果输入为空，则价格为原来的价格
                if (txtConfirmPrice.Text == "") {
                    txtConfirmPrice.Text = CurrentOrder.NegotiateAmount.ToString();
                }
                bllServeiceOrder.OrderFlow_BusinessNegotiate(CurrentOrder,Convert.ToDecimal(txtConfirmPrice.Text));
                break;
            case "Assigned":
                bllServeiceOrder.OrderFlow_BusinessStartService(CurrentOrder);
                break;
            case "Begin":
                bllServeiceOrder.OrderFlow_BusinessFinish(CurrentOrder);
                break;
        }
        BingData();
    }

    protected void BingData()
    {
        btnConfirmOrder.Visible = false;
        txtConfirmPrice.Visible = false;
        btnConfirmPrice.Visible = false;
        btnBegin.Visible = false;
        btnIsEndOrder.Visible = false;
        ServiceOrder order = CurrentOrder;
        switch (order.OrderStatus)
        {
            case Dianzhu.Model.Enums.enum_OrderStatus.Payed:
                panelConfirmOrder.Visible = true;
                ctnrOrderStatus.Visible = true;
                btnConfirmOrder.Visible = true;
                break;
            case Dianzhu.Model.Enums.enum_OrderStatus.Negotiate:
                ctnrOrderStatus.Visible = true;
                panelConfirmPrice.Visible = true;
                txtConfirmPrice.Visible = true;
                btnConfirmPrice.Visible = true;
                break;
            case Dianzhu.Model.Enums.enum_OrderStatus.Begin:
                ctnrOrderStatus.Visible = true;
                btnIsEndOrder.Visible = true;
                break;
            case Dianzhu.Model.Enums.enum_OrderStatus.Assigned:
                ctnrOrderStatus.Visible = true;
                btnBegin.Visible = true;
                break;
            default:
                return;
        }
    }



}

