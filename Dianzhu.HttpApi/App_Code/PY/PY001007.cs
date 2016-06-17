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
using Dianzhu.Api.Model;
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

        DZMembershipProvider bllMember = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLPayment bllPayment = Bootstrap.Container.Resolve<BLLPayment>();

        try
        {
            Guid payId;
            
            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(bllMember).ValidateUser(new Guid(requestData.userID), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }

            bool isOrderId = Guid.TryParse(requestData.payID, out payId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "payId格式有误";
                return;
            }
            
            Payment payment = bllPayment.GetOne(payId);

            if (payment == null)
            {
                ilog.Error("该单号" + payId + "不存在！");
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "该单号不存在！";
                return;
            }

            switch (requestData.target.ToLower())
            {
                case "alipay":
                    // http://119.29.39.211:8168
                    IPayRequest ipayAli = new PayAlipayApp(payment.Amount, payment.Id.ToString(), payment.Order.Title,Dianzhu.Config.Config.GetAppSetting("PaySite") + "PayCallBack/Alipay/notify_url.aspx", payment.Order.Description);
                    string respDataAli = ipayAli.CreatePayRequest();
                    RespDataPY001007 respAliObj = new RespDataPY001007();
                    respAliObj.orderString = respDataAli;
                    this.state_CODE = Dicts.StateCode[0];
                    this.RespData = respAliObj;
                    return;
                case "wepay":
                    for (int i = 0; i < 10; i++)
                    {
                        
                        IPayRequest ipayWe = new PayWeChat(payment.Amount, payment.Id.ToString(), payment.Order.Title
                            , Dianzhu.Config.Config.GetAppSetting("PaySite")+ "PayCallBack/Wepay/notify_url.aspx", payment.Order.Description);
                        //var respDataWeibo = new NameValueCollection();
                        string respDataWechat = "<xml>";

                        string[] arrPropertyValues = ipayWe.CreatePayRequest().Split('&');
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

                        string orderString = ipayWe.CreatePayStr(respData.prepay_id);

                        RespDataPY001007 respObj = new RespDataPY001007();
                        respObj.orderString = orderString;
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
