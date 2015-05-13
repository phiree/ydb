using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dianzhu.Model
{
    /// <summary>
    /// 商户
    /// </summary>
    public class Business_Abs
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }

    /// <summary>
    /// 商家
    /// </summary>
   public class Business:Business_Abs
    {
       /// <summary>
       /// 公司地址
       /// </summary>
       public virtual string Address{ get; set; }
       
       /// <summary>
       /// 店铺地理坐标,精度，维度。
       /// </summary>
       public virtual double Longitude { get; set; }
       public virtual double Latitude { get; set; }
       /// <summary>
       /// 商家介绍
       /// </summary>
       public virtual string Description { get; set; }

       /// <summary>
       /// 是否通过了审核.
       /// </summary>
       public virtual bool IsApplyApproved { get; set; }
       /// <summary>
       /// 审核拒绝信息.
       /// </summary>
       public virtual string ApplyRejectMessage { get; set; }
      
       

    }
}
