using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Pay;
using System.Net;
using Dianzhu.Model.Enums;
using Newtonsoft.Json;
using PHSuit;
using System.Collections.Specialized;
using System.Web.Security;
/// <summary>
/// Summary description for PY001007
/// </summary>
public class ResponsePY001007:BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponsePY001007(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataPY001007 requestData = this.request.ReqData.ToObject<ReqDataPY001007>();

        DZMembershipProvider bllMember = new DZMembershipProvider();

        try
        {
            Guid orderId;

            DZMembership member;
            bool validated = new Account(bllMember).ValidateUser(new Guid(requestData.userID), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }

            bool isOrderId = Guid.TryParse(requestData.orderID, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "OrderId格式有误";
                return;
            }

            BLLPayment bllPayment = new BLLPayment();
            Payment payment = bllPayment.GetOne(orderId);

            switch (requestData.type.ToLower())
            {
                case "alipay":
                    break;
                case "wepay":
                    bool sucdess = false;
                    while (!sucdess)
                    {
                        IPay ipay = new PayWeChat(payment.Amount, payment.Id.ToString(), payment.Order.ServiceName, Dianzhu.Config.Config.GetAppSetting("NotifyServer"), payment.Order.ServiceDescription);
                        //var respDataWeibo = new NameValueCollection();
                        string respDataWechat = "<xml>";

                        string[] arrPropertyValues = ipay.CreatePayRequest().Split('&');
                        foreach (string value in arrPropertyValues)
                        {
                            string[] arrValue = value.Split('=');
                            //respDataWeibo.Add(arrValue[0], arrValue[1]);
                            if (arrValue[0] != "key")
                            {
                                respDataWechat += "<" + arrValue[0] + ">" + arrValue[1] + "</" + arrValue[0] + ">";
                            }
                        }
                        respDataWechat = respDataWechat + "</xml>";
                        ilog.Debug(respDataWechat);
                        //XmlDocument respDataWechatXml = JsonConvert.DeserializeXmlNode(respDataWechat);

                        string requestUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder?";
                        string resultTokenWechat = HttpHelper.CreateHttpRequestPostXml(requestUrl, respDataWechat);
                        ilog.Debug(resultTokenWechat);
                        resultTokenWechat = resultTokenWechat.Replace("\\n", string.Empty);
                        ilog.Debug(resultTokenWechat);
                        string json = JsonHelper.Xml2Json(resultTokenWechat, true);
                        ilog.Debug(json);

                        //todo:下面这段数据就是返回的字符串，不知道怎么解析
                        //resultTokenWechat="<xml>< return_code >< ![CDATA[SUCCESS]] ></ return_code >\n      < return_msg >< ![CDATA[OK]] ></ return_msg >\n            < appid >< ![CDATA[wxd928d1f351b77449]] ></ appid >\n                  < mch_id >< ![CDATA[1304996701]] ></ mch_id >\n                        < nonce_str >< ![CDATA[RnTyNTtoDpMC335q]] ></ nonce_str >\n                              < sign >< ![CDATA[8440115DC99103B7B242042239395967]] ></ sign >\n                                    < result_code >< ![CDATA[SUCCESS]] ></ result_code >\n                                          < prepay_id >< ![CDATA[wx201602031137215b5350a5300317902456]] ></ prepay_id >\n                                                < trade_type >< ![CDATA[APP]] ></ trade_type >\n                                                      </ xml > ";

                        RespData_WeChatUserObj respData = JsonConvert.DeserializeObject<RespData_WeChatUserObj>(json);
                        ilog.Debug(respData.return_code);
                        if(respData.return_code != "SUCCESS")
                        {
                            continue;
                        }
                        if (respData.result_code != "SUCCESS")
                        {
                            ilog.Error("错误代码：" + respData.err_code + "  错误代码描述：" + respData.err_code_des);
                            continue;
                        }
                        sucdess = true;
                        RespDataPY001007 respObj = new RespDataPY001007();
                        respObj.appid = respData.appid;
                        respObj.partnerid = respData.mch_id;
                        respObj.prepayid = respData.prepay_id;
                        this.state_CODE = Dicts.StateCode[0];
                        this.RespData = respObj;
                        return; 
                    }
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }
    

    private string DownloadToMediaserver(string fileUrl)
    {
        string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        var respData = new NameValueCollection();
        respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
        respData.Add("originalName", string.Empty);
        respData.Add("domainType", "UserAvatar");
        respData.Add("fileType", "image");

        return HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
    }
}

public class ReqDataPY001007
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    public string type { get; set; }
}

public class RespData_WeChatUserObj
{
    public string return_code { get; set; }//返回状态码
    public string return_msg { get; set; }//返回信息

    public string appid { get; set; }//公众账号ID,企业号corpid即为此appId
    public string mch_id { get; set; }//商户号
    public string device_info { get; set; }//设备号    
    public string nonce_str { get; set; }//随机字符串
    public string sign { get; set; }//签名    
    public string result_code { get; set; }//业务结果
    public string err_code { get; set; }//错误代码
    public string err_code_des { get; set; }//错误代码描述

    public string trade_type { get; set; }//交易类型
    public string prepay_id { get; set; }//预支付交易会话标识
    public string code_url { get; set; }//二维码链接 

}

public class RespDataPY001007
{
    public string appid { get; set; }
    public string partnerid { get; set; }
    public string prepayid { get; set; }
}
