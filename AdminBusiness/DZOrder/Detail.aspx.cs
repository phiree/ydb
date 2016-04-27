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
  public   ServiceOrder CurrentOrder;

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
                        
                        btnConfirmOrder.Visible = true;
                        
                        break;
                    case Dianzhu.Model.Enums.enum_OrderStatus.Negotiate:
                      
                        txtConfirmPrice.Visible = true;
                        
                        btnConfirmPrice.Visible = true;
                        break;
                    
                    case Dianzhu.Model.Enums.enum_OrderStatus.Begin:
                     
                        btnIsEndOrder.Visible = true;
                       
                        break;

                case Dianzhu.Model.Enums.enum_OrderStatus.Assigned:

                btnBegin.Visible = true;

                break;
            default:
                        return;
                }
            }

        

}

