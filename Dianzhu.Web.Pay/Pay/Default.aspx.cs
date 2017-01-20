using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using Ydb.Common;
using Ydb.PayGateway;
using Ydb.Order.DomainModel;
using Ydb.Order.Application;

/// <summary>
/// 
/// </summary>
[Obsolete("网页支付已经停用,该页面只保证编译通过.")]
public partial class Pay_Default : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    IServiceOrderService orderService = Bootstrap.Container.Resolve<IServiceOrderService>();
    IPaymentService paymentService = Bootstrap.Container.Resolve<IPaymentService>();
    ServiceOrder order = null;
    Payment payment = null;
    public Payment Payment
    {
        get { return payment; }
    }
    public ServiceOrder Order {
        get { return order; }
        
    }
    string orderId;
    enum_PayTarget payTarget;
    protected void Page_Load(object sender, EventArgs e)
    {

        string orderId = Request["orderId"];
        string paramPayTarget = Request["paytarget"];
      bool isPayTarget=  Enum.TryParse<enum_PayTarget>(paramPayTarget, out payTarget);
       


    }
    protected void btnPay_Click(object sender, EventArgs e)
    {


        //支付类型: 支付定金/支付尾款/支付赔偿
        enum_PayTarget payTarget = payment.PayTarget;
        //支付方式:线上,线下.
        enum_PayType payType = enum_PayType.Online;
        
        //在线支付接口
        enum_PayAPI payAPI = enum_PayAPI.None;
        if (radioAlipay.Checked)
        {
            payAPI = enum_PayAPI.AlipayWeb;
        }
        else if (radioWechat.Checked)
        {
            payAPI = enum_PayAPI.Wechat;
        }
        else
        {
            payAPI = enum_PayAPI.AliBatch;
        }
        
       
        string requestString=string.Empty;
        payment = paymentService.ApplyPay(orderId, payTarget);
        //在线支付
        if (payType == enum_PayType.Online)
        {
            IPayRequest pay = PayFactory.CreatePayAPI(payAPI,payment.Amount,payment.Id.ToString(),payment.Memo,payTarget);
            requestString = pay.CreatePayRequest();
            
            Response.Write(requestString);
            
        }
        //保存支付记录
       
        //离线支付.
        if (payType== enum_PayType.Offline)
        {
           
            Response.Redirect("payoffline.html",true);
        }

       
    }
   
    
}