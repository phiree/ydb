using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class test_Default : System.Web.UI.Page
{
    public IBLLServiceOrder bllOrder = Installer.Container.Resolve<IBLLServiceOrder>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreateOrder_Click(object sender,EventArgs e)
    {
        DZMembershipProvider bllMembership = new DZMembershipProvider();
        
        BLLDZService bllService = new BLLDZService();
        BLLPayment bllPayment = new BLLPayment();

        DZMembership customer = bllMembership.GetUserByName(tbxCustomerName.Text);
        if (customer == null)
        {
            lblCreateOrderResult.Text = "用户不存在";
            return;
        }
        DZMembership customerService = bllMembership.GetUserByName("aa@aa.aa");
        ServiceOrder order= ServiceOrderFactory.CreateDraft(customerService, customer);
        DZService service = bllService.GetOne(new Guid("0f4bdace-dad0-43aa-8cce-a5c501180535"));
         order.AddDetailFromIntelService(service, 1, "test_服务地址", DateTime.Now);

        order.CreatedFromDraft();
        order.LatestOrderUpdated = DateTime.Now;
        order.DepositAmount = 0.01m;
        bllOrder.SaveOrUpdate(order);
        bllPayment.ApplyPay(order, Dianzhu.Model.Enums.enum_PayTarget.Deposit);
        lblCreateOrderResult.Text = "创建成功";

    }
}