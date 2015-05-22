using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 具体某项服务的定义
    /// </summary>
   public class DZService
    {
       public virtual Guid Id { get; set; }
       /// <summary>
       /// 服务项目所属类别
       /// </summary>
       public virtual ServiceType ServiceType { get; set; }
       /// <summary>
       /// 服务名称
       /// </summary>
       public virtual string Name { get; set; }
       /// <summary>
       /// 所属商家
       /// </summary>
       public virtual Business Business { get; set; }
       /// <summary>
       /// 详细描述
       /// </summary>
       public virtual string Description { get; set; }
 
        

    }
}
