
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
        static int num =3;
        static int payServerNum = 4;
        #region 服务器定义
        static string[] IMServers = new string[] { "localhost", "119.29.39.211", "115.159.72.236", "192.168.1.172","192.168.1.150" };
        static string[] IMDomains = new string[] { "localhost", "119.29.39.211", "business.ydban.cn", "192.168.1.172", "192.168.1.150" };
        static string[] ApplicationServers = new string[] { "localhost", "119.29.39.211", "business.ydban.cn", "192.168.1.172", "192.168.1.150" };
        static string[] HttpApiServers = new string[] { "localhost", "119.29.39.211", "business.ydban.cn", "192.168.1.172", "192.168.1.150" };
        static string[] IMNotifyServers = new string[] { "localhost", "119.29.39.211", "business.ydban.cn", "192.168.1.172", "192.168.1.150" };
        static string[] PayServers = new string[] { "localhost", "119.29.39.211", "business.ydban.cn", "192.168.1.172", "jsyklan.asuscomm.com" };
        #endregion
        #region   部署前，只需要手动修改此处 /

        static string IMServer = IMServers.GetValue(num).ToString();//即时通讯服务器地址
        static string IMDomain = IMDomains.GetValue(num).ToString();//即时通讯服务器地址
        static string HttpApiServer = HttpApiServers.GetValue(num).ToString();//即时通讯服务器地址
        static string ApplicationServer = ApplicationServers.GetValue(num).ToString();//应用服务器地址
        static string IMNotifyServer = IMNotifyServers.GetValue(num).ToString();//通知服务服务地址

        #endregion
        static log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Config");
        

       
         

        static Dictionary<string, KeyValuePair<string, string>> DictsDianDianLogins = new Dictionary<string, KeyValuePair<string, string>>() {
              { "119.29.39.211",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
             ,{ "business.ydban.cn",new KeyValuePair<string,string>("dc73ba0f-91a4-4e14-b17a-a567009dfd6a","diandian") }
             ,{ "192.168.1.172",new KeyValuePair<string,string>("c64d9dda-4f6e-437b-89d2-a591012d8c65","123456") }
            ,{ "localhost",new KeyValuePair<string,string>("09be8ea0-f353-4527-81d8-a604009fc5d2","123456") }
               ,{ "192.168.1.150",new KeyValuePair<string,string>("3d9ed306-7b5d-4e0d-8776-a5e50093515f","123456") }
        };
        //通知中心登陆用户账号，不同数据库服务器有不同的值
        //todo: 需要使用 username登陆 而不是id
        static Dictionary<string, KeyValuePair<string, string>> DictsNotifySenderLogins = new Dictionary<string, KeyValuePair<string, string>>() {
              { "119.29.39.211",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
             ,{ "business.ydban.cn",new KeyValuePair<string,string>("c6b13498-2259-4ff3-a75e-a4f90123683c","123456") }
             ,{ "192.168.1.172",new KeyValuePair<string,string>("fa7ef456-0978-4ccd-b664-a594014cbfe7","123456") }
              ,{ "localhost",new KeyValuePair<string,string>("30d16607-fbac-41a3-b4de-a60500c19756","198989") }
              ,{ "192.168.1.150",new KeyValuePair<string,string>("054c400b-4afb-49f3-bbdf-a5e500939c59","123456") }
        };
         

        static Dictionary<string, string> DictsAppSettings = new Dictionary<string, string>() {
             {"cdnroot", BuildHttpUrlString(ApplicationServer, 886)}
            , {"ImServer",IMServer  }
            , {"ImDomain",IMDomain  }

            , {"MediaUploadUrl",BuildHttpUrlString(ApplicationServer, 8038,"UploadFile.ashx") }
            , {"MediaGetUrl",BuildHttpUrlString(ApplicationServer, 8038,"GetFile.ashx?fileName=")   }


            , {"NoticeSenderId",DictsNotifySenderLogins[IMNotifyServer].Key  }
            , {"NoticeSenderPwd",DictsNotifySenderLogins[IMNotifyServer].Value  }

            , {"PaySite",BuildHttpUrlString(PayServers[payServerNum], 8168)   }

            , {"OpenfireRestApiSessionListUrl",BuildHttpUrlString(IMServer, 9090,"plugins/restapi/v1/sessions/")  }

            , {"DiandianLoginId",DictsDianDianLogins[IMNotifyServer].Key}
            , {"DiandianLoginPwd",DictsDianDianLogins[IMNotifyServer].Value  }
            , {"APIBaseURL",BuildHttpUrlString(HttpApiServer, 8037,"DianzhuApi.ashx")  }
            
            , {"NotifyServer",BuildHttpUrlString(IMNotifyServer, 8039, "IMServerAPI.ashx?")   }
            , {"BaiduGeocodingAPI","http://api.map.baidu.com/geocoder/v2/?ak="  }
            , {"BaiduTranAPI","http://api.map.baidu.com/geoconv/v1/?ak="  }
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
        };

        static private string BuildHttpUrlString(string server)
        {
            return string.Format("http://{0}/", server);
        }
        static private string BuildHttpUrlString(string server, int port)
        {
            return string.Format("http://{0}:{1}/", server, port);
        }
        static private string BuildHttpUrlString(string server, int port, string path)
        {
            return string.Format("http://{0}:{1}/{2}", server, port, path);
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
