using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.Application
{
    public class UserTypeSharePointDto : Entity<Guid>
    {
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
