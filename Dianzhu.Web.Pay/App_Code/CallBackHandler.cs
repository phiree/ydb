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

public partial class CallBackHandler : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    string errMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

        log.Debug("支付完成，调用notifyurl");
        Response.Write(Request.RawUrl);
        string rawUrl = Request.RawUrl;
        IPayCallBack payCallBack;
        if (rawUrl.ToLower().StartsWith("/paycallback/wepay"))
        {
            payCallBack = new PayCallBackWePay();
        }
        else if (rawUrl.ToLower().StartsWith("/paycallback/alipay"))
        {
            //保存支付接口返回的原始数据
            payCallBack = new PayCallBackAli();
        }
        else
        {
            errMsg = "错误的回调页面: " + rawUrl;
            log.Error(errMsg);
            Response.Write(errMsg);
        }
        try
        {
            BLLPay bllPay = new BLLPay();
            bllPay.ReceiveAPICallBack(Dianzhu.Model.Enums.enum_PaylogType.ResultNotifyFromAli, new PayCallBackAli(), Request.RawUrl, Request.QueryString);
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