using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Data;

public partial class order_detail : BasePage
{
    IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
    Dianzhu.BLL.Finance.IBLLServiceTypePoint bllServiceTypePoint = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBLLServiceTypePoint>();
    Dianzhu.BLL.Finance.IBalanceFlowService balanceService = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBalanceFlowService>();
    Dianzhu.BLL.Finance.IBLLSharePoint bllSharePoint = Bootstrap.Container.Resolve<Dianzhu.BLL.Finance.IBLLSharePoint>();
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
        //Dianzhu.Model.Enums.enum_OrderStatus status = (Dianzhu.Model.Enums.enum_OrderStatus)Enum.Parse(typeof(Dianzhu.Model.Enums.enum_OrderStatus), Request.QueryString["status"].ToString());
        //serviceorder = bllServiceOrder.GetOrderStatusPrevious(status).OrderByDescending(x => x.OrderCreated);

        lblTitle.Text = serviceorder.Title;
        lblServiceBusinessName.Text = serviceorder.ServiceBusinessName;
        lblDescription.Text = serviceorder.Description;
        lblCustomer.Text = serviceorder.Customer.UserName;
        lblCustomerService.Text = serviceorder.CustomerService.UserName;
        lblOrderCreated.Text = serviceorder.OrderCreated.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderConfirmTime.Text = serviceorder.OrderConfirmTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblLatestOrderUpdated.Text = serviceorder.LatestOrderUpdated.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderFinished.Text = serviceorder.OrderFinished.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderServerStartTime.Text = serviceorder.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblOrderServerFinishedTime.Text = serviceorder.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss");
        lblMemo.Text = serviceorder.Memo;
        lblOrderStatus.Text = serviceorder.OrderStatus.ToString();
        lblTargetAddress.Text = serviceorder.TargetAddress;
        lblTargetTime.Text = serviceorder.TargetTime;
        //lblStaff.Text = serviceorder.Staff.ToArray().ToString();
        if (serviceorder.Staff.Count > 0)
        {
            foreach (Staff s in serviceorder.Staff)
            {
                lblStaff.Text = lblStaff.Text + s.Name + ",";
            }
            lblStaff.Text = lblStaff.Text.TrimEnd(',');
        }
        else
        { lblStaff.Text = ""; }
        lblUnitAmount.Text = serviceorder.UnitAmount.ToString();
        lblOrderAmount.Text = serviceorder.OrderAmount.ToString();
        lblDepositAmount.Text = serviceorder.DepositAmount.ToString();
        lblNegotiateAmount.Text = serviceorder.NegotiateAmount.ToString();
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
        
            Dianzhu.BLL.Finance.OrderShare os = new Dianzhu.BLL.Finance.OrderShare(bllServiceTypePoint, bllSharePoint, agentService, balanceService);
            IList < Dianzhu.Model.Finance.BalanceFlow > shareFlow= os.Share(serviceorder);

            var typePoint = bllServiceTypePoint.GetPoint(serviceorder.Details[0].OriginalService.ServiceType);
            var sharedAmount = serviceorder.NegotiateAmount * typePoint;
            lblShareAmount.Text = sharedAmount.ToString();

            foreach (Dianzhu.Model.Finance.BalanceFlow bf in shareFlow)
            {
                if (bf.Member == serviceorder.CustomerService)
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