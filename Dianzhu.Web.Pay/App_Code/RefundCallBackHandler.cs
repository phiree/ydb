using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
 


 
using Ydb.Common;
using Ydb.PayGateway;
using Ydb.Order.Application;
using System.Web.Script.Serialization;
public partial class RefundCallBackHandler : BasePage
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    IPaymentService paymentService = Bootstrap.Container.Resolve<IPaymentService>();
    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

        string invokeUrl = Request.RawUrl;
        string httpMethod = Request.HttpMethod.ToLower();
        object parameters =null;

        if (httpMethod == "get") { parameters = Request.QueryString; }
        else if (httpMethod == "post") { using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream)) { parameters = sr.ReadToEnd(); } }
        else { log.Error("请求参数有误：" + Request.HttpMethod); throw new Exception("请求参数有误：" + Request.HttpMethod); }


        string rawUrl = Request.RawUrl;
     
       
        enum_PayAPI payApi;
      
        string refundId;
        decimal totalAmount;
        
        ////微信代码已删除, 因为微信退款通知是同步返回的,无需回调.


        IRefundCallBack refundCallBack = RefundCallbackFactory.CreateCallBack(invokeUrl,out payApi);
       string callbackResult= refundCallBack.RefundCallBack(parameters, out refundId, out totalAmount, out errMsg);

        //处理业务: 订单状态已经在 接口中处理了, 回调至需要更新退款申请状态
       
        try
        {
            //BLLPay bllPay = new BLLPay();


            paymentService.RefundCallBack(payApi, callbackResult, refundId, string.Empty);

            //bllRefund.ReceiveAPICallBack(payLogType, refundCallBack, Request.RawUrl, parameters);
            //bllPay.ReceiveAPICallBack(payLogType, payCallBack, Request.RawUrl, parameters);
            if (rawUrl.Contains("return_url"))
            {
                log.Debug("同步调用成功,跳转至成功页面");
                Response.Redirect("~/paysuc.html");
            }
            else
            {
                log.Debug(payApi + "异步调用成功");
                if (payApi == enum_PayAPI.AlipayWeb)
                {
                    Response.Write("success");
                }
            }
        }
        catch (Exception ex)
        {
            errMsg = "ERROR:" + ex.Message;
            log.Error(errMsg);
            Response.Write("fail");
        }
        log.Debug("-------------------回调结束------------------");


    }

}