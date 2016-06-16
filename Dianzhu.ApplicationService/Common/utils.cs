using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Dianzhu.ApplicationService
{
    public class utils
    {

        /// <summary>
        /// 接口异常结果返回
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static common_Trait_400_Rsponses SetRes_Error(Exception ex)
        {
            common_Trait_400_Rsponses res_Error = new common_Trait_400_Rsponses();
            if (ex.Message == "009004")
            {
                res_Error.errCode = "009004";
                res_Error.errString = "没有找到符合条件的数据！";
            }
            else
            {
                res_Error.errCode = "009001";
                res_Error.errString = ex.Message;
            }
            return res_Error;
        }

        /// <summary>
        /// 判断分页参数是否正确
        /// </summary>
        /// <param name="filtert">通用筛选器</param>
        /// <returns></returns>
        public static int[] CheckFilter(common_Trait_Filtering filter)
        {
            int[] page = { 0, 0 };
            if (filter.pageSize != null && filter.pageNum != null)
            {
                try
                {
                    page[0] = int.Parse(filter.pageSize);
                    page[1] = int.Parse(filter.pageNum);
                    if (page[0] <= 0 || page[1] < 1)
                    {
                        throw new Exception("分页参数pageSize,pageNum错误！");
                    }
                }
                catch
                {
                    throw new Exception("分页参数pageSize,pageNum错误！");
                }
            }
            return page;
        }

        /// <summary>
        /// 判断Guid
        /// </summary>
        /// <param name="strGuidID">GuidID</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public static Guid CheckGuidID(string strGuidID, string strError)
        {
            if (strGuidID == null ||  strGuidID == "")
                return Guid.Empty;
            Guid GuidID;
            bool guididisGuid = Guid.TryParse(strGuidID, out GuidID);
            if (!guididisGuid)
            {
                throw new Exception(strError+"格式有误");
            }
            return GuidID;
        }

        /// <summary>
        /// 判断DateTime
        /// </summary>
        /// <param name="strDateTime">DateTime</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public static DateTime CheckDateTime(string strDateTime,string strType, string strError)
        {
            if (strDateTime == null || strDateTime == "")
                return DateTime.MinValue;
            DateTime dt;
            try
            {
                string stime = DateTime.ParseExact(strDateTime, strType, null).ToString("yyyy-MM-dd HH:mm:ss");
                dt = DateTime.Parse(stime);
            }
            catch (Exception e)
            {
                throw new Exception(strError + "格式有误");
            }
            return dt;
        }



        /// <summary>
        /// 根据经纬度获取城市
        /// </summary>
        /// <param name="lng">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public static string GetCity(string lng, string lat)
        {
            //string lng = location.longitude;//经度
            //string lat = location.latitude;//纬度
            string tran_url = Dianzhu.Config.Config.GetAppSetting("BaiduTranAPI") +
                Dianzhu.Config.Config.GetAppSetting("BaiduTranAK") +
                "&coords=" + lat + "," + lng + "&from=3&to=5";
            string tran_return = utils.Get_Http(tran_url, 1000000);
            RespTran obj = utils.Deserialize<RespTran>(tran_return);

            double tran_lng = obj.result[0].x;//转换后经度
            double tran_lat = obj.result[0].y;//转换后纬度
            string geo_url = Dianzhu.Config.Config.GetAppSetting("BaiduGeocodingAPI") +
                Dianzhu.Config.Config.GetAppSetting("BaiduGeocodingAK") +
                "&output=json&pois=0&location=" + tran_lng + "," + tran_lat;
            string geo_return = Regex.Unescape(utils.Get_Http(geo_url, 1000000));
            return geo_return;
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
        public static string Get_Http(string strUrl, int timeout)
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
}
