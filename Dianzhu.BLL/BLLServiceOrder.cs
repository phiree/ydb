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


        DALServiceOrder DALServiceOrder = null;
        DZMembershipProvider membershipProvider = null;
        BLLDZService bllDzService = null;

        public BLLServiceOrder()
        {
            DALServiceOrder = DALFactory.DALServiceOrder;
           membershipProvider= new DZMembershipProvider();
           bllDzService = new BLLDZService();
        }
        public BLLServiceOrder(DALServiceOrder dal)
        {
            DALServiceOrder = dal;
        }

        
        /// <summary>
        /// 创建服务,系统外的服务,未注册用户
        /// </summary>
       
        /// <param name="serviceName"></param>
        /// <param name="totalPrice"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="businessName"></param>
        /// <param name="externalUrl"></param>
        /// <param name="targetAddress"></param>
        /// <returns></returns>
        public ServiceOrder CreateOrder(
            string serviceName, decimal unitPrice, int unitAmount, string serviceDescription, string businessName,
            string customerName, string customerPhone, string customerEmail,
            string externalUrl, string targetAddress, decimal adjustPrice)
        {

            ServiceOrder order = new ServiceOrder
            {
                CustomerEmail = customerEmail,
                CustomerPhone = customerPhone,
                CustomerName = customerName,

                ServiceBusinessName = businessName,
                ServiceDescription = serviceDescription,
                ServiceName = serviceName,
                ServiceURL = externalUrl,
                TargetAddress = targetAddress,
                ServiceUnitPrice = unitPrice * unitAmount

            };
            if (adjustPrice > 0)
            {
                order.ServiceUnitPrice = adjustPrice;
            }
            DALServiceOrder.Save(order);
            return order;
        }
        /// <summary>
        /// 未注册用户 系统外服务.
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="serviceId"></param>
        /// <param name="targetAddress"></param>
        /// <param name="unitAmount"></param>
        /// <returns></returns>
        public ServiceOrder CreateOrder(DZMembership customer, Guid serviceId, string targetAddress, int unitAmount)
        {
             DZService service = bllDzService.GetOne(serviceId);
            ServiceOrder order = new ServiceOrder
            {
                Customer = customer,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.Phone,
                CustomerName = customer.NickName,
                Service = service,
                ServiceBusinessName = service.Business.Name,
                ServiceDescription = service.Description,
                ServiceName = service.Name,
                ServiceURL = string.Empty,
                TargetAddress = targetAddress,
                ServiceUnitPrice = service.UnitPrice * unitAmount
            };
            DALServiceOrder.Save(order);
            return order;
        }
        /// <summary>
        /// 注册用户 系统外服务
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceBusinessName"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceUnitPrice"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="serviceUnitAmount"></param>
        /// <param name="targetAddress"></param>
        /// <returns></returns>
        public ServiceOrder CreateOrder(DZMembership customer, string serviceName,
            string serviceBusinessName, string serviceDescription, string serviceUnitPrice,
            string serviceUrl, string serviceUnitAmount, string targetAddress)
        {

            ServiceOrder order = new ServiceOrder
            { 
                Customer=customer,
                CustomerEmail= customer.Email,
                CustomerName=customer.UserName,
                CustomerPhone= customer.Phone,
                ServiceBusinessName = serviceBusinessName,
                ServiceDescription = serviceDescription,
                ServiceName = serviceName,
                ServiceURL = serviceUrl,
                TargetAddress = targetAddress,
                ServiceUnitPrice=Convert.ToDecimal(serviceUnitPrice),
                UnitAmount=Convert.ToInt32(serviceUnitAmount),

            };
             
             
            DALServiceOrder.Save(order);
            return order;
        }

        /// <summary>
        /// 根据用户确认的服务 创建订单.
        /// </summary>
        /// <param name="confirmedService"></param>
        /// <returns></returns>
        public ServiceOrder CreateOrder(ReceptionChatServiceConfirmed confirmedService)
        {
            DZMembership customer = confirmedService.From;
            ServiceOrder order = new ServiceOrder { 
             Customer=confirmedService.From,
               ServiceUnitPrice=confirmedService.UnitPrice,
               UnitAmount=confirmedService.UnitAmount,
                ServiceBusinessName=confirmedService.ServiceName,
                Service=confirmedService.Service
                
            };
            throw new NotImplementedException();
        }

        public int GetServiceOrderCount(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType)
        {
            return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
        public IList<ServiceOrder> GetServiceOrderList(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

        public ServiceOrder GetOne(Guid guid)
        {
            return DALServiceOrder.GetOne(guid);
        }
    }


}
