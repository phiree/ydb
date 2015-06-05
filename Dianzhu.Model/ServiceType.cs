using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Dianzhu.Model
{
    /// <summary>
    /// 服务类型
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServiceType
    {
        public ServiceType()
        {
            Properties = new List<ServiceProperty>();
            Children = new List<ServiceType>();
        }
        [JsonProperty(PropertyName = "id")]
        public virtual Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
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
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int OrderNumber { get; set; }
        public virtual IList<ServiceProperty> Properties { get; set; }
        //private string fullTypeName = string.Empty;
        public override string ToString()
        {
            string fulleTypeName = Name;
            ServiceType parent = this.Parent;
            while(parent!=null)
            {
                fulleTypeName = Parent.Name + ">" + fulleTypeName;

                parent = parent.Parent;
            }
            
            return fulleTypeName;

        }
        

        
    }
}
