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
using Dianzhu.Pay;
using Dianzhu.Model.Enums;
using System.Web.Script.Serialization;
public partial class RefundCallBackHandler : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    BLLRefund bllRefund = Bootstrap.Container.Resolve<BLLRefund>();
    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

        log.Debug("-------------------支付完成,回调开始------------------");
      
        string rawUrl = Request.RawUrl;
        log.Debug("回调地址：" + rawUrl);
        //IPayCallBack payCallBack=null;
        IRefundCallBack refundCallBack = null;
        enum_PaylogType payLogType= enum_PaylogType.None;
        enum_PayAPI payApi;
        if (rawUrl.ToLower().StartsWith("/refundcallback/wepay"))
        {
            payApi = enum_PayAPI.Wechat;
            log.Debug("微支付回调开始");
            //payCallBack = new PayCallBackWePay();
            refundCallBack = new RefundCallBackAli();
            payLogType = enum_PaylogType.ReturnNotifyFromWePay;
        }
        else if (rawUrl.ToLower().StartsWith("/refundcallback/alipay"))
        {
            payApi = enum_PayAPI.Alipay;
            log.Debug("支付宝回调开始");
            //保存支付接口返回的原始数据
            if (rawUrl.ToLower().Contains("return_url"))
            {
                payLogType = enum_PaylogType.ResultReturnFromAli;
            }
            else
            {
                payLogType = enum_PaylogType.ResultNotifyFromAli;
            }
            //payCallBack = new PayCallBackAli();
            refundCallBack = new RefundCallBackAli();
        }
        else
        {
            payApi = enum_PayAPI.None;
            errMsg = "错误的回调页面: " + rawUrl;
            log.Error(errMsg);
            Response.Write(errMsg);
            Response.End();
        }
        try
        {
            //BLLPay bllPay = new BLLPay();
           

            object parameters = null;
            log.Debug("回调参数:");
            if (Request.HttpMethod.ToLower() == "get")
            {
                log.Debug("Get参数:"+Request.RawUrl);
                parameters = Request.QueryString;               
            }
            else if(Request.HttpMethod.ToLower()=="post")
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream))
                {
                    parameters = sr.ReadToEnd();
                    log.Debug("Post参数:" + parameters);
                }
            }
            else
            {
                log.Error("请求参数有误：" + Request.HttpMethod);
                throw new Exception("请求参数有误：" + Request.HttpMethod);
            }

            bllRefund.ReceiveAPICallBack(payLogType, refundCallBack, Request.RawUrl, parameters);
            //bllPay.ReceiveAPICallBack(payLogType, payCallBack, Request.RawUrl, parameters);
            if (rawUrl.Contains("return_url"))
            {
                log.Debug("同步调用成功,跳转至成功页面");
                Response.Redirect("~/paysuc.html");
            }
            else
            {
                log.Debug(payApi + "异步调用成功");
                if (payApi == enum_PayAPI.Alipay)
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