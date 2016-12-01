using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using Ydb.Common;

namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.DemoClient");
        public static readonly string ServerName =Dianzhu.Config.Config.GetAppSetting("ImServer");  //home'pc: "20141220-pc";
        public static readonly string DomainName = Dianzhu.Config.Config.GetAppSetting("ImDomain");  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = Dianzhu.Config.Config.GetAppSetting("APIBaseURL");
        public static string MediaUploadUrl = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        public static string MediaGetUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl");
        static GlobalViables()

        {


            XMPPConnection = new XmppClientConnection(ServerName);
            XMPPConnection.Server = DomainName;
            XMPPConnection.ConnectServer = ServerName;
            XMPPConnection.Resource = enum_XmppResource.YDBan_User.ToString();
            XMPPConnection.AutoResolveConnectServer = false;


        }
      public  static bool CheckConfig()
        {
            log.Debug("--开始 检查配置是否冲突");
            //need: openfire服务器 数据库,api服务器,三者目标ip应该相等.
            bool isValidConfig = false;
           
             string ofserver = Dianzhu.Config.Config.GetAppSetting("ImServer");
            System.Text.RegularExpressions.Match m2 = System.Text.RegularExpressions.Regex.Match(Dianzhu.Config.Config.GetAppSetting("APIBaseURL"), "(?<=https?://).+?(?=:" + Dianzhu.Config.Config.GetAppSetting("GetHttpAPIPort") + ")");

            if (ofserver ==  m2.Value)
            {
                isValidConfig = true;
            }
            else
            {
                log.Error(  m2.Value + "," + ofserver);
            }

            return isValidConfig;




        }

    }
}
