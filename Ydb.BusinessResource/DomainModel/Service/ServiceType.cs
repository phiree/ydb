using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Ydb.Common.Domain;
namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 服务类型
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServiceType: Entity<Guid>
    {
        public ServiceType()
        {
            
            Children = new List<ServiceType>();
           
        }
        [JsonProperty(PropertyName = "id")]
        public virtual Guid TypeId {
            get { return this.Id; }
        }
        
        [JsonProperty(PropertyName = "name")]
        public virtual string Name { get; set; }
        /// <summary>
        /// id
        /// </summary>
         
        /// <summary>
        /// 自定义编码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public virtual string Code { get; set; }
        /// <summary>
        /// 上级类型
        /// </summary> 
        
        public virtual ServiceType Parent { get; set; }
        /// <summary>
        /// json序列化要求的字段.
        /// </summary>
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Guid ParentId { get { return Parent==null ?Guid.Empty:  Parent.Id; } }
        /// <summary>
        /// 层级结构中的层数.
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public virtual int DeepLevel { get; set; }
        /// <summary>
        /// 直接子类型.
        /// </summary>
        public virtual IList<ServiceType> Children { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int OrderNumber { get; set; }
       
        public virtual ServiceType TopType {
            get {
                if (this.Parent == null)
                {
                    return this;
                }
                else {

                    ServiceType theParent =this.Parent;
                    while (theParent.DeepLevel !=0)
                    {
                        theParent = this.Parent.Parent;
                    }
                    return theParent;

                }
            }
        }

        //private string fullTypeName = string.Empty;
        public override string ToString()
        {
            string fulleTypeName = Name;
            ServiceType parent = this.Parent;
            while(parent!=null)
            {
                fulleTypeName = parent.Name + ">" + fulleTypeName;

                parent = parent.Parent;
            }
            
            return fulleTypeName;

        }
        
        


        
    }
}
