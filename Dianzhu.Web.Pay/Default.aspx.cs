using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Com.Alipay;
using Dianzhu.Pay;

public partial class _Default : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    BLLServiceOrder bllOrder = new BLLServiceOrder();
    ServiceOrder order = null;
    public ServiceOrder Order {
        get { return order; }
        
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
     
        LoadOrder();
       

    }
    protected void btnPay_Click(object sender, EventArgs e)
    {


        //支付类型: 支付定金/支付尾款/支付赔偿
        enum_PayTarget payTarget = (enum_PayTarget)Enum.Parse(typeof(enum_PayTarget), Request["ptarget"]);
        //支付方式:线上,线下.
        enum_PayType payType = enum_PayType.Online;
        
        //在线支付接口
        enum_PayAPI payAPI = enum_PayAPI.None;
        if (radioAlipay.Checked)
        {
            payAPI = enum_PayAPI.Alipay;
        }
        else if (radioWechat.Checked)
        {
            payAPI = enum_PayAPI.Wechat;
        }
        
        BLLPay bllPay = new BLLPay();
        string requestString=string.Empty;
        //在线支付
        if (payType == enum_PayType.Online)
        {
         
            IPay pay = bllPay.CreatePayAPI(payAPI, order);
          // todo:修改支付接口  requestString = pay.CreatePayRequest(payTarget);
            Response.Write(requestString);
            
        }
        //保存支付记录
        bllPay.SavePaymentLog(order, payType, enum_PaylogType.ApplyFromUser, payTarget, payAPI, requestString);
        //离线支付.
        if (payType== enum_PayType.Offline)
        {
           
            Response.Redirect("payoffline.html",true);
        }

       
    }
    private void LoadOrder() {
       string paramOrderId = Request["orderId"];
        Guid orderId;
        bool isValidId = Guid.TryParse(paramOrderId, out orderId);
        if (!isValidId)
        {
          
            Response.Redirect("error.aspx?err=1",true);
        }
        order=  bllOrder.GetOne(orderId);
        if (order == null)
        {
            
            Response.Redirect("error.aspx?err=2",true);
        }
        if (order.OrderStatus != Dianzhu.Model.Enums.enum_OrderStatus.Created)
        { 
            Response.Redirect("error.aspx?err=3", true);
        }
        
       
    }
    
}