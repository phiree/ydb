using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alipay;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using System.Web;

/// <summary>
/// PayCallBack 的摘要说明
/// </summary>
public class CallBackAliBatch
{
    public CallBackAliBatch()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public string PayCallBack(object parameters)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
        NameValueCollection coll = new NameValueCollection();

        string[] parameters_str = parameters.ToString().Split('&');
        string[] item;
        for (int i = 0; i < parameters_str.Count(); i++)
        {
            item = parameters_str[i].Split('=');
            coll.Add(item[0], HttpUtility.UrlDecode(item[1], Encoding.UTF8));
        }
        SortedDictionary<string, string> sPara = Dianzhu.Pay.GetAliRequestGet.GetRequestGet(coll);

        //获取订单号
        //string total_fee = coll["total_fee"];            //获取总金额
        //string subject = coll["subject"];                //商品名称、订单名称
        //string body = coll["body"];                      //商品描述、订单备注、描述
        //string buyer_email = coll["buyer_email"];        //买家支付宝账号
        //交易状态   

        string notify_id = coll["notify_id"];
        string sign = coll["sign"];
        bool isVerified = false;
        try
        {
            isVerified = new Notify().Verify(sPara, notify_id, sign);
        }
        catch (Exception ex)
        {
            log.Error("参数验证结果异常:" + ex.Message.ToString ());
        }
        log.Debug("参数验证结果:" + isVerified);
       
        if (isVerified)
        {
            //platformOrderId = coll["trade_no"];              //支付宝交易号
            //businessOrderId = coll["out_trade_no"];
            //total_amount = Convert.ToDecimal(coll["total_fee"]);
            string success_details = coll["success_details"];
            string fail_details = coll["fail_details"];

            Ydb.Finance.Application.IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<Ydb.Finance.Application.IWithdrawApplyService>();
            if (!string.IsNullOrEmpty(success_details))
            {
                withdrawApplyService.PayWithdrawSuccess(success_details);
            }
            if (!string.IsNullOrEmpty(fail_details))
            {
                withdrawApplyService.PayWithdrawFail(fail_details);
            }
            
            //string trade_status = coll["trade_status"].ToUpper();
            //log.Debug("交易结果:" + trade_status);
            //log.Debug("结果说明:" + Dianzhu.Pay.PayCallBackAliBatch.TradeStatus[trade_status]);
            //return trade_status;
            return "";
        }
        else
        {
            return "";
        }
    }
}