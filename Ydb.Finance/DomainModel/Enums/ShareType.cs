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
        None=1,
        /// <summary>
        /// 助理
        /// </summary>
        CustomerService=2,
        /// <summary>
        /// 区域代理
        /// </summary>
        AreaAgent=4
    }
}
