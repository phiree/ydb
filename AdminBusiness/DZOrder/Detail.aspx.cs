﻿
 
using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;

 
public partial class DZOrder_Detail : BasePage
{
    IServiceOrderService bllServeiceOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
   IBusinessService businessService= Bootstrap.Container.Resolve<IBusinessService>();
    // BLLServiceOrder bllServeiceOrder =Bootstrap.Container.Resolve<BLLServiceOrder>();
    IServiceOrderStateChangeHisService bllServiceOrderStateChangeHis = Bootstrap.Container.Resolve<IServiceOrderStateChangeHisService>();
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
     public ServiceOrder CurrentOrder;
  
    Guid OrderId;
    public string merchantID
    {
        get
        {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Guid businessId = new Guid(Request["businessId"]);
      
         OrderId = new Guid(Request["orderId"]);
        CurrentOrder = bllServeiceOrder.GetOne(OrderId);

        BingData();
        BindDoneStatusData();
        BindCustomerInfo();
    }
    private void BindCustomerInfo()
    {
        var memberDto = memberService.GetUserById(CurrentOrder.CustomerId);
        liCustomerName.Text = memberDto.DisplayName+memberDto.UserName;
        liCustomerPhone.Text = memberDto.Phone;
    }
    protected void BindDoneStatusData()
    {
        IOrderedEnumerable<ServiceOrderStateChangeHis> statusList = bllServiceOrderStateChangeHis.GetOrderHisList(CurrentOrder).OrderByDescending(x => x.CreatTime);
        rptOrderDoneStatus.DataSource = statusList;
        foreach(ServiceOrderStateChangeHis item in statusList)
        {
            item.NewStatusStr = item.Order.GetStatusTitleFriendly(item.NewStatus);
            item.OldStatusStr = item.Order.GetStatusTitleFriendly(item.OldStatus);
        }
        rptOrderDoneStatus.DataBind();
    }

    protected void btnOrderStatusChange_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
       
        switch (btn.CommandName)
        {
            case "ConfirmOrder":
                bllServeiceOrder.OrderFlow_BusinessConfirm(OrderId);
                break;
            case "ConfirmPrice":
                // 如果输入为空，则价格为原来的价格
                if (txtConfirmPrice.Text == "") {
                    txtConfirmPrice.Text = CurrentOrder.NegotiateAmount.ToString();
                }
                bllServeiceOrder.OrderFlow_BusinessNegotiate(OrderId, Convert.ToDecimal(txtConfirmPrice.Text));
                break;
            case "Assigned":
                bllServeiceOrder.OrderFlow_BusinessStartService(OrderId);
                break;
            case "Begin":
                bllServeiceOrder.OrderFlow_BusinessFinish(OrderId);
                break;
        }
        BingData();
    }

    protected void BingData()
    {
        // 订单状态控制区
        panelOrderStatus.Visible = false;

        // 确认订单控制
        panelConfirmOrder.Visible = false;
        btnConfirmOrder.Visible = false;

        // 确认价格控制
        panelConfirmPrice.Visible = false;
        txtConfirmPrice.Visible = false;
        btnConfirmPrice.Visible = false;

        // 服务开始控制
        btnBegin.Visible = false;

        // 订单完成控制
        btnIsEndOrder.Visible = false;

        ServiceOrder order = CurrentOrder;

        switch (order.OrderStatus)
        {
            case  enum_OrderStatus.Payed:
                panelOrderStatus.Visible = true;
                panelConfirmOrder.Visible = true;
                btnConfirmOrder.Visible = true;
                break;
            case  enum_OrderStatus.Negotiate:
                panelOrderStatus.Visible = true;
                panelConfirmPrice.Visible = true;
                txtConfirmPrice.Visible = true;
                btnConfirmPrice.Visible = true;
                break;
            case  enum_OrderStatus.Assigned:
                panelOrderStatus.Visible = true;
                btnBegin.Visible = true;
                break;
            case  enum_OrderStatus.Begin:
                panelOrderStatus.Visible = true;
                btnIsEndOrder.Visible = true;
                break;
            default:
                return;
        }
    }



}

