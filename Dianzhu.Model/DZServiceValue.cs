using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 具体服务项目所属类别的属性值.
    /// </summary>
  public  class DZServiceValue
    {
      public virtual Guid Id { get; set; }
      public virtual DZService DZService {get;set;}
      public virtual ServicePropertyValue ServicePropertyValue { get; set; }
    }
}
