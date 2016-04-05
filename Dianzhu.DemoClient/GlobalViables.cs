using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
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
            XMPPConnection.Resource = Model.Enums.enum_XmppResource.YDBan_DemoClient.ToString();
            XMPPConnection.AutoResolveConnectServer = false;

            
        }
       
    }
}
