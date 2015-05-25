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
        public ServiceType()
        {
            Properties = new List<ServiceProperty>();
            Children = new List<ServiceType>();
        }
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 上级类型
        /// </summary> 
        public virtual ServiceType Parent { get; set; }
        /// <summary>
        /// 层级结构中的层数.
        /// </summary>
        public virtual int DeepLevel { get; set; }
        /// <summary>
        /// 直接子类型.
        /// </summary>
        public virtual IList<ServiceType> Children { get; set; }
        public virtual IList<ServiceProperty> Properties { get; set; }
        

        
    }
}
