using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dianzhu.Model
{
    /// <summary>
    /// 商户基类.泛指可以提供服务的单位,可以是公司 也可以是个人
    /// </summary>
    public class Business_Abs
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public virtual string Contact { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Description { get; set; }
        
    }

    /// <summary>
    /// 商家
    /// </summary>
   public class Business:Business_Abs
    {
       /// <summary>
       /// 公司所在辖区
       /// </summary>
       public virtual Area AreaBelongTo { get; set; }
       /// <summary>
       /// 公司服务范围
       /// </summary>
       public virtual IList<Area> AreaServiceTo { get; set; }
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
       /// 是否通过了审核.
       /// </summary>
       public virtual bool IsApplyApproved { get; set; }
       /// <summary>
       /// 审核拒绝信息.
       /// </summary>
       public virtual string ApplyRejectMessage { get; set; }
       /// <summary>
       /// 申请日期
       /// </summary>
       public virtual DateTime DateApply { get; set; }
       /// <summary>
       /// 审核通过日期
       /// </summary>
       public virtual DateTime DateApproved { get; set; }
    }
}
