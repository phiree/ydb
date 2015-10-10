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
            throw new Exception("to do tomorrow");
            //ServiceOrder order =  ServiceOrder.
            //{
            //    CustomerEmail = customerEmail,
            //    CustomerPhone = customerPhone,
            //    CustomerName = customerName,

            //    ServiceBusinessName = businessName,
            //    ServiceDescription = serviceDescription,
            //    ServiceName = serviceName,
            //    ServiceURL = externalUrl,
            //    TargetAddress = targetAddress,
            //    ServiceUnitPrice = unitPrice * unitAmount

            //};
            //if (adjustPrice > 0)
            //{
            //    order.ServiceUnitPrice = adjustPrice;
            //}
            //DALServiceOrder.Save(order);
            //return order;
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
            throw new Exception("to do tomorrow");
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

            
            throw new Exception("to do tomorrow");
        }

        /// <summary>
        /// 根据用户确认的服务 创建订单.
        /// </summary>
        /// <param name="confirmedService"></param>
        /// <returns></returns>
        

        public int GetServiceOrderCount(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType)
        {
            return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
        public IList<ServiceOrder> GetServiceOrderList(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

        public virtual ServiceOrder GetOne(Guid guid)
        {
            return DALServiceOrder.GetOne(guid);
        }
        public void SaveOrUpdate(ServiceOrder order)
        {

            DALServiceOrder.SaveOrUpdate(order);
        }
    }


}
