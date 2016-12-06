using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
public partial class test_Default : BasePage
{
    public IBLLServiceOrder bllOrder =Bootstrap.Container.Resolve<IBLLServiceOrder>();
    Ydb.Common.Infrastructure.ISerialNoBuilder serialNoBuilder = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.ISerialNoBuilder>();
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreateOrder_Click(object sender,EventArgs e)
    {
      
       IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();

        MemberDto customer = memberService.GetUserByName(tbxCustomerName.Text);
        if (customer == null)
        {
            lblCreateOrderResult.Text = "用户不存在";
            return;
        }
        MemberDto customerService = memberService.GetUserByName("aa@aa.aa");
        ServiceOrder order= ServiceOrderFactory.CreateDraft(customerService.Id.ToString(), customer.Id.ToString());
        DZService service = dzServiceService.GetOne2(new Guid("0f4bdace-dad0-43aa-8cce-a5c501180535"));
         order.AddDetailFromIntelService(service.Id.ToString(),new ServiceSnapShot
         {
             AllowedPayType =service.AllowedPayType.ToString(), CancelCompensation = service.CancelCompensation, ChargeUnit =service.ChargeUnit.ToString() 
             ,DepositAmount =0.01m,Description = service.Description,Enabled = service.Enabled,IsCompensationAdvance = service.IsCompensationAdvance,IsForBusiness = service.IsForBusiness
             ,MinPrice = service.MinPrice,OrderDelay = service.OrderDelay
             
         },new WorkTimeSnapshot(), 1, "test_用户名", "13999999999", "test_服务地址", DateTime.Now, string.Empty);

        order.CreatedFromDraft();
        order.LatestOrderUpdated = DateTime.Now;
        order.DepositAmount = 0.01m;
        bllOrder.Save(order);
        bllPayment.ApplyPay(order, enum_PayTarget.Deposit);
        lblCreateOrderResult.Text = "创建成功";

    }
}