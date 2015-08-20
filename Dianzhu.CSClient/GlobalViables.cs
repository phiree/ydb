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
        public static readonly string Domain = ConfigurationManager.AppSettings["domain"];
        public static readonly string ButtonNamePrefix = "btn";
        public static XmppClientConnection XMPPConnection = null;
        static GlobalViables()
        {
            
            XMPPConnection = new XmppClientConnection(ServerName);
        }
        /// <summary>
        /// 当前登录的客服
        /// </summary>
        public static Model.DZMembership CurrentCustomerService = null;
       
    }
}
