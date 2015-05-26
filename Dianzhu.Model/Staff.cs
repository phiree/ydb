using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 商家职员信息
    /// </summary>
   public  class Staff
    {
       public virtual Guid Id { get; set; }
       /// <summary>
       /// 所属商家
       /// </summary>
       public virtual Business Belongto { get; set; }
       /// <summary>
       /// 编号
       /// </summary>
       public virtual string Code{get;set;}
       public virtual string Name { get; set; }
       /// <summary>
       /// 昵称
       /// </summary>
       public virtual string NickName { get; set; }
       public virtual string Gender { get; set; }
       public virtual string Phone { get; set; }
       /// <summary>
       /// 职员照片
       /// </summary>
       public virtual string Photo { get; set; }
   
       /// <summary>
       /// 职员的服务分类
       /// </summary>
       public virtual IList<ServiceType> ServiceTypes { get; set; }

       
       
    }
}
