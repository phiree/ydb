using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Data;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
public partial class order_detail : BasePage
{
    IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
    Dianzhu.BLL.Finance.IBLLServiceTypePoint bllServiceTypePoint = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBLLServiceTypePoint>();
    Dianzhu.BLL.Finance.IBalanceFlowService balanceService = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBalanceFlowService>();
    Dianzhu.BLL.Finance.IBLLSharePoint bllSharePoint = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBLLSharePoint>();
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    Dianzhu.BLL.Agent.AgentService agentService = new Dianzhu.BLL.Agent.AgentService();
    ServiceOrder serviceorder;

    string strID;//链接字符串
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == "" || Request.QueryString["ID"] == null)
        {
            Response.Redirect("index.aspx");
        }
        else
        {
            strID = Request.QueryString["ID"].ToString();
        }
        Guid id = Guid.Parse(strID);
        serviceorder = bllServiceOrder.GetOne(id);
        //.enum_OrderStatus status = (.enum_OrderStatus)Enum.Parse(typeof(.enum_OrderStatus), Request.QueryString["status"].ToString());
        //serviceorder = bllServiceOrder.GetOrderStatusPrevious(status).OrderByDescending(x => x.OrderCreated);

        lblTitle.Text = serviceorder.Title;
        lblServiceBusinessName.Text = serviceorder.ServiceBusinessName;
        lblServiceBusinessPhone.Text = serviceorder.ServiceBusinessPhone;
        lblDescription.Text = serviceorder.Description;
        lblCustomer.Text = serviceorder.CustomerId ;
        lblCustomerService.Text = serviceorder.CustomerServiceId;
        lblOrderCreated.Text = serviceorder.OrderCreated.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderConfirmTime.Text = serviceorder.OrderConfirmTime.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : serviceorder.OrderConfirmTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblLatestOrderUpdated.Text = serviceorder.LatestOrderUpdated.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderFinished.Text = serviceorder.OrderFinished.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : serviceorder.OrderFinished.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderServerStartTime.Text = serviceorder.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : serviceorder.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderServerFinishedTime.Text = serviceorder.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? "" : serviceorder.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblMemo.Text = serviceorder.Memo;
        lblOrderStatus.Text = serviceorder.GetStatusTitleFriendly(serviceorder.OrderStatus);
        lblTargetAddress.Text = serviceorder.TargetAddress;
        lblTargetTime.Text = serviceorder.TargetTime;
        //lblStaff.Text = serviceorder.Staff.ToArray().ToString();
        //if (serviceorder.Staff.Count > 0)
        //{
        //    foreach (Staff s in serviceorder.Staff)
        //    {
        //        lblStaff.Text = lblStaff.Text + s.Name + ",";
        //    }
        //    lblStaff.Text = lblStaff.Text.TrimEnd(',');
        //}
        //else
        //{ lblStaff.Text = ""; }
        if (serviceorder.Staff == null)
        {
            lblStaff.Text = "";
        }
        else
        {
            lblStaff.Text = serviceorder.Staff.Name;
        }
        lblUnitAmount.Text = serviceorder.UnitAmount.ToString("f2");
        lblOrderAmount.Text = serviceorder.OrderAmount.ToString("f2");
        lblDepositAmount.Text = serviceorder.DepositAmount.ToString("f2");
        lblNegotiateAmount.Text = serviceorder.NegotiateAmount.ToString("f2");
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();
        Payment payment = bllPayment.GetOne(id);
        if (payment == null)
        {
            lblGetPayAmount.Text = "0";
        }
        else
        {
            lblGetPayAmount.Text = bllPayment.GetPayAmount(serviceorder, payment.PayTarget).ToString();
        }
        lblTitle.Text = serviceorder.Title;
        if (lblOrderStatus.Text == "Finished")
        {
        
            Dianzhu.BLL.Finance.OrderShare os = new Dianzhu.BLL.Finance.OrderShare(bllServiceTypePoint, bllSharePoint, agentService, balanceService,memberService);
            IList < Dianzhu.Model.Finance.BalanceFlow > shareFlow= os.Share(serviceorder);

            var typePoint = bllServiceTypePoint.GetPoint(serviceorder.Details[0].OriginalService.ServiceType);
            var sharedAmount = serviceorder.NegotiateAmount * typePoint;
            lblShareAmount.Text = sharedAmount.ToString();

            foreach (Dianzhu.Model.Finance.BalanceFlow bf in shareFlow)
            {
                if (bf.MemberId.ToString() == serviceorder.CustomerServiceId)
                {
                    lblCustomerServiceShare.Text = bf.Amount.ToString();
                }
                else
                {
                    lblAgentShare.Text = bf.Amount.ToString();
                }
            }
        }
        try {
            lblPlatformShare.Text = (decimal.Parse(lblShareAmount.Text) - decimal.Parse(lblCustomerServiceShare.Text) - decimal.Parse(lblAgentShare.Text)).ToString();
        }
        catch { }
    }
}