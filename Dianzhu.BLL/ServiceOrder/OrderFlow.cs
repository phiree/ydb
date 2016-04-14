using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 订单状态变更控制.
    /// </summary>
    public class OrderServiceFlow
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");

        /// <summary>
        /// 确保目标状态是可以执行的.
        /// </summary>

        public void ChangeStatus(ServiceOrder order, Model.Enums.enum_OrderStatus targetStatus)
        {

            bool validated = dictAvailabelStatus[targetStatus].Contains(order.OrderStatus);
            if (validated)
            {
                order.OrderStatus = targetStatus;
            }
            else
            {
                string errMsg = string.Format("不合法的状态变更{0}->{1}", order.OrderStatus, targetStatus);
                log.Error(errMsg);
                throw new Exception(errMsg);
            }

        }

        //状态对应表. key:状态, value:该状态可以从哪些状态转变而来.
        static Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> dictAvailabelStatus =
            new Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> {
                //正常支付流程订单状态变更
                { enum_OrderStatus.Created,new List<enum_OrderStatus>() {enum_OrderStatus.DraftPushed }},
                { enum_OrderStatus.CheckPayWithDeposit,new List<enum_OrderStatus>() {enum_OrderStatus.Created}},
                { enum_OrderStatus.Payed,new List<enum_OrderStatus>() {enum_OrderStatus.DraftPushed ,
                                                                        enum_OrderStatus.CheckPayWithDeposit}},
                { enum_OrderStatus.Negotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Payed,
                                                                            enum_OrderStatus.IsEnd }},
                { enum_OrderStatus.isNegotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Negotiate }},
                { enum_OrderStatus.Assigned,new List<enum_OrderStatus>() {enum_OrderStatus.isNegotiate }},
                { enum_OrderStatus.Begin,new List<enum_OrderStatus>() {enum_OrderStatus.Assigned }},
                { enum_OrderStatus.IsEnd,new List<enum_OrderStatus>() {enum_OrderStatus.Begin }},
                { enum_OrderStatus.Ended,new List<enum_OrderStatus>() {enum_OrderStatus.IsEnd ,
                                                                        enum_OrderStatus.Begin}},
                { enum_OrderStatus.CheckPayWithNegotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Ended }},
                { enum_OrderStatus.Finished,new List<enum_OrderStatus>() {enum_OrderStatus.CheckPayWithNegotiate }},
                { enum_OrderStatus.Appraised,new List<enum_OrderStatus>() {enum_OrderStatus.Finished }},
                { enum_OrderStatus.EndWarranty,new List<enum_OrderStatus>() {enum_OrderStatus.Appraised }},

                //订单取消状态可从哪些状态变更而来
                { enum_OrderStatus.Canceled,new List<enum_OrderStatus>() {enum_OrderStatus.Created,
                                                                            enum_OrderStatus.Payed,
                                                                             enum_OrderStatus.Negotiate,
                                                                              enum_OrderStatus.isNegotiate,
                                                                               enum_OrderStatus.Assigned}},
                //取消流程订单状态变更
               { enum_OrderStatus.WaitingDepositWithCanceled,new List<enum_OrderStatus>() {enum_OrderStatus.Canceled}},
               { enum_OrderStatus.EndCancel,new List<enum_OrderStatus>() { enum_OrderStatus.WaitingDepositWithCanceled,
                                                                            enum_OrderStatus.Canceled}},

               //订单理赔状态可从哪些状态变更而来
               { enum_OrderStatus.Refund,new List<enum_OrderStatus>() {enum_OrderStatus.Begin,
                                                                        enum_OrderStatus.IsEnd,
                                                                         enum_OrderStatus.Ended,
                                                                           enum_OrderStatus.Finished,
                                                                            enum_OrderStatus.Appraised}},

               //理赔流程订单状态变更
               { enum_OrderStatus.WaitingRefund,new List<enum_OrderStatus>() {enum_OrderStatus.Refund }},
               { enum_OrderStatus.isRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.RejectRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.AskPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.WaitingPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.AskPayWithRefund }},
               { enum_OrderStatus.CheckPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingPayWithRefund }},
               { enum_OrderStatus.EndRefund,new List<enum_OrderStatus>() {enum_OrderStatus.isRefund,
                                                                            enum_OrderStatus.CheckPayWithRefund}},
               //订单一点办介入状态从哪个状态变更而来
               { enum_OrderStatus.InsertIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.RejectRefund,
                                                                                    enum_OrderStatus.AskPayWithRefund}},

               //介入流程订单状态变更
               { enum_OrderStatus.HandleWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.InsertIntervention }},
               { enum_OrderStatus.NeedRefundWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.HandleWithIntervention }},
               { enum_OrderStatus.NeedPayWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.HandleWithIntervention }},
               { enum_OrderStatus.CheckPayWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.NeedPayWithIntervention }},
               { enum_OrderStatus.EndIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.NeedRefundWithIntervention,
                                                                                    enum_OrderStatus.CheckPayWithIntervention}},
               //投诉流程订单状态变更
               { enum_OrderStatus.WaitingComplaints,new List<enum_OrderStatus>() {enum_OrderStatus.Complaints }},
               { enum_OrderStatus.EndComplaints,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingComplaints }},

               //强制终止状态的变更
               { enum_OrderStatus.ForceStop,new List<enum_OrderStatus>() {enum_OrderStatus.Ended,
                                                                          enum_OrderStatus.WaitingPayWithRefund,
                                                                           enum_OrderStatus.NeedPayWithIntervention}},
        };
    }

    /// <summary>
    /// 订单状态历史记录
    /// </summary>
    public class BLLServiceOrderStateChangeHis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis = null;
        public BLLServiceOrderStateChangeHis()
        {
            dalServiceOrderStateChangeHis = DALFactory.DALServiceOrderStateChangeHis;
        }

        public void SaveOrUpdate(ServiceOrder oldOrder, enum_OrderStatus newStatus)
        {
            int num = 1;
            ServiceOrderStateChangeHis oldOrderHis = GetMaxNumberOrderHis(oldOrder);
            if (oldOrderHis != null)
            {
                num = oldOrderHis.Number + 1;
            }
            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis(oldOrder, newStatus, num);
            dalServiceOrderStateChangeHis.SaveOrUpdate(orderHis);
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetMaxNumberOrderHis(order);
        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetOrderHisList(order);
        }
        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            return dalServiceOrderStateChangeHis.GetChangeTime(order, status);
        }
    }

    public class BLLServiceOrderAppraise
    {
        public DALServiceOrderAppraise dalServiceOrderAppraise = DALFactory.DALServiceOrderAppraise;

        public void Save(ServiceOrderAppraise appraise)
        {
            dalServiceOrderAppraise.Save(appraise);
        }
    }

    public class BLLServiceOrderRemind
    {
        public DALServiceOrderRemind dalServiceOrderRemind = DALFactory.DALServiceOrderRemind;

        public void SaveOrUpdate(ServiceOrderRemind Remind)
        {
            dalServiceOrderRemind.SaveOrUpdate(Remind);
        }

        public ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
        {
            return dalServiceOrderRemind.GetOneByIdAndUserId(Id, UserId);
        }

        public int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            return dalServiceOrderRemind.GetSumByUserIdAndDatetime(userId, startTime, endTime);
        }

        public IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            IList<ServiceOrderRemind> remindList = null;

            if (startTime < endTime)
            {
                remindList = dalServiceOrderRemind.GetListByUserIdAndDatetime(userId, startTime, endTime);
            }

            return remindList;
        }
    }
}
