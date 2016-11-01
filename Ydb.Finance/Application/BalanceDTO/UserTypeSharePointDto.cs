using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class UserTypeSharePointDto 
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual string UserType { get; set; }

        /// <summary>
        /// 分账比例
        /// </summary>
        public virtual decimal Point { get; set; }
    }
}
