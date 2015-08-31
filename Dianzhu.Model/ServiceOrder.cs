using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
namespace Dianzhu.Model
{
    /// <summary>
    /// 订单
    /// </summary>
    public class ServiceOrder
    {

        public ServiceOrder()
        {
            OrderStatus = enum_OrderStatus.Created;
            OrderCreated = DateTime.Now;
        }
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 订单关联的服务,可以为空.
        /// </summary>
        public virtual DZService Service { get; set; }
        
        /// <summary>
        /// 客户,可以为空
        /// </summary>
        public virtual DZMembership Customer { get; set; }
 
        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime OrderCreated { get; set; }

        /// <summary>
        /// 订单结束的时间.
        /// </summary>
        public virtual DateTime OrderFinished { get; set; }
      /// <summary>
      /// 订单状态.
      /// </summary>
        
        public virtual enum_OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 服务的目标地址
        /// </summary>
        public virtual string TargetAddress { get; set; }
        /// <summary>
        /// 分配的职员
        /// </summary>
        public virtual IList<Staff> Staff { get; set; }
        /// <summary>
        /// 服务总数, 用来计算总价 service.unitprice*unitamount
        /// </summary>
        public virtual int UnitAmount { get; set; }
        /// <summary>
        /// 服务的详细网址(平台外,平台内)
        /// </summary>
       

        #region 服务冗余信息
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDescription { get; set; }
        public virtual string ServiceBusinessName { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual string ServiceURL { get; set; }
        
        #endregion
        #region 客户冗余信息,一定是注册用户
        public virtual string CustomerName { get; set; }
        public virtual string CustomerPhone { get; set; }
        public virtual string CustomerEmail { get; set; }

        #endregion

    }
 
     

}
