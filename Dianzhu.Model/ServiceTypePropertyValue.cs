using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 类别属性的值
    /// </summary>
   public class ServicePropertyValue
    {
       public virtual Guid Id { get; set; }
       /// <summary>
       /// 所属服务属性.
       /// </summary>
       public virtual ServiceProperty ServiceProperty { get; set; }
       public virtual string PropertyValue { get; set; }
    }
}
