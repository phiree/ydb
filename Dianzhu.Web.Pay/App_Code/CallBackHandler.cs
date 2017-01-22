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
using Com.Alipay;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.Order.Application;
using Ydb.Common;
using Ydb.PayGateway;
using System.Web.Script.Serialization;
/// <summary>
/// 支付结果回调 和 退款结果回调 接口参数一样.
/// </summary>
public partial class CallBackHandler : BasePage
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {
        string invokeUrl = Request.RawUrl;
        string httpMethod = Request.HttpMethod.ToLower();
        object parameters = string.Empty;

        if (httpMethod == "get") {  parameters = Request.QueryString; }
        else if (httpMethod == "post") { using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream)) { parameters = sr.ReadToEnd(); } }
        else { log.Error("请求参数有误：" + Request.HttpMethod); throw new Exception("请求参数有误：" + Request.HttpMethod); }

        enum_PayAPI payApi;
        IPayCallBack callBack = PayCallBackFactory.CreateCallBack(invokeUrl, httpMethod, parameters,out payApi);
        string callBackResult;
        string businessOrderId, platformOrderId;
        decimal totalAmount;
        string success_details, fail_details;
        //根据回调值,处理业务逻辑
        if (callBack is IPayCallBackSingle)
        {
             callBackResult = ((IPayCallBackSingle)callBack).PayCallBack(parameters, out businessOrderId, out platformOrderId, out totalAmount, out errMsg);
            //更新订单及支付项状态.
            IServiceOrderService orderService = Bootstrap.Container.Resolve<IServiceOrderService>();
            IPaymentService paymentService = Bootstrap.Container.Resolve<IPaymentService>();
            paymentService.PayCallBack(payApi, callBackResult, businessOrderId, platformOrderId);

        }
        else //batch
        {
            callBackResult = ((IPayCallBacBatch)callBack).PayCallBackBatch(parameters, out success_details, out fail_details, out errMsg);
            //更新提现记录申请.
            Ydb.Finance.Application.IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<Ydb.Finance.Application.IWithdrawApplyService>();
            if (!string.IsNullOrEmpty(success_details))
            {
                withdrawApplyService.PayWithdrawSuccess(success_details);
            }
            if (!string.IsNullOrEmpty(fail_details))
            {
                withdrawApplyService.PayWithdrawFail(fail_details);
            }

        }

        //处理结果
            log.Debug(payApi + "异步调用成功");
            if (payApi == enum_PayAPI.AlipayWeb||payApi== enum_PayAPI.AlipayApp)
            {
                Response.Write("success");
            }
            else
            {
                string xml = @"<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>";
                Response.ContentType = "text/xml";
                Response.Write(xml);
            }
       
    }

}

