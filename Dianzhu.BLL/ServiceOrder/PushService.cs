using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Ydb.BusinessResource.Application;
 
namespace Dianzhu.BLL
{
   public  class PushService
    {
        IDAL.IDALServiceOrderPushedService dalSOP;
        BLLPayment bllPayment;
        IBLLServiceOrder bllServiceOrder;
        IDZServiceService dzServiceService;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis;
        public PushService(IDAL.IDALServiceOrderPushedService dalSOP,IBLLServiceOrder bllServiceOrder, IDZServiceService dzServiceService, BLLPayment bllPayment,BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis)
        {
            this.dalSOP = dalSOP;
            this.bllServiceOrder = bllServiceOrder;
            this.dzServiceService = dzServiceService;
            this.bllPayment = bllPayment;

            this.bllServiceOrderStateChangeHis = bllServiceOrderStateChangeHis;
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
            //order.OrderStatus = Model.Enums.enum_OrderStatus.DraftPushed;
           

            foreach (ServiceOrderPushedService service in services)
            {
                dalSOP.Add(service);
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
             ServiceDto serviceDto=  dzServiceService.GetOne(new Guid(selectedServiceId));
                ServiceSnapShotForOrder serviceSnapShot = new ServiceSnapShotForOrder {
                    CancelCompensation = serviceDto.CancelCompensation,
                    ChargeUnit = (enum_ChargeUnit)Enum.Parse(typeof(enum_ChargeUnit), serviceDto.ChargeUnitType),
                    DepositAmount = serviceDto.DepositAmount,
                    Description = serviceDto.Description,
                     IsCompensationAdvance= serviceDto.IsCompensationAdvance,
                      MinPrice=serviceDto.MinPrice,
                       OverTimeForCancel=serviceDto.OverTimeForCancel,
                        ServiceBusinessId=serviceDto.ServiceBusinessId,
                        ServiceBusinessName=serviceDto.ServiceBusinessName,
                        ServiceBusinessPhone=serviceDto.ServiceBusinessPhone,
                        ServiceMode = (enum_ServiceMode)Enum.Parse(typeof(enum_ServiceMode), serviceDto.ServiceModeType),
                         ServiceName=serviceDto.ServiceName,
                          ServiceTypeName= serviceDto.ServiceTypeName,
                           UnitPrice=serviceDto.UnitPrice,
                           ServiceBusinessOwnerId=serviceDto.ServiceBusinessOwnerId
                     
                };
                ServiceOpenTimeDto openTimeDto = dzServiceService.GetTimeDto(new Guid(selectedServiceId), s.TargetTime);
                ServiceOpenTimeSnapshot serviceTimeSnapShot = new ServiceOpenTimeSnapshot {
                     Date=openTimeDto.Date,
                      MaxOrderForDay=openTimeDto.MaxOrderForDay,
                    MaxOrderForPeriod = openTimeDto.MaxOrderForPeriod,
                     PeriodBegin=openTimeDto.PeriodBegin,
                      PeriodEnd=openTimeDto.PeriodEnd
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
