﻿
 
using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Common;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;


public partial class DZOrder_Default : BasePage
{
    IServiceOrderService bllOrder = Bootstrap.Container.Resolve<IServiceOrderService>();

    //   BLLServiceOrder bllServeiceOrder =Bootstrap.Container.Resolve<BLLServiceOrder>();
     IStaffService staffService = Bootstrap.Container.Resolve<IStaffService>();    
    public string merchantID {
        get {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindTotalData();
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

        var IOrderList = bllOrder.GetListForBusiness(CurrentBusiness.Id.ToString(), currentPageIndex, pager.PageSize, out totalRecord)
             .OrderByDescending(x => x.LatestOrderUpdated);
        rpOrderList.DataSource = IOrderList;
        foreach (ServiceOrder item in IOrderList) {
            item.OrderStatusStr = item.GetStatusTitleFriendly(item.OrderStatus);
        }


        pager.RecordCount = Convert.ToInt32(totalRecord);
        rpOrderList.DataBind();
    }
   
    private void BindTotalData()
    {
        // 未完成订单: Created
       // Dianzhu.BLL.BLLServiceOrder bllOrder = new Dianzhu.BLL.BLLServiceOrder();
        liUnDoneOrderCount.Text = (bllOrder.GetAllOrdersForBusiness(CurrentBusiness.Id).Count()
            -
            bllOrder.GetAllOrdersForBusiness(CurrentBusiness.Id)
            .Where(x => x.OrderStatus >= enum_OrderStatus.Finished).Count()).ToString();

        // 已完成订单: IsEnd
        liFinishOrderCount.Text = bllOrder.GetAllCompleteOrdersForBusiness(CurrentBusiness.Id)
            .Where(x => x.OrderStatus >= enum_OrderStatus.Finished).Count().ToString();
    }
   IOrderAssignmentService bllOrderAssignment = Bootstrap.Container.Resolve<IOrderAssignmentService>();
    Ydb.Membership.Application.IDZMembershipService memberService = Bootstrap.Container.Resolve<Ydb.Membership.Application.IDZMembershipService>();

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e) {
        ServiceOrder order = (ServiceOrder)e.Item.DataItem;
        Label txtStaffs = e.Item.FindControl("assignStaffs") as Label;
        IList<OrderAssignment> list = bllOrderAssignment.GetOAListByOrder(order);

        if (list.LongCount() == 0) {
            txtStaffs.Text = "尚未指派";
        }

        foreach (OrderAssignment ass in list)
        {
            var assedStaff= staffService.GetOne(new Guid(ass.AssignedStaffId));
            txtStaffs.Text += assedStaff.Name + ";";
        }
        //CustomerName
        Label lblCustomerName = e.Item.FindControl("lblCustomerName") as Label;
        lblCustomerName.Text = memberService.GetUserById( order.CustomerId).DisplayName;

    }

    //protected void rptOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //    {
    //        ServiceOrder order = (ServiceOrder)e.Item.DataItem;
    //            switch (order.OrderStatus)
    //            {
    //                case .enum_OrderStatus.Created:
    //                    //获取支付项
    //                    Payment payMent = bllPayment.GetPaymentForWaitPay(order);// .ApplyPay(order, .enum_PayTarget.Deposit);
    //                    if (payMent == null)
    //                    {
    //                        return;
    //                    }
    //                    string payLinkDepositAmount = bllPayment.BuildPayLink(payMent.Id);
    ////                    HyperLink hlDepositAmount = e.Item.FindControl("PayDepositAmount") as HyperLink;
    ////                    hlDepositAmount.Text = "用户订金付款链接：";
    ////                    hlDepositAmount.NavigateUrl = payLinkDepositAmount;
    ////                    hlDepositAmount.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.Payed:
    ////                    Button btnConfimOrder = e.Item.FindControl("btnConfimOrder") as Button;
    ////                    btnConfimOrder.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.Negotiate:
    ////                    TextBox txtConfirmPrice = e.Item.FindControl("txtConfirmPrice") as TextBox;
    ////                    txtConfirmPrice.Visible = true;
    ////                    Button btnConfirmPrice = e.Item.FindControl("btnConfirmPrice") as Button;
    ////                    btnConfirmPrice.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.Assigned:
    //                    Button btnConfirmPriceCustomer = e.Item.FindControl("btnConfirmPriceCustomer") as Button;
    //                    btnConfirmPriceCustomer.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.Begin:
    //                    Button btnIsEndOrder = e.Item.FindControl("btnIsEndOrder") as Button;
    //                    btnIsEndOrder.Visible = true;
    //                    Button btnIsEndOrderCustomer = e.Item.FindControl("btnIsEndOrderCustomer") as Button;
    //                    btnIsEndOrderCustomer.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.IsEnd:
    //                    Button btnIsEndOrderCustomerIs = e.Item.FindControl("btnIsEndOrderCustomer") as Button;
    //                    btnIsEndOrderCustomerIs.Visible = true;
    //                    break;
    //                case .enum_OrderStatus.Ended:
    //                    Payment paymentFinal = bllPayment.GetPaymentForWaitPay(order); //bllPayment.ApplyPay(order, .enum_PayTarget.FinalPayment);
    //                    string payLinkFinalPayment = bllPayment.BuildPayLink(paymentFinal.Id);
    //                    HyperLink hlFinalPayment = e.Item.FindControl("PayFinalPayment") as HyperLink;
    //                    hlFinalPayment.Text = "用户尾款付款链接：";
    //                    hlFinalPayment.NavigateUrl = payLinkFinalPayment;
    //                    hlFinalPayment.Visible = true;
    //                    break;
    //                default:
    //                    return;
    //            }
    //}

    //protected void rptOrderList_Command(object sender, RepeaterCommandEventArgs e)
    //{
    //    Guid orderId = new Guid(e.CommandArgument.ToString());

    //    ServiceOrder order = bllServeiceOrder.GetOne(orderId);
    //    switch (e.CommandName.ToLower())
    //    {
    //        case "confirmorder":
    //            bllServeiceOrder.OrderFlow_BusinessConfirm(order);
    //            break;
    //        case "confirmprice":
    //            TextBox t = e.Item.FindControl("txtConfirmPrice") as TextBox;
    //            decimal price = t.Text == string.Empty ? 0 : Convert.ToDecimal(t.Text);
    //            bllServeiceOrder.OrderFlow_BusinessNegotiate(order, price);
    //            break;
    //        case "confirmpricecustomer":
    //            bllServeiceOrder.OrderFlow_BusinessStartService(order);
    //            break;
    //        case "isendorder":
    //            bllServeiceOrder.OrderFlow_BusinessFinish(order);
    //            break;
    //        case "isendordercustomer":
    //            bllServeiceOrder.OrderFlow_CustomerFinish(order);
    //            break;
    //        default:
    //            return;
    //    }
    //    BindData();
    //}
}