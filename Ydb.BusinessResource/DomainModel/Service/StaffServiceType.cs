using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 订单分配详情.
    /// </summary>
    public   class StaffServiceType
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 提供的服务
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Staff Staff { get; set; }
    }
}
