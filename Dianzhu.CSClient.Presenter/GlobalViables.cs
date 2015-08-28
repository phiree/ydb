using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using System.Configuration;
using Dianzhu.CSClient.XMPP;
namespace Dianzhu.CSClient.Presenter
{
    
    public class GlobalViables
    {  public static readonly string ButtonNamePrefix = "btn";

        public static XMPP.XMPP XMPP = new XMPP.XMPP();
         static GlobalViables()
        {
          }
        /// <summary>
        /// 当前登录的客服
        /// </summary>
         /// <summary>
         /// 当前登录的客服
         /// </summary>
         public static DZMembership CurrentCustomerService = null;
       
    }
}
