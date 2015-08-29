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
                TotalPrice = unitPrice * unitAmount

            };
            if (adjustPrice > 0)
            {
                order.TotalPrice = adjustPrice;
            }
            DALServiceOrder.Save(order);
            return order;
        }
        /// <summary>
        /// 系统内服务,注册用户
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="serviceId"></param>
        /// <param name="targetAddress"></param>
        /// <param name="unitAmount"></param>
        /// <returns></returns>
        public ServiceOrder CreateOrder(Guid membershipId, Guid serviceId, string targetAddress, int unitAmount)
        {
            DZMembership customer = membershipProvider.GetUserById(membershipId);
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
                TotalPrice = service.UnitPrice * unitAmount
            };
            DALServiceOrder.Save(order);
            return order;
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
