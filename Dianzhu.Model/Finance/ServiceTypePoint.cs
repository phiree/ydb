using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Finance
{
    /// <summary>
    /// 服务分类的扣点比例
    /// </summary>
    public class ServiceTypePoint
    {
        public Guid Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal Point { get; set; }
        
    }
}
