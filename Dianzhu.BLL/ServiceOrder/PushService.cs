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
        DAL.DALServiceOrderPushedService dalSOP;
        BLLServiceOrder bllServiceOrder;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis;
        public PushService(DAL.DALServiceOrderPushedService dalSOP)
        {
            this.dalSOP = dalSOP;
            this.bllServiceOrder = new BLLServiceOrder();
            this.bllServiceOrderStateChangeHis = new BLLServiceOrderStateChangeHis();
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
            order.OrderStatus = Model.Enums.enum_OrderStatus.DraftPushed;
            bllServiceOrder.SaveOrUpdate(order);

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
            if (l.Count > 0)
            {
                ServiceOrderPushedService s = l.Single(x => x.OriginalService == selectedService);
                order.AddDetailFromIntelService(s.OriginalService, 1, s.TargetAddress, s.TargetTime);

                order.CreatedFromDraft();

                PHSuit.HttpHelper.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=ordernotice&orderId=" + order.Id.ToString(), "get", null);

                if (order.DepositAmount>0)
                {
                    BLLPayment bllPayment = new BLLPayment();
                    Payment payment = bllPayment.ApplyPay(order, enum_PayTarget.Deposit);
                }
                else
                {
                    bllServiceOrder.OrderFlow_PayDeposit(order);
                }
            }            
        }
    }
}
