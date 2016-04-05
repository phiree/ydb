using agsXMPP;

namespace DianzhuService.Diandian
{

    public class GlobalViables
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Diandian");
        public static readonly string ServerName =Dianzhu.Config.Config.GetAppSetting("ImServer");  //home'pc: "20141220-pc";
        public static readonly string DomainName = Dianzhu.Config.Config.GetAppSetting("ImDomain");  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = Dianzhu.Config.Config.GetAppSetting("APIBaseURL");
        public static string MediaUploadUrl = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        public static string MediaGetUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl");
        static GlobalViables()
        {
            log.Debug("xmpp服务初始化");
           
            XMPPConnection = new XmppClientConnection();
            XMPPConnection.Server = DomainName;
            XMPPConnection.ConnectServer = ServerName;
            XMPPConnection.AutoResolveConnectServer = false;
            
            XMPPConnection.Resource = Dianzhu.Model.Enums.enum_XmppResource.YDBan_DianDian.ToString();
           
        }
       
    }
}
