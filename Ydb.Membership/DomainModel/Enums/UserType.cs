using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.Enums
{
    public enum UserType
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        customer = 1,
        /// <summary>
        /// 商户
        /// </summary>
        business = 2,
        /// <summary>
        /// 客服
        /// </summary>
        customerservice = 4,
        /// <summary>
        /// 管理员
        /// </summary>
        admin = 8,
        /// <summary>
        /// 员工
        /// </summary>
        staff = 16,
        /// <summary>
        /// 代理商
        /// </summary>
        agent = 32,
        /// <summary>
        /// 点点
        /// </summary>
        diandian = 64,
        /// <summary>
        /// 通知服务器
        /// </summary>
        notify = 128,
        /// <summary>
        /// openfire服务器
        /// </summary>
        openfire = 256
    }
    
}
