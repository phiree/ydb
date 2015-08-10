using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = "yuanfei-pc";
        public static XmppClientConnection XMPPConnection = null;
        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
        }
       
    }
}
