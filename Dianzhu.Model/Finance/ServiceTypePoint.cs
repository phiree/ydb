using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Finance
{
    /// <summary>
    /// 服务分类的扣点比例
    /// </summary>
    public class ServiceTypePoint:DDDCommon.Domain.Entity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual decimal Point { get; set; }
        
    }
}
