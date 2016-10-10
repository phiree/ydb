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
using System.Collections.Specialized;
using PHSuit;
using System.Reflection;
using System.Drawing;
using System.Security.Cryptography;

namespace Dianzhu.ApplicationService
{
    public class utils
    {
        /// <summary>
        /// 星期数验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DayOfWeek CheckWeek(string str)
        {
            DayOfWeek week = new DayOfWeek();
            switch ( str.ToLower())
            {
                case "0":
                    week = DayOfWeek.Sunday;
                    break;
                case "1":
                    week = DayOfWeek.Monday;
                    break;
                case "2":
                    week = DayOfWeek.Tuesday;
                    break;
                case "3":
                    week = DayOfWeek.Wednesday;
                    break;
                case "4":
                    week = DayOfWeek.Thursday;
                    break;
                case "5":
                    week = DayOfWeek.Friday;
                    break;
                case "6":
                    week = DayOfWeek.Saturday;
                    break;
                case "7":
                    week = DayOfWeek.Sunday;
                    break;
                case "monday":
                    week = DayOfWeek.Monday;
                    break;
                case "tuesday":
                    week = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                    week = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                    week = DayOfWeek.Thursday;
                    break;
                case "friday":
                    week = DayOfWeek.Friday;
                    break;
                case "saturday":
                    week = DayOfWeek.Saturday;
                    break;
                case "sunday":
                    week = DayOfWeek.Sunday;
                    break;
                case "星期一":
                    week = DayOfWeek.Monday;
                    break;
                case "星期二":
                    week = DayOfWeek.Tuesday;
                    break;
                case "星期三":
                    week = DayOfWeek.Wednesday;
                    break;
                case "星期四":
                    week = DayOfWeek.Thursday;
                    break;
                case "星期五":
                    week = DayOfWeek.Friday;
                    break;
                case "星期六":
                    week = DayOfWeek.Saturday;
                    break;
                case "星期日":
                    week = DayOfWeek.Sunday;
                    break;
                case "星期天":
                    week = DayOfWeek.Sunday;
                    break;
                case "周一":
                    week = DayOfWeek.Monday;
                    break;
                case "周二":
                    week = DayOfWeek.Tuesday;
                    break;
                case "周三":
                    week = DayOfWeek.Wednesday;
                    break;
                case "周四":
                    week = DayOfWeek.Thursday;
                    break;
                case "周五":
                    week = DayOfWeek.Friday;
                    break;
                case "周六":
                    week = DayOfWeek.Saturday;
                    break;
                case "周天":
                    week = DayOfWeek.Sunday;
                    break;
                case "周日":
                    week = DayOfWeek.Sunday;
                    break;
                default:
                    throw new FormatException("星期数错误，必须为:1~7 或 monday~sunday 或 周一~周日 或 星期一~星期天！");
            }
            return week;
        }

        /// <summary>
        /// 接口异常结果返回
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static common_Trait_400_Rsponses SetRes_Error(Exception ex)
        {
            NHibernateUnitOfWork.UnitOfWork.Current.Dispose();
            NHibernateUnitOfWork.UnitOfWork.Start();
            common_Trait_400_Rsponses res_Error = new common_Trait_400_Rsponses();
            switch (ex.Message)
            {
                case "009004":
                    res_Error.errCode = "009004";
                    res_Error.errString = "没有找到符合条件的数据！";
                    break;
                case "001002":
                    res_Error.errCode = "001002";
                    res_Error.errString = "用户名或密码错误！";
                    break;
                default:
                    res_Error.errCode = "009001";
                    res_Error.errString = ex.Message;
                    break;
            }
            //if (ex.Message == "009004")
            //{
            //    res_Error.errCode = "009004";
            //    res_Error.errString = "没有找到符合条件的数据！";
            //}
            //else
            //{
            //    res_Error.errCode = "009001";
            //    res_Error.errString = ex.Message;
            //}
            return res_Error;
        }

        /// <summary>
        /// 判断分页参数是否正确
        /// </summary>
        /// <param name="filtert">通用筛选器</param>
        /// <returns></returns>
        public static Model.Trait_Filtering CheckFilter(common_Trait_Filtering filter,string TName)
        {
            Model.Trait_Filtering filter1 = new Model.Trait_Filtering();
            filter1.baseID = filter.baseID;
            filter1.ascending = filter.ascending;
            filter1.ids = filter.ids;
            if (filter.pageSize != null && filter.pageSize != "" && filter.pageNum != null && filter.pageNum != "")
            {
                try
                {
                    filter1.pageSize = int.Parse(filter.pageSize);
                    filter1.pageNum = int.Parse(filter.pageNum);
                    if (filter1.pageSize <= 0 || filter1.pageNum < 1)
                    {
                        throw new FormatException("分页参数pageSize,pageNum错误！");
                    }
                }
                catch
                {
                    throw new FormatException("分页参数pageSize,pageNum错误！");
                }
            }
            else
            {
                filter1.pageSize = 0;
                filter1.pageNum = 0;
            }
            int intN = 0;
            //if (!int.TryParse(filter.offset, out intN))
            //{
            //    filter1.offset = intN;
            //}
            int.TryParse(filter.offset, out intN);
            filter1.offset = intN;
            try
            {
                filter1.sortby = SortMapping.SortMap(filter.sortby,TName);
            }
            catch
            {
                throw new FormatException("排序参数转换错误！");
            }
            return filter1;
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
                throw new FormatException(strError+"格式有误");
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
                throw new FormatException(strError + "格式有误");
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

        /// <summary>
        /// Url上传文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="strOriginalName"></param>
        /// <param name="strDomainType"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public static string DownloadToMediaserver(string fileUrl, string strOriginalName, string strFileType)
        {
            //string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
            //var respData = new NameValueCollection();
            //respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
            //respData.Add("originalName", string.Empty);
            //respData.Add("domainType", "StaffAvatar");
            //respData.Add("fileType", "image");
            try
            {
                string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrlByDate");
                //url = "http://192.168.1.177:8038/UploadFileByDate.ashx";
                var respData = new NameValueCollection();
                string strUrl = HttpUtility.UrlEncode(fileUrl);
                if (fileUrl.IndexOf(':') == 1)
                {
                    respData.Add("fileBase64", ToBase64(fileUrl));
                }
                else
                {
                    respData.Add("fileUrl", strUrl);
                }
                respData.Add("originalName", strOriginalName);
                respData.Add("fileType", strFileType);
                respData.Add("domainType", "StaffAvatar");
                string strResult = HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
                if (strResult .Contains( "上传失败！"))
                {
                    throw new Exception(strResult);
                }
                return strResult;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("上传失败！"))
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("上传失败！" + ex.Message);
                }
            }
        }

        /// <summary>
        /// base64上传文件
        /// </summary>
        /// <param name="fileBase64"></param>
        /// <param name="strOriginalName"></param>
        /// <param name="strDomainType"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public static string DownloadToMediaserver1(string fileBase64, string strOriginalName,  string strFileType)
        {
            try
            {
                string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrlByDate");
                //url = "http://192.168.1.172:8038/UploadFileByDate.ashx";
                //url = "http://192.168.1.177:8038/UploadFileByDate.ashx";
                var respData = new NameValueCollection();
                respData.Add("fileBase64", fileBase64);
                respData.Add("originalName", strOriginalName);
                respData.Add("fileType", strFileType);
                string strResult= HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
                if (strResult.Contains("上传失败！"))
                {
                    throw new Exception(strResult);
                }
                return strResult;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("上传失败！"))
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("上传失败！" + ex.Message);
                }
            }
        }
        
        /// <summary>
        /// 从文件Url中获取文件名
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static string GetFileName(string fileUrl)
        {
            string strFileName = fileUrl;
            if (fileUrl.Contains("?fileName="))
            {
                string[] strN = fileUrl.Split(new string[] { "?fileName=" },StringSplitOptions.None);
                strFileName = strN[1];
                string regp = @"(_(\d+)[x|X](\d+))$";
                Regex re = new Regex(regp);
                bool isImageThumbnail = re.IsMatch(strFileName);
                if (isImageThumbnail)
                {
                    Match regexMatch = re.Match(strFileName);
                    //thumbnailWidth = Convert.ToInt32(regexMatch.Groups[2].Value);
                    //thumbnailHeight = Convert.ToInt32(regexMatch.Groups[3].Value);
                    //cleanedFileName = fileName.Replace(regexMatch.Groups[1].Value, string.Empty);
                    strFileName = strFileName.Replace(regexMatch.Groups[1].Value, string.Empty);
                }
            }
            if (!JudgeFileExist03(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + strFileName))
            {
                throw new Exception("该文件资源不存在！");
            }
            return strFileName;
        }

        /// <summary>
        /// 验证网络图片是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool JudgeFileExist03(string url)
        {
            try
            {
                //创建根据网络地址的请求对象
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;
                //返回响应状态是否是成功比较的布尔值
                return (((System.Net.HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将图片数据转换为Base64字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string ToBase64(string fileUrl)
        {
            //Image img = new Image();
            //img.ImageUrl = fileUrl;
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //binFormatter.Serialize(memStream, img);
            //byte[] bytes = memStream.GetBuffer();
            //string base64 = Convert.ToBase64String(bytes);
           
            string base64Img = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileUrl));
            return base64Img;
            
        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Base64ToImage(string fileBase64,out string height,out string width, out string size)//(object sender, EventArgs e)
        {
            string base64 = fileBase64;//this.richTextBox.Text;
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Bitmap.FromStream(memStream);
            
            //using System.Runtime.Serialization.Formatters.Binary;
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //Image img = (Image)binFormatter.Deserialize(memStream);

            height = img.Height.ToString ();
            width = img.Width.ToString();
            size = (bytes.Length / 1024).ToString();
        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Base64ToAudio(string fileBase64, out string size)//(object sender, EventArgs e)
        {
            string base64 = fileBase64;//this.richTextBox.Text;
            byte[] bytes = Convert.FromBase64String(base64);
            size = (bytes.Length / 1024).ToString();
        }

        /// <summary>
        /// json 转为 对象或list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IList<T> SortAndFilter<T>(IList<T> _list, common_Trait_Filtering filter)
        {
            IList<T> sortedList = new List<T>();
            if (filter.sortby != null && filter.sortby != "")
            {
                if (_list.Count == 0) return _list;
                PropertyInfo property = typeof(T).GetProperty(filter.sortby);
                if (property == null)
                {
                    return _list;
                }
                sortedList = filter.ascending ? _list.OrderBy(x => property.GetValue(x)).ToList() : _list.OrderByDescending(x => property.GetValue(x)).ToList();
            }
            else
            {
                sortedList = _list;
            }
            if (filter.offset != null && filter.offset != "")
            {
                int intOffSet = 0;
                if (int.TryParse(filter.offset, out intOffSet))
                {
                    if (intOffSet < sortedList.Count)
                    {
                        sortedList= sortedList.Skip(intOffSet).ToList();
                    }
                }
            }
            if (filter.baseID != null && filter.baseID != "")
            {
                int intOffSet = 0;
                if (int.TryParse(filter.offset, out intOffSet))
                {
                    if (intOffSet < sortedList.Count)
                    {
                        return sortedList.Skip(intOffSet).ToList();
                    }
                }
            }
            return sortedList;
        }

        /// <summary>
        /// 获取服务类型名称
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string getServiceType(Model.ServiceType st)
        {
            string str = st.ToString();//Model.ServiceType里面重写了ToString
            string strType = "";
            while (st != null)
            {
                strType = ">" + strType;
                strType = st.Name + strType;
                st = st.Parent;
            }
            return strType.TrimEnd('>');
        }

        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="_input_charset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string SignMD5(string prestr, string key, string _input_charset)
        {
            StringBuilder sb = new StringBuilder(32);

            prestr = prestr + key;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
