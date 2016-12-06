
using System.Collections.Generic;

using System.Configuration;
using System.Diagnostics;
namespace Dianzhu.Config
{
    /// <summary>
    ///  服务器环境配置。
    /// </summary>
    public static partial class Config
    {
        #region 服务器定义
        //本地服务器,局域网测试服务器,远程服务器,正式服务器
        static string[] IMServers = new string[]            { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "192.168.1.172" };
        static string[] IMDomains = new string[]            { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "192.168.1.172" };
        static string[] ApplicationServers = new string[]   { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "192.168.1.172" };
        static string[] HttpApiServers = new string[]       { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "192.168.1.172" };
        static string[] IMNotifyServers = new string[]      { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "192.168.1.172" };
        static string[] PayServers = new string[]           { "localhost",  "192.168.1.150", "dev.ydban.cn", "business.ydban.cn", "112.66.184.232" };
        

        static string IMServer = IMServers.GetValue(int.Parse(ConfigurationManager.AppSettings["ServerNum"])).ToString(); 
        static string IMDomain = IMDomains.GetValue(int.Parse(ConfigurationManager.AppSettings["ServerNum"])).ToString(); 
        static string HttpApiServer = HttpApiServers.GetValue(int.Parse(ConfigurationManager.AppSettings["ServerNum"])).ToString(); 
        static string ApplicationServer = ApplicationServers.GetValue(int.Parse(ConfigurationManager.AppSettings["ServerNum"])).ToString(); 
        static string IMNotifyServer = IMNotifyServers.GetValue(int.Parse(ConfigurationManager.AppSettings["ServerNum"])).ToString(); 

        #endregion
        static log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Config");

        //配置访问协议(http/https)的访问端口
        static Dictionary<string, int>HttpPort = new Dictionary<string, int>()
        {
            { "cdnroot",886 },
            { "RestApiAuthUrl",8041 },
            { "MediaUploadUrl",8038 },
            { "MediaGetUrl",8038 },
            { "MediaUploadUrlByDate",8038 },
            { "PaySite",8168 },
            { "OpenfireRestApiSessionListUrl",9090 },
            { "APIBaseURL",8037 },
            { "NotifyServer",8039 }
        };
        static Dictionary<string, int> HttpsPort = new Dictionary<string, int>()
        {
            { "cdnroot",686 },
            { "RestApiAuthUrl",6041 },
            { "MediaUploadUrl",6038 },
            { "MediaGetUrl",6038 },
            { "MediaUploadUrlByDate",6038 },
            { "PaySite",6168 },
            { "OpenfireRestApiSessionListUrl",9091 },
            { "APIBaseURL",6037 },
            { "NotifyServer",6039 }
        };
        static bool UseHttps = bool.Parse(ConfigurationManager.AppSettings["UseHttps"]);
        static Dictionary<string, int> PortSet = UseHttps ? HttpsPort : HttpPort;
        static string strHttp = UseHttps ? "https" : "http";


        static Dictionary<string, KeyValuePair<string, string>> DictsDianDianLogins = new Dictionary<string, KeyValuePair<string, string>>() {
             { "localhost",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
            ,{ "dev.ydban.cn",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
            ,{ "business.ydban.cn",new KeyValuePair<string,string>("dc73ba0f-91a4-4e14-b17a-a567009dfd6a","123456") }
            ,{ "192.168.1.150",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
            ,{ "192.168.1.172",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
        };
        //通知中心登陆用户账号，不同数据库服务器有不同的值
        //todo: 需要使用 username登陆 而不是id
        static Dictionary<string, KeyValuePair<string, string>> DictsNotifySenderLogins = new Dictionary<string, KeyValuePair<string, string>>() {
             { "localhost",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
            ,{ "192.168.1.172",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
            ,{ "dev.ydban.cn",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
            ,{ "business.ydban.cn",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
            ,{ "192.168.1.150",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
        };


        static Dictionary<string, string> DictsAppSettings = new Dictionary<string, string>() {
             {"cdnroot", BuildHttpUrlString(ApplicationServer, PortSet["cdnroot"])}
            , {"ImServer",IMServer  }
            , {"ImDomain",IMDomain  }
            , {"RestApiAuthUrl",BuildHttpUrlString(ApplicationServer, PortSet["RestApiAuthUrl"],"api/v1/authorization")  }
            , {"MediaUploadUrl",BuildHttpUrlString(ApplicationServer, PortSet["MediaUploadUrl"],"UploadFile.ashx") }
            , {"MediaGetUrl",BuildHttpUrlString(ApplicationServer, PortSet["MediaGetUrl"],"GetFile.ashx?fileName=")   }
            , {"MediaUploadUrlByDate",BuildHttpUrlString(ApplicationServer, PortSet["MediaUploadUrlByDate"],"UploadFileByDate.ashx") }//按日期生成保存路径

            , {"ImageHandler",BuildHttpUrlString(ApplicationServer, "ImageHandler.ashx?imagename=")}

            , {"NoticeSenderId",DictsNotifySenderLogins[IMNotifyServer].Key  }
            , {"NoticeSenderPwd",DictsNotifySenderLogins[IMNotifyServer].Value  }

            , {"PaySite",BuildHttpUrlString(PayServers[int.Parse(ConfigurationManager.AppSettings["PayServerNum"])],PortSet["PaySite"])   }

            , {"OpenfireRestApiBaseUrl",BuildHttpUrlString(IMServer, PortSet["OpenfireRestApiSessionListUrl"],"plugins/restapi/v1/")  }

            , {"DiandianLoginId",DictsDianDianLogins[IMNotifyServer].Key}
            , {"DiandianLoginPwd",DictsDianDianLogins[IMNotifyServer].Value  }
            , {"APIBaseURL",BuildHttpUrlString(HttpApiServer, PortSet["APIBaseURL"],"DianzhuApi.ashx")  }

            , {"GetHttpAPIPort", PortSet["APIBaseURL"].ToString()}
            
            , {"NotifyServer",BuildHttpUrlString(IMNotifyServer, PortSet["NotifyServer"], "IMServerAPI.ashx?")   }
            , {"BaiduGeocodingAPI","https://api.map.baidu.com/geocoder/v2/?s=1&ak="  }//http://api.map.baidu.com/geocoder/v2/?ak=
            , {"BaiduTranAPI","https://api.map.baidu.com/geoconv/v1/?s=1&ak="  }//http://api.map.baidu.com/geoconv/v1/?ak=
            , {"BaiduGeocodingAK","SDHO8UtRNvOl4Cc29KA74UxF"  }
            , {"BaiduTranAK","McW4pZayH2PZyWqczoqj2xaV"  }
            , {"LocalMediaSaveDir","/localmedia/"  }
            , {"OrderTimeout","120"  }
            , {"WeChatAccessTokenUrl","https://api.weixin.qq.com/sns/oauth2/access_token?"  }
            , {"WeChatRefreshTokenUrl","https://api.weixin.qq.com/sns/oauth2/refresh_token?"  }
            , {"WeChatUserInfoUrl","https://api.weixin.qq.com/sns/userinfo?"  }
            , {"WeiboAccessTokenUrl","https://api.weibo.com/oauth2/access_token"  }
            , {"WeiboTokenUrl","https://api.weibo.com/oauth2/get_token_info"  }
            , {"WeiboBackUrl","https://api.weibo.com/oauth2/default.html"  }
            , {"WeiboUserUrl","https://api.weibo.com/2/users/show.json?"  }
            , {"QQOpenidUrl","https://graph.qq.com/oauth2.0/me?"  }
            , {"QQUserInfoUrl","https://graph.qq.com/user/get_user_info?"  }

            , {"CityId","2"  }
            , {"SecurityKey","1qaz2wsx3edc4rfv"  }//系统内部加密密钥
            , {"business_image_root","/media/business/"  }//图片保存
            , {"OpenfireRestApiAuthKey","an4P0ja6v3rykV4H"  }

            , {"IsAutoAssignCustomer","false"  }

            , {"BookOrderRefundTimes","-1" }//预约订单的退款时间,负数表示不退订金，非负数表示可退订金的时间
        };

        static private string BuildHttpUrlString(string server)
        {
            return string.Format(strHttp+"://{0}/", server);
        }
        static private string BuildHttpUrlString(string server, int port)
        {
            return string.Format(strHttp + "://{0}:{1}/", server, port);
        }
        static private string BuildHttpUrlString(string server, string path)
        {
            return string.Format(strHttp + "://{0}/{1}", server, path);
        }
        static private string BuildHttpUrlString(string server, int port, string path)
        {
            return string.Format(strHttp + "://{0}:{1}/{2}", server, port, path);
        }
        public static string GetAppSetting(string key)
        {
            string errMsg = string.Empty;
            if (DictsAppSettings.ContainsKey(key))
            {
                string settingValue = DictsAppSettings[key];
                if (!string.IsNullOrEmpty(settingValue))
                {

                }
                else
                {
                    errMsg = "配置节读取结果为空.";
                    ilog.Warn(errMsg);
                }
                return settingValue;
            }
            else
            {
                errMsg = "配置信息中没有找到对应的key：" + key;
                ilog.Error(errMsg);
                throw new System.Exception(errMsg);
            }

        }

    }

}
