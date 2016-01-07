using agsXMPP;

namespace DianzhuService.Diandian
{

    public class GlobalViables
    {
        public static readonly string ServerName =Dianzhu.Config.Config.GetAppSetting("ImServer");  //home'pc: "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = Dianzhu.Config.Config.GetAppSetting("APIBaseURL");
        public static string MediaUploadUrl = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        public static string MediaGetUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl");
        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
            XMPPConnection.Resource = Dianzhu.Model.Enums.enum_XmppResource.YDBan_Win_DianDian.ToString();
            XMPPConnection.AutoResolveConnectServer = false;
        }
       
    }
}
