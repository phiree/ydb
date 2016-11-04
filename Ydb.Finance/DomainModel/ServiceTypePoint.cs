using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
namespace Ydb.Finance.DomainModel
{
    /// <summary>
    /// 服务分类的平台扣点比例
    /// </summary>
    public class ServiceTypePoint : Entity<Guid>
    {
        /// <summary>
        /// 服务类型ID
        /// </summary>
        public virtual string  ServiceTypeId { get; set; }

        /// <summary>
        /// 服务扣点比例
        /// </summary>
        public virtual decimal Point { get; set; }

    }
}
