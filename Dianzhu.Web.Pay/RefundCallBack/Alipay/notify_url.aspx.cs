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

public partial class notify_url : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    protected void Page_Load(object sender, EventArgs e)
    {
        log.Debug("----------------回调开始------------------");
        log.Debug("支付完成，AliPay回调 notifyurl");
        //保存支付接口返回的原始数据
        BLLPay bllPay = new BLLPay();
        try {
            bllPay.ReceiveAPICallBack(Dianzhu.Model.Enums.enum_PaylogType.ResultNotifyFromAli, new PayCallBackAli(), Request.RawUrl,  Request.QueryString);
            Response.Write("success");
        }
        catch {
            Response.Write("fail");
        }
        log.Debug("----------------回调结束------------------");

    }
 
}