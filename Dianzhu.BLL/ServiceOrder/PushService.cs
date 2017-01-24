using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Ydb.Common;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
namespace Dianzhu.BLL
{
   public  class PushService
    {
        IDAL.IDALServiceOrderPushedService dalSOP;
       
        IServiceOrderService bllServiceOrder;
        IDZServiceService dzServiceService;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis;
        public PushService(IDAL.IDALServiceOrderPushedService dalSOP, IServiceOrderService bllServiceOrder, IDZServiceService dzServiceService,BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis)
        {
            this.dalSOP = dalSOP;
            this.bllServiceOrder = bllServiceOrder;
            this.dzServiceService = dzServiceService;
       


            this.bllServiceOrderStateChangeHis = bllServiceOrderStateChangeHis;
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
        public void SelectServiceAndCreate(ServiceOrder order, string selectedServiceId)
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
             DZService serviceDto=  dzServiceService.GetOne2(new Guid(selectedServiceId));
                ServiceSnapShot serviceSnapShot = AutoMapper.Mapper.Map<ServiceSnapShot>(serviceDto);
                
                ServiceOpenTimeForDay workTime = dzServiceService.GetWorkTime(new Guid(selectedServiceId), s.TargetTime);
                WorkTimeSnapshot serviceTimeSnapShot = new WorkTimeSnapshot {

                    MaxOrderForWorkDay = workTime.ServiceOpenTime.MaxOrderForDay,
                    MaxOrderForWorkTime = workTime.MaxOrderForOpenTime,
                    TimePeriod = workTime.TimePeriod
                };
                order.AddDetailFromIntelService(s.OriginalServiceId, serviceSnapShot,
                    serviceTimeSnapShot,
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
