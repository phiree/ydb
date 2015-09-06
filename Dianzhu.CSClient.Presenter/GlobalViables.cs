using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using System.Configuration;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.CSClient.Presenter
{
    
    public class GlobalViables
    { 
        public static readonly string ButtonNamePrefix = "btn";

        
        /// <summary>
        /// 当前登录的客服
        /// </summary>
         /// <summary>
         /// 当前登录的客服
         /// </summary>
         public static DZMembership CurrentCustomerService = null;
        
    }
}
