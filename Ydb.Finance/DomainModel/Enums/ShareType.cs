using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.DomainModel.Enums
{
    /// <summary>
    /// 参与分账的用户的类型
    /// </summary>
   public enum UserType
    {
        None=0,
        /// <summary>
        /// 商户
        /// </summary>
        business = 2,
        /// <summary>
        /// 客服
        /// </summary>
        customerservice = 4,
        /// <summary>
        /// 代理商
        /// </summary>
        agent = 32,
        /// <summary>
        /// 点点
        /// </summary>
        diandian = 64
    }
}
