using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;

namespace Dianzhu.BLL
{
   public  class PushService
    {
        IDAL.IDALServiceOrderPushedService dalSOP;
        BLLPayment bllPayment;
        IBLLServiceOrder bllServiceOrder;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis;
        public PushService(IDAL.IDALServiceOrderPushedService dalSOP,IBLLServiceOrder bllServiceOrder, BLLPayment bllPayment,BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis)
        {
            this.dalSOP = dalSOP;
            this.bllServiceOrder = bllServiceOrder;
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
        public void SelectServiceAndCreate(ServiceOrder order, DZService selectedService)
        {
            IList<ServiceOrderPushedService> l = GetPushedServicesForOrder(order);
            if (l.Count > 1)
            {
                throw new Exception("包含多个推送服务");
            }
            else if (l.Count == 1)
            {
                ServiceOrderPushedService s = l.Single(x => x.OriginalService.Id == selectedService.Id);
                if (s == null)
                {
                    throw new Exception("该服务不是该订单的推送服务！");
                }
                order.AddDetailFromIntelService(s.OriginalService, s.UnitAmount,s.TargetCustomerName,s.TargetCustomerPhone, s.TargetAddress, s.TargetTime);

                order.CreatedFromDraft();
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                //保存订单历史记录
                bllServiceOrderStateChangeHis.Save(order, enum_OrderStatus.DraftPushed);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                Payment payment = bllPayment.ApplyPay(order, enum_PayTarget.Deposit);

                if (order.DepositAmount > 0)
                {
                    PHSuit.HttpHelper.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=ordernotice&orderId=" + order.Id.ToString(), "get", null);
                }
                else
                {
                    bllServiceOrder.OrderFlow_ConfirmDeposit(order);

                    payment.Status = enum_PaymentStatus.Trade_Success;
                    bllPayment.Update(payment);
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                }
            }
        }
    }
}
