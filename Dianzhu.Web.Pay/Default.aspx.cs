using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Com.Alipay;
public partial class _Default : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger("dzpay");
    BLLServiceOrder bllOrder = new BLLServiceOrder();
    ServiceOrder order = null;
    public ServiceOrder Order {
        get { return order; }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
         
            LoadOrder();
       

    }
    protected void btnAlipay_Click(object sender, EventArgs e)
    {
        
        //商户订单号
        string out_trade_no =order.Id.ToString();
        //商户网站订单系统中唯一订单号，必填

        //订单名称
        string subject = order.ServiceName ;
        //必填

        //付款金额
        string total_fee = order.OrderAmount.ToString("#0.00");
        //必填

        //商品展示地址
        string show_url = "http://www.ydban.cn/";
        //必填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

        //订单描述
        string body = order.Memo;
        
 


        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("partner", Config.partner);
        sParaTemp.Add("seller_id", Config.seller_id);
        sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
        sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
        sParaTemp.Add("payment_type", Config.paytype);
        sParaTemp.Add("notify_url", System.Configuration.ConfigurationManager.AppSettings["PaySite"]+"alipay/return_url.aspx");
        sParaTemp.Add("return_url", System.Configuration.ConfigurationManager.AppSettings["PaySite"] + "alipay/return_url.aspx");
        sParaTemp.Add("out_trade_no", out_trade_no);
        sParaTemp.Add("subject", subject);
        sParaTemp.Add("total_fee", total_fee);
        sParaTemp.Add("show_url", show_url);
        sParaTemp.Add("body", body);
        sParaTemp.Add("it_b_pay", string.Empty);
        sParaTemp.Add("extern_token", string.Empty);

        //建立请求
        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
       // throw new Exception(sHtmlText);
        Response.Write(sHtmlText);
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