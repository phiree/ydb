using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Dianzhu.Api.Model;

/// <summary>
/// 上传经纬度
/// </summary>
public class ResponseLCT001008 : BaseResponse
{
    public ResponseLCT001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataLCT001008 requestData = this.request.ReqData.ToObject<ReqDataLCT001008>();

       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        string raw_id = requestData.userID;

        try
        {
            if (request.NeedAuthenticate)
            {
                MemberDto member;
                bool validated = new Account(memberService).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }

            try
            {
                string lng = requestData.longitude;//经度
                string lat = requestData.latitude;//纬度
                string tran_url = Dianzhu.Config.Config.GetAppSetting("BaiduTranAPI") +
                    Dianzhu.Config.Config.GetAppSetting("BaiduTranAK") +
                    "&coords=" + lat + "," + lng + "&from=3&to=5";
                string tran_return = Get_Http(tran_url,1000000);
                RespTran obj = Deserialize<RespTran>(tran_return);

                double tran_lng = obj.result[0].x;//转换后经度
                double tran_lat = obj.result[0].y;//转换后纬度
                string geo_url = Dianzhu.Config.Config.GetAppSetting("BaiduGeocodingAPI") +
                    Dianzhu.Config.Config.GetAppSetting("BaiduGeocodingAK") +
                    "&output=json&pois=0&location=" + tran_lng + "," + tran_lat;
                string geo_return = Regex.Unescape(Get_Http(geo_url, 1000000));
                RespGeo geoObj= Deserialize<RespGeo>(geo_return);

                RespDataLCT001008_locationObj locationObj = new RespDataLCT001008_locationObj();
                locationObj.name = geoObj.result.addressComponent.city;
                string spell = GetChineseSpell(locationObj.name);
                locationObj.key = spell.Substring(0,1);
                locationObj.code = geoObj.result.cityCode;

                RespDataLCT001008 respData = new RespDataLCT001008();
                respData.locationObj = locationObj;

                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];
            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }


    }

    /// <summary>
    /// 只转换每个汉字首字母（大写）
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    public static string GetChineseSpell(string strText)
    {
        int len = strText.Length;
        string myStr = "";
        for (int i = 0; i < len; i++)
        {
            myStr += getSpell(strText.Substring(i, 1));
        }
        return myStr;
    }

    /// <summary>
    /// 获得第一个汉字的首字母（大写）；
    /// </summary>
    /// <param name="cnChar"></param>
    /// <returns></returns>
    public static string getSpell(string cnChar)
    {
        byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return "*";
        }
        else return cnChar;
    }

    /// <summary>
    /// 获取访问http结果
    /// </summary>
    /// <param name="strUrl">指定URL路径地址</param>
    /// <param name="timeout">超时时间设置</param>
    private string Get_Http(string strUrl, int timeout)
    {
        string strResult;
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            myReq.Timeout = timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {
            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }

    /// <summary>
    /// json 转为 对象或list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string data)
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        return json.Deserialize<T>(data);
    }
}