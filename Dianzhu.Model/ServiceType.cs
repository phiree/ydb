using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public class ServiceType
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 上级类型
        /// </summary>
        public ServiceType Parent { get; set; }
        
    }
}
