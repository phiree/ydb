using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using System.Configuration;
 
namespace Dianzhu.CSClient.Presenter
{
    
    public class GlobalViables
    {  public static readonly string ButtonNamePrefix = "btn";

        
        /// <summary>
        /// 当前登录的客服
        /// </summary>
         /// <summary>
         /// 当前登录的客服
         /// </summary>
         public static DZMembership CurrentCustomerService = null;
        public static string MediaUploadUrl = "http://192.168.1.140:8033/ajaxservice/FileUploadCommon.ashx";
        public static string MediaRootUrl = "http://192.168.1.140:8033/";


    }
}
