using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.DemoClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = "20141220-pc";
        public static XmppClientConnection XMPPConnection = null;
        public static string APIBaseURL = "http://localhost:805/DianzhuApi.ashx";

        static GlobalViables()
        {
            XMPPConnection = new XmppClientConnection(ServerName);
        }
       
    }
}
