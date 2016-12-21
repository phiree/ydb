using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.Order.DomainModel.Repository;
namespace Ydb.Order.Application
{
    /// <summary>
    /// 推送给用户的服务.
    /// </summary>
   public  class PushService
    {
        IRepositoryServiceOrderPushedService repoPushedService;
       
        public PushService( )
        {
             
        }
        public void Push(ServiceOrder order, ServiceOrderPushedService service, string targetAddress, DateTime targetTime)
        {
            IList<ServiceOrderPushedService> serviceOrderPushedServices = new List<ServiceOrderPushedService>();
            serviceOrderPushedServices.Add(service);

            Push(order, serviceOrderPushedServices, targetAddress, targetTime);
        }
        /// <summary>
        /// 为某个订单推送服务.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="services"></param>
        /// <param name="targetAddress"></param>
        /// <param name="targetTime"></param>
        public void Push(ServiceOrder order, IList<ServiceOrderPushedService> services, string targetAddress, DateTime targetTime)
        {
            //order.OrderStatus = enum_OrderStatus.DraftPushed;
           

            foreach (ServiceOrderPushedService service in services)
            {
                repoPushedService.Add(service);
            }
        }
        public IList<ServiceOrderPushedService> GetPushedServicesForOrder(ServiceOrder order)
        {
            return repoPushedService.FindByOrder(order);
        }
        /// <summary>
        /// 用户选择某项服务之后创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="selectedService"></param>
        public void SelectServiceAndCreate(ServiceOrder order,ServiceSnapShot serviceSnapshot,WorkTimeSnapshot worktimeSnapshot,  string selectedServiceId)
        {


            IList<ServiceOrderPushedService> l = GetPushedServicesForOrder(order);
            if (l.Count > 1)
            {
                throw new Exception("包含多个推送服务");
            }
            else if (l.Count == 1)
            {
                ServiceOrderPushedService s = l.Single(x => x.OriginalServiceId == selectedServiceId);
                if (s == null)
                {
                    throw new Exception("该服务不是该订单的推送服务！");
                }

                //todo:  需要用Automapper
           
                order.AddDetailFromIntelService(s.OriginalServiceId, serviceSnapshot,
                    worktimeSnapshot,
                    s.UnitAmount,s.TargetCustomerName,s.TargetCustomerPhone, s.TargetAddress, s.TargetTime,s.Memo);

                order.CreatedFromDraft();
                bllServiceOrder.OrderFlow_ConfirmOrder(order);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                ////保存订单历史记录
                //bllServiceOrderStateChangeHis.Save(order, enum_OrderStatus.DraftPushed);
                //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            }
        }
    }
}
