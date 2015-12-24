﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;

/// <summary>
/// 功能：批量付款到支付宝账户有密接口接入页
/// 版本：3.3
/// 日期：2012-07-05
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
/// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
/// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
/// 
/// 如果不想使用扩展功能请把扩展功能参数赋空值。
/// </summary>
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void BtnAlipay_Click(object sender, EventArgs e)
    {
        ////////////////////////////////////////////请求参数////////////////////////////////////////////

        //服务器异步通知页面路径
        string notify_url = "http://商户网关地址/batch_trans_notify-CSHARP-UTF-8/notify_url.aspx";
        //需http://格式的完整路径，不允许加?id=123这类自定义参数

        //付款账号
        string email = WIDemail.Text.Trim();
        //必填

        //付款账户名
        string account_name = WIDaccount_name.Text.Trim();
        //必填，个人支付宝账号是真实姓名公司支付宝账号是公司名称

        //付款当天日期
        string pay_date = WIDpay_date.Text.Trim();
        //必填，格式：年[4位]月[2位]日[2位]，如：20100801

        //批次号
        string batch_no = WIDbatch_no.Text.Trim();
        //必填，格式：当天日期[8位]+序列号[3至16位]，如：201008010000001

        //付款总金额
        string batch_fee = WIDbatch_fee.Text.Trim();
        //必填，即参数detail_data的值中所有金额的总和

        //付款笔数
        string batch_num = WIDbatch_num.Text.Trim();
        //必填，即参数detail_data的值中，“|”字符出现的数量加1，最大支持1000笔（即“|”字符出现的数量999个）

        //付款详细数据
        string detail_data = WIDdetail_data.Text.Trim();
        //必填，格式：流水号1^收款方帐号1^真实姓名^付款金额1^备注说明1|流水号2^收款方帐号2^真实姓名^付款金额2^备注说明2....


        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("partner", Config.Partner);
        sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
        sParaTemp.Add("service", "batch_trans_notify");
        sParaTemp.Add("notify_url", notify_url);
        sParaTemp.Add("email", email);
        sParaTemp.Add("account_name", account_name);
        sParaTemp.Add("pay_date", pay_date);
        sParaTemp.Add("batch_no", batch_no);
        sParaTemp.Add("batch_fee", batch_fee);
        sParaTemp.Add("batch_num", batch_num);
        sParaTemp.Add("detail_data", detail_data);

        //建立请求
        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
        Response.Write(sHtmlText);
        
    }
}
