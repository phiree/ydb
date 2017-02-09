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
using Ydb.PayGateway.DomainModel.Pay;
using Ydb.Order.DomainModel;
/// <summary>
/// 支付结果回调 和 退款结果回调 接口参数一样.
/// </summary>
public partial class CallBackHandler : BasePage
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    IServiceOrderService orderService = Bootstrap.Container.Resolve<IServiceOrderService>();


    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {
        string invokeUrl = Request.RawUrl;
        string httpMethod = Request.HttpMethod.ToLower();
        object parameters = string.Empty;
        log.Debug("回调开始.回调网页:" + invokeUrl);
        if (httpMethod == "get") {  parameters = Request.QueryString; log.Debug("回调参数:" + Request.RawUrl); }
        else if (httpMethod == "post")
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream))
            { parameters = sr.ReadToEnd(); }
            log.Debug("回调参数:" + parameters);
        }
        else
        { log.Error("请求参数有误：" + Request.HttpMethod); throw new Exception("请求参数有误：" + Request.HttpMethod); }
       //根据参数判断返回状态, 如果是 wait_buyer_pay 则直接忽略

        //判断订单状态,如果已经支付成功,则直接返回.

        enum_PayAPI payApi;

        IPayCallBackFactory pcbf = Bootstrap.Container.Resolve<IPayCallBackFactory>();

        IPayCallBack callBack = pcbf.Create(invokeUrl, httpMethod, parameters,out payApi);
        bool needIgnore= callBack.DoIgnore(parameters);
        if (needIgnore)
        {
            return;
        }
        bool callBackResult;
        string payedStatus;
        string businessOrderId, platformOrderId;
        decimal totalAmount;
        string success_details, fail_details;
        //根据回调值,处理业务逻辑
        if (callBack is IPayCallBackSingle)
        {
             callBackResult = ((IPayCallBackSingle)callBack).ParseBusinessData(parameters,out payedStatus, out businessOrderId, out platformOrderId, out totalAmount, out errMsg);
            //更新订单及支付项状态.
            if (callBackResult==false)
            {
                log.Error("回调参数解析错误");
                return;
            }
            ServiceOrder order = GetOrder(new Guid(businessOrderId));
            IPaymentService paymentService = Bootstrap.Container.Resolve<IPaymentService>();
            paymentService.PayCallBack(payApi, payedStatus, businessOrderId, platformOrderId);

        }
        else //batch
        {
            callBackResult = ((IPayCallBacBatch)callBack).ParseBusinessData(parameters,out   payedStatus, out success_details, out fail_details, out errMsg);
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
   static  ServiceOrder currentOrder;
    ServiceOrder GetOrder(Guid id) {
      
            if (currentOrder == null)
            {
                currentOrder = orderService.GetOne(id);
            }
            return currentOrder;
       
    }

}

