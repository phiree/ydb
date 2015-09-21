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
         
        /// <summary>
        /// 请不要直接调用此方法创建订单.
        /// </summary>
        protected  ServiceOrder()
        {
            OrderStatus = enum_OrderStatus.Draft;
            OrderCreated = DateTime.Now;
            Staff = new List<Staff>();
            
        }
        /// <summary>
        /// 系统内服务,系统内用户.
        /// </summary>
        /// <param name="service"></param>
        public static ServiceOrder Create(enum_ServiceScopeType scopeType, 
            DZService service, DZMembership member,
            string targetAddress,int unitAmount,decimal orderAmount
            )
        {
            if (scopeType != enum_ServiceScopeType.ISIM)
            {
                throw new Exception("传入参数有误");
            }

            ServiceOrder order = new ServiceOrder();
            order.ScopeType = scopeType;
            RedundantService(service, order);
            RedundantMembership(member, order);
            AppendOrderInfo(order,targetAddress, unitAmount, orderAmount);
  
            return order;
        }
        /// <summary>
        /// 系统内服务,系统外客户
        /// </summary>
        /// <param name="scopeType"></param>
        /// <param name="service"></param>
        /// <param name="member"></param>
        /// <param name="targetAddress"></param>
        /// <param name="unitAmount"></param>
        /// <param name="orderAmount"></param>
        /// <returns></returns>
        public static ServiceOrder Create(enum_ServiceScopeType scopeType, 
            DZService service, 
            string customerName,string customerPhone,string customerEmail,
            string targetAddress, int unitAmount, decimal orderAmount
            )
        {
            if (scopeType != enum_ServiceScopeType.ISIM)
            {
                throw new Exception("传入参数有误");
            }

            ServiceOrder order = new ServiceOrder();
            order.ScopeType = scopeType;
            AppendMemberInfo(order, customerName, customerPhone, customerEmail);
            RedundantService(service, order);
            AppendOrderInfo(order,targetAddress, unitAmount, orderAmount);

            return order;
        }
        /// <summary>
        /// 系统外服务,系统内用户
        /// </summary>
        /// <param name="scopeType"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceBusinessName"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceUnitPrice"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="member"></param>
        /// <param name="targetAddress"></param>
        /// <param name="unitAmount"></param>
        /// <param name="orderAmount"></param>
        /// <returns></returns>
        public static ServiceOrder Create(enum_ServiceScopeType scopeType,
           string serviceName,string serviceBusinessName,string serviceDescription,decimal serviceUnitPrice,string serviceUrl,
           DZMembership member,
           string targetAddress, int unitAmount, decimal orderAmount
           )
        {
            if (scopeType != enum_ServiceScopeType.OSIM)
            {
                throw new Exception("传入参数有误");
            }

            ServiceOrder order = new ServiceOrder();
            order.ScopeType = scopeType;
            AppendServiceInfo(order, serviceName, serviceBusinessName, serviceDescription, serviceUnitPrice, serviceUrl);
            RedundantMembership(member, order);
            AppendOrderInfo(order,targetAddress, unitAmount, orderAmount);

            return order;
        }
        /// <summary>
        /// 系统外服务,系统外用户.
        /// </summary>
        /// <param name="scopeType"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceBusinessName"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceUnitPrice"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="customerName"></param>
        /// <param name="customerPhone"></param>
        /// <param name="customerEmail"></param>
        /// <param name="targetAddress"></param>
        /// <param name="unitAmount"></param>
        /// <param name="orderAmount"></param>
        /// <returns></returns>
        public static ServiceOrder Create(enum_ServiceScopeType scopeType,
           string serviceName, string serviceBusinessName, string serviceDescription, decimal serviceUnitPrice, string serviceUrl,
            string customerName, string customerPhone, string customerEmail,
           string targetAddress, int unitAmount, decimal orderAmount
           )
        {
            if (scopeType != enum_ServiceScopeType.OSIM)
            {
                throw new Exception("传入参数有误");
            }

            ServiceOrder order = new ServiceOrder();
            order.ScopeType = scopeType;
            AppendMemberInfo(order, customerName, customerPhone, customerEmail);
            AppendServiceInfo(order, serviceName, serviceBusinessName, serviceDescription, serviceUnitPrice, serviceUrl);
          
            AppendOrderInfo(order,targetAddress, unitAmount, orderAmount);

            return order;
        }
        private static void AppendMemberInfo(ServiceOrder order, string customerName, string customerPhone, string customerEmail)
        {
            order.CustomerEmail = customerEmail;
            order.CustomerName = customerName;
            order.CustomerPhone = customerPhone;
        }
        private static void AppendServiceInfo(ServiceOrder order, string serviceName, string serviceBusinessName, string serviceDescription, decimal serviceUnitPrice, string serviceUrl)
        {
            order.ServiceName = serviceName;
            order.ServiceDescription = serviceDescription;
            order.ServiceBusinessName = serviceBusinessName;
            order.ServiceUnitPrice = serviceUnitPrice;
            order.ServiceURL = serviceUrl;
        }
        /// <summary>
        /// 订单的基础信息.
        /// </summary>
        private static void AppendOrderInfo(ServiceOrder order, string targetAddress,int unitAmount,decimal orderAmount)
        {
            order.TargetAddress = targetAddress;
            order.UnitAmount = unitAmount;
            order.OrderAmount = orderAmount;
             
        }
         
        /// <summary>
        /// 服务数据冗余
        /// </summary>
        /// <param name="service"></param>
        /// <param name="order"></param>
        private static void RedundantService(DZService service, ServiceOrder order)
        {
            order.Service = service;
            order.ServiceName = service.Name;
            order.ServiceDescription = service.Description;
            order.ServiceBusinessName = service.Business.Name;
            order.ServiceUnitPrice = service.UnitPrice;
        }
        /// <summary>
        /// 客户数据冗余
        /// </summary>
        /// <param name="member"></param>
        /// <param name="order"></param>
        private static void RedundantMembership(DZMembership member, ServiceOrder order)
        {
            order.Customer = member;
            order.CustomerEmail = member.Email;
            order.CustomerName = member.UserName;
            order.CustomerPhone = member.Phone;
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
        /// 订单备注.
        /// </summary>
        public virtual string Memo { get; set; }
        /// <summary>
        /// 订单状态.
        /// </summary>

        public virtual enum_OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 服务的目标地址
        /// </summary>
        public virtual string TargetAddress { get; set; }
        /// <summary>
        /// 订单的时间要求
        /// </summary>
        public virtual string TargetTime { get; set; }
        /// <summary>
        /// 分配的职员
        /// </summary>
        public virtual IList<Staff> Staff { get; set; }
        /// <summary>
        /// 服务总数, 用来计算总价 service.unitprice*unitamount
        /// </summary>
        public virtual int UnitAmount { get; set; }
        /// <summary>
        ///订单总价
        /// </summary>
        public virtual decimal OrderAmount { get; set; }
        /// <summary>
        /// 外部服务的链接
        /// </summary>
        public virtual string ServiceURL { get; set; }
        #region 服务冗余信息,
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDescription { get; set; }
        public virtual string ServiceBusinessName { get; set; }
        public virtual decimal ServiceUnitPrice { get; set; }
       
        
        #endregion
        #region 客户冗余信息,一定是注册用户
        public virtual string CustomerName { get; set; }
        public virtual string CustomerPhone { get; set; }
        public virtual string CustomerEmail { get; set; }
        #endregion

        public virtual enum_ServiceScopeType ScopeType { get; set; }
    

    }
 
     

}
