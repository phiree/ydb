using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
   public  class PushService
    {
        DAL.DALServiceOrderPushedService dalSOP;
        public PushService(DAL.DALServiceOrderPushedService dalSOP)
        {
            this.dalSOP = dalSOP;
        }
        public PushService():this(new DAL.DALServiceOrderPushedService())
        {

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
            foreach (ServiceOrderPushedService service in services)
            {
                 
                dalSOP.Save(service);
            }
            
        }
        public IList<ServiceOrderPushedService> GetPushedServicesForOrder(ServiceOrder order)
        {
            return dalSOP.FindByOrder(order);
        }
        /// <summary>
        /// 用户选择某项服务之后创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="selectedService"></param>
        public void SelectServiceAndCreate(ServiceOrder order, DZService selectedService)
        {
            IList<ServiceOrderPushedService> l = GetPushedServicesForOrder(order);
            ServiceOrderPushedService s = l.Single(x => x.OriginalService == selectedService);
            order.AddDetailFromIntelService(s.OriginalService, 1, s.TargetAddress, s.TargetTime);

            order.CreatedFromDraft();
        }
    }
}
