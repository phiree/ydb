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
       public Staff() {

           StaffAvatar = new List<BusinessImage>();
       }
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
       public virtual int Age { get; set; }
       /// <summary>
       /// 工作年数
       /// </summary>
       public virtual int WorkingYears { get; set; }
       /// <summary>
       /// 昵称
       /// </summary>
       public virtual string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
       public virtual string Gender { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
       public virtual string Phone { get; set; }
       /// <summary>
       /// 职员照片
       /// </summary>
       public virtual string Photo { get; set; }

        /// <summary>
        /// 是否已经被分配.
        /// </summary>
       public virtual bool IsAssigned { get; set; }
   
       /// <summary>
       /// 职员的服务分类
       /// </summary>
       public virtual IList<ServiceType> ServiceTypes { get; set; }
       /// <summary>
       /// 职员的头像. 可能会有多个,但是只有一个是
       /// </summary>
       public virtual IList<BusinessImage> StaffAvatar { get; set; }
       public virtual BusinessImage AvatarCurrent {
           get {
               IList<BusinessImage> list = StaffAvatar.Where(x => x.IsCurrent).ToList();
               if (list.Count ==0)
               {
                   return null;
               }
               else if (list.Count == 1)
               {
                   return list[0];
               }
               else
               {
                   throw new Exception("错误:用户有多个头像");
               }
           }
       }
       
    }
    
}
