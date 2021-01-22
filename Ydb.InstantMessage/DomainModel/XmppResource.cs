using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Enums
{
    public enum XmppResource
    {
        /// <summary>
        /// 用户版
        /// </summary>
        YDBan_User = 50,
        /// <summary>
        /// 商户版
        /// </summary>
        YDBan_Store = 52,
        /// <summary>
        /// 客服工具 
        /// </summary>
        YDBan_CustomerService = 54,
        /// <summary>
        /// IM服务
        /// </summary>
        YDBan_IMServer = 56,
        /// <summary>
        /// 点点
        /// </summary>
        YDBan_DianDian = 58,
        /// <summary>
        /// 员工
        /// </summary>
        YDBan_Staff = 60,
        /// <summary>
        /// 未知资源名称
        /// </summary>
        Unknow = 100,
    }
}
