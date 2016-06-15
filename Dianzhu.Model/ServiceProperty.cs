﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务类型的属性. 比如 保姆这个类别,需要具备的属性
    /// </summary>
   public class ServiceProperty
    {
       public ServiceProperty()
       {
           Values = new List<ServicePropertyValue>();
       }
       public virtual Guid Id { get; set; }
       public virtual string Name { get; set; }
       /// <summary>
       /// 所属类别
       /// </summary>
       public virtual ServiceType ServiceType { get; set; }
       /// <summary>
       /// 商家自定义的属性.
       /// </summary>
       public virtual Business BelongsTo { get; set; }
       public virtual IList<ServicePropertyValue> Values { get; set; }

       public virtual string GetValuesStringFormat()
       {
           string r = string.Empty;
           foreach (ServicePropertyValue v in Values)
           {
               r += v.PropertyValue + ",";
           }
           return r = r.TrimEnd(',');
       }
    }
}
