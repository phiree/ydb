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
public partial class CallBackHandler : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

        log.Debug("支付完成，调用notifyurl");
      
        string rawUrl = Request.RawUrl;
        IPayCallBack payCallBack=null;
        enum_PaylogType payLogType= enum_PaylogType.None;
        if (rawUrl.ToLower().StartsWith("/paycallback/wepay"))
        {
            payCallBack = new PayCallBackWePay();
            payLogType = enum_PaylogType.ReturnNotifyFromWePay;
        }
        else if (rawUrl.ToLower().StartsWith("/paycallback/alipay"))
        {
            //保存支付接口返回的原始数据
            if (rawUrl.ToLower().Contains("return_url"))
            { payLogType = enum_PaylogType.ResultReturnFromAli; }
            else
            {
                payLogType = enum_PaylogType.ResultNotifyFromAli;
            }
            payCallBack = new PayCallBackAli();
            
        }
        else
        {
            errMsg = "错误的回调页面: " + rawUrl;
            log.Error(errMsg);
            Response.Write(errMsg);
            Response.End();
        }
        try
        {
            BLLPay bllPay = new BLLPay();

            object parameters = null;
            if (Request.HttpMethod.ToLower() == "get")
            {
                parameters = Request.QueryString;
            }
            using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream))
            {
                parameters = sr.ReadToEnd();
            }

            bllPay.ReceiveAPICallBack(payLogType, payCallBack, Request.RawUrl, parameters);
            if (rawUrl.Contains("return_url"))
            {
                Response.Redirect("~/paysuc.html");
            }
            else
            {
                Response.Write("success");
            }
        }
        catch (Exception ex)
        {
            errMsg = "ERROR:" + ex.Message;
            log.Error(errMsg);
            Response.Write("fail");
        }

    }

}