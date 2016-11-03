using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public class ServiceTypePointDto 
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 服务类型ID
        /// </summary>
        public virtual string ServiceTypeId { get; set; }

        /// <summary>
        /// 服务扣点比例
        /// </summary>
        public virtual decimal Point { get; set; }
    }
}
