using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using Dianzhu.DAL;
namespace Dianzhu.BLL
{

    /// <summary>
    /// 用户创建订单
    /// </summary>
    public class BLLServiceOrder
    {
        DALServiceOrder DALServiceOrder = DALFactory.DALServiceOrder;
        DZMembershipProvider membershipProvider = new DZMembershipProvider();
        BLLDZService bllDzService = new BLLDZService();
        public void CreateOrder(Guid membershipId,Guid serviceId)
        {
            DZMembership customer = membershipProvider.GetUserById(membershipId);
  
            DZService service = bllDzService.GetOne(serviceId);
            ServiceOrderCreator creator = new ServiceOrderCreator(customer, service);
           ServiceOrder order= creator.CreateOrder();
           DALServiceOrder.Save(order);
            
        }
        
        public int GetServiceOrderCount(Guid userId,Dianzhu.Model.Enums.enum_OrderSearchType searchType)
        {
            return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
    }
    //订单创建类.
    public class ServiceOrderCreator
    {
        private DZMembership customer;
        private DZService service;
        public ServiceOrderCreator(DZMembership customer,DZService dzService)
        {
            this.customer = customer;
            this.service = dzService;
        }
        public ServiceOrder CreateOrder()
        {
            ServiceOrder order = new ServiceOrder();
            order.Customer = this.customer;
            order.OrderCreated = DateTime.Now;
            order.OrderStatus = Model.Enums.enum_OrderStatus.Wt;//等待商户接单
            order.Service = service;

            return order;
        }
    }
    
}
