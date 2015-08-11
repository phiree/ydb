using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using System.Configuration;
namespace Dianzhu.CSClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = ConfigurationManager.AppSettings["server"];
        public static XmppClientConnection XMPPConnection = null;
        static GlobalViables()
        {
            
            XMPPConnection = new XmppClientConnection(ServerName);
        }
        public static string CurrentUserName = string.Empty;
       
    }
}
