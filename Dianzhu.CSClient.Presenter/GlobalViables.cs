using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using System.Configuration;
 
namespace Dianzhu.CSClient.Presenter
{
    
    public class GlobalViables
    {
        public static readonly string ButtonNamePrefix = "btn";
        
         /// <summary>
         /// 当前登录的客服
         /// </summary>
         public static DZMembership CurrentCustomerService = null;
        public static DZMembership Diandian = null;
        public static string MediaUploadUrl = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        public static string MediaGetUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl");
        public static readonly string LocalMediaSaveDir =Environment.CurrentDirectory+ @"\localmedia\";

    }
}
