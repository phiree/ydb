using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Pay;
using Dianzhu.BLL;

public partial class Pay_BatchTest : Dianzhu.Web.Common.BasePage
{
    BLLPay bllPay = Bootstrap.Container.Resolve<BLLPay>();
    Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder iserialno = Bootstrap.Container.Resolve<Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AliBatch_Click(object sender, EventArgs e)
    {
        //jsyk_development@126.com
        string strSerialNo = iserialno.GetSerialNo("PAYB" + DateTime.Now.ToString("yyyyMMddHHmm"), 2);
        string strSubject= iserialno.GetSerialNo("PAYBL" + DateTime.Now.ToString("yyyyMMddHHmmssfff"), 2)+"^13288844855^邓楚献^1^测试|";
        strSubject = strSubject+iserialno.GetSerialNo("PAYBL" + DateTime.Now.ToString("yyyyMMddHHmmssfff"), 2) + "^jsyk_development@126.com^海口集思优科网络科技有限公司^1^测试";
        IPayRequest pay = bllPay.CreatePayBatch(2, strSerialNo, strSubject);
        string requestString = pay.CreatePayRequest();
        Response.Write(requestString);
    }

    protected void WePay_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 企业付款(请求需要双向证书)
    /// 企业付款业务是基于微信支付商户平台的资金管理能力，为了协助商户方便地实现企业向个人付款，
    /// 针对部分有开发能力的商户，提供通过API完成企业付款的功能。 比如目前的保险行业向客户退保、给付、理赔。
    /// 企业付款将使用商户的可用余额，需确保可用余额充足。查看可用余额、充值、提现请登录商户平台“资金管理”进行操作。https://pay.weixin.qq.com/ 
    /// 注意：与商户微信支付收款资金并非同一账户，需要单独充值。
    /// </summary>
    /// <param name="json">企业支付数据</param>
    /// <returns></returns>
    //public CorpPayResult CorpPay(CorpPayJson json)
    //{
        //CheckAccount();//检查AccountInfo的对象属性值

        //WxPayData data = new WxPayData();
        //data.SetValue("mch_appid", AccountInfo.UniteAppId);//公众账号appid， 注意是mch_appid，而非wxappid
        //data.SetValue("mchid", AccountInfo.MchID);//商户号, 注意是mchid而非mch_id
        //data.SetValue("nonce_str", data.GenerateNonceStr());//随机字符串
        //data.SetValue("spbill_create_ip", NetworkUtil.GetIPAddress());//终端ip      
        //data.SetValue("partner_trade_no", data.GenerateOutTradeNo(AccountInfo.MchID));//随机字符串

        //data.SetValue("device_info", json.device_info);//终端ip            
        //data.SetValue("openid", json.openid);
        //data.SetValue("check_name", json.check_name);
        //data.SetValue("re_user_name", json.re_user_name);
        //data.SetValue("amount", json.amount);
        //data.SetValue("desc", json.desc);

        //data.SetValue("sign", data.MakeSign(AccountInfo.PayAPIKey));//最后生成签名

        //var url = string.Format("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers");
        //return Helper.GetPayResultWithCert<CorpPayResult>(data, url, AccountInfo.CertPath, AccountInfo.CertPassword);
    //}
}

/// <summary>
/// 企业付款的数据信息
/// </summary>
public class CorpPayJson
{
    public CorpPayJson()
    {
        this.check_name = "FORCE_CHECK";
    }

    /// <summary>
    /// 微信支付分配的终端设备号
    /// </summary>
    public string device_info { get; set; }

    /// <summary>
    /// 用户openid
    /// </summary>
    public string openid { get; set; }

    /// <summary>
    /// 校验用户姓名选项,可以使用 PayCheckName枚举对象获取名称
    /// NO_CHECK：不校验真实姓名 
    /// FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
    /// OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
    /// </summary>
    public string check_name { get; set; }

    /// <summary>
    /// 收款用户真实姓名。 
    /// 如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名
    /// </summary>
    public string re_user_name { get; set; }

    /// <summary>
    /// 企业付款金额，单位为分
    /// </summary>
    public int amount { get; set; }

    /// <summary>
    /// 企业付款操作说明信息。必填。
    /// </summary>
    public string desc { get; set; }

    /// <summary>
    /// 调用接口的机器Ip地址
    /// </summary>
    public string spbill_create_ip { get; set; }
}

/// <summary>
/// 企业付款操作的返回结果
/// </summary>
public class CorpPayResult //: PayResult
{
    /// <summary>
    /// 微信分配的公众账号ID（企业号corpid即为此appId）
    /// </summary>
    public string mch_appid { get; set; }

    /// <summary>
    /// 微信支付分配的终端设备号
    /// </summary>
    public string device_info { get; set; }

    /// <summary>
    /// 商户使用查询API填写的单号的原路返回. 
    /// </summary>
    public string partner_trade_no { get; set; }

    /// <summary>
    /// 企业付款成功，返回的微信订单号
    /// </summary>
    public string payment_no { get; set; }

    /// <summary>
    /// 企业付款成功时间
    /// </summary>
    public string payment_time { get; set; }
}