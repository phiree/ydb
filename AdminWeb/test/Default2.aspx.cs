using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class test_Default : BasePage
{
    IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreateOrder_Click(object sender,EventArgs e)
    {
        DZMembershipProvider bllMembership = Bootstrap.Container.Resolve<DZMembershipProvider>();

        BLLDZService bllService = Bootstrap.Container.Resolve<BLLDZService>();
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();

        DZMembership customer = bllMembership.GetUserByName(tbxCustomerName.Text);
        if (customer == null)
        {
            lblCreateOrderResult.Text = "用户不存在";
            return;
        }
        DZMembership customerService = bllMembership.GetUserByName("aa@aa.aa");
        ServiceOrder order= ServiceOrderFactory.CreateDraft(customerService, customer);
        DZService service = bllService.GetAll()[0];
         order.AddDetailFromIntelService(service, 1, "test_用户名", "13999999999", "test_服务地址", DateTime.Now);

        order.CreatedFromDraft();
        order.LatestOrderUpdated = DateTime.Now;
        order.DepositAmount = 0.01m;
        bllOrder.Save(order);
        bllPayment.ApplyPay(order, Dianzhu.Model.Enums.enum_PayTarget.Deposit);
        lblCreateOrderResult.Text = "创建成功";

    }
}