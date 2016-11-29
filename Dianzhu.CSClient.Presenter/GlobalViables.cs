using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Membership.Application.Dto;

namespace Dianzhu.CSClient.Presenter
{
    
    public class GlobalViables
    {
        public static readonly string ButtonNamePrefix = "btn";
        /// <summary>
        /// 用户tab控件对应的常量前缀
        /// </summary>
        public const string PRE_TAB_CUSTOMER = "tab";

        /// <summary>
        /// 当前登录的客服
        /// </summary>
        public static MemberDto CurrentCustomerService = null;
        public static MemberDto Diandian = null;
        public static string MediaUploadUrl = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        public static string MediaGetUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl");
        public static readonly string LocalMediaSaveDir =Environment.CurrentDirectory+ @"\localmedia\";

    }
}
