using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Ydb.Common;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
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
            //order.OrderStatus = enum_OrderStatus.DraftPushed;
           

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
                ServiceSnapShot serviceSnapShot = new ServiceSnapShot {
                    CancelCompensation = serviceDto.CancelCompensation,
                    ChargeUnitType =   serviceDto.ChargeUnitType,
                    DepositAmount = serviceDto.DepositAmount,
                    Description = serviceDto.Description,
                     IsCompensationAdvance= serviceDto.IsCompensationAdvance,
                      MinPrice=serviceDto.MinPrice,
                       OverTimeForCancel=serviceDto.OverTimeForCancel,
                        ServiceBusinessId=serviceDto.ServiceBusinessId,
                        ServiceBusinessName=serviceDto.ServiceBusinessName,
                        ServiceBusinessPhone=serviceDto.ServiceBusinessPhone,
                        ServiceModeType =   serviceDto.ServiceModeType,
                         ServiceName=serviceDto.ServiceName,
                          ServiceTypeName= serviceDto.ServiceTypeName,
                    ServiceTypeId = serviceDto.ServiceTypeId,
                    UnitPrice =serviceDto.UnitPrice,
                           ServiceBusinessOwnerId=serviceDto.ServiceBusinessOwnerId
                     
                };
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
