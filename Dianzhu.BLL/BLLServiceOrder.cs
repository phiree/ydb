using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    
    /// <summary>
    /// 订单业务逻辑
    /// </summary>
    public class BLLServiceOrder
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");
        
        DALServiceOrder DALServiceOrder = null;
        DZMembershipProvider membershipProvider = null;
        BLLDZService bllDzService = null;

       
        public BLLServiceOrder()
        {
            DALServiceOrder = DALFactory.DALServiceOrder;
            membershipProvider = new DZMembershipProvider();
            bllDzService = new BLLDZService();

        }



        public BLLServiceOrder(DALServiceOrder dal)
        {
            DALServiceOrder = dal;
        }


        #region 基本操作

        public IList<ServiceOrder> GetListForBusiness(object b)
        {
            throw new NotImplementedException();
        }

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
        public IList<ServiceOrder> GetAll() //获取全部订单
        {
            return DALServiceOrder.GetAll<ServiceOrder>();
        }

        public IList<ServiceOrder> GetAllByOrderStatus(Dianzhu.Model.Enums.enum_OrderStatus status)
        {
            return DALServiceOrder
               .GetAll<ServiceOrder>()
               .Where(x => x.OrderStatus == status)
               .ToList();
        }

        public IList<ServiceOrder> GetAllByTradeNo(string tradeno)  //根据交易号，查询订单对象
        {
            return DALServiceOrder
               .GetAll<ServiceOrder>()
               .Where(x => x.TradeNo == tradeno)
               .ToList();
        }



        public IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        {
            return DALServiceOrder.GetListForBusiness(business, pageNum, pageSize, out totalAmount);
        }

        public IList<ServiceOrder> GetListForCustomer(DZMembership customer, int pageNum, int pageSize, out int totalAmount)
        {
            return DALServiceOrder.GetListForCustomer(customer, pageNum, pageSize, out totalAmount);
        }

        public void Delete(ServiceOrder order)
        {
            DALServiceOrder.Delete(order);
        }

        public virtual ServiceOrder GetDraftOrder(DZMembership c, DZMembership cs)
        {
            return DALServiceOrder.GetDraftOrder(c, cs);
        }
        public IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime date)
        {
            return DALServiceOrder.GetOrderListByDate(service, date);
        }
        #endregion

        #region 订单流程变化

        //用户定金支付完成
        public void OrderFlow_PayDeposit(ServiceOrder order)
        {
             ChangeStatus(order, enum_OrderStatus.Payed);

            //订单支付成功
            log.Debug("调用IMServer,发送订单状态变更通知");
            System.Net.WebClient wc = new System.Net.WebClient();
            string notifyServer = Dianzhu.Config.Config.GetAppSetting("NotifyServer");
            Uri uri = new Uri(notifyServer + "IMServerAPI.ashx?type=ordernotice&orderId=" + order.Id);
            System.IO.Stream returnData = wc.OpenRead(uri);
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();
            log.Debug("发送结果:" + result);
        }
        /// <summary>
        /// 商家确认订单,准备执行.
        /// </summary>
        public void OrderFlow_BusinessConfirm(ServiceOrder order)
        {
            order.LatestOrderUpdated = DateTime.Now;
           
            ChangeStatus(order, enum_OrderStatus.Negotiate);
        }
        /// <summary>
        /// 商家输入协议
        /// </summary>
        /// <param name="order"></param>
        /// <param name="negotiateAmount"></param>
        public void OrderFlow_BusinessNegotiate(ServiceOrder order, decimal negotiateAmount)
        {
            order.NegotiateAmount = negotiateAmount;
            if (negotiateAmount < order.DepositAmount)
            {
                log.Warn("协商价格小于订金");
            }
            order.LatestOrderUpdated = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Assigned);

        }
        /// <summary>
        /// 用户确认协商价格,并确定开始服务
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerConfirmNegotiate(ServiceOrder order)
        {
            order.OrderServerStartTime = DateTime.Now;
            order.LatestOrderUpdated = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Begin);
        }
        /// <summary>
        /// 商家确定服务完成
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            order.LatestOrderUpdated = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.IsEnd);
        }
        /// <summary>
        /// 用户确认服务完成。
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            order.LatestOrderUpdated = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Ended);
        }
        /// <summary>
        /// 用户支付尾款
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayFinalPayment(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            order.LatestOrderUpdated = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Finished);
        }

        //订单状态改变通用方法
        private void ChangeStatus(ServiceOrder order, Model.Enums.enum_OrderStatus targetStatus)
        {
            OrderServiceFlow flow = new OrderServiceFlow(order, targetStatus);
            flow.ChangeStatus();

            ServiceOrderStateChangeHis orderHist = new ServiceOrderStateChangeHis
            {
                NewAmount = order.OrderAmount,
                Order = order,
                Remark = string.Empty,
                Status = targetStatus,
            };

            SaveOrUpdate(order);
        }
        #endregion

        #region 分配工作人员
        public void AssignStaff(ServiceOrder order, Staff staff)
        {

        }
        public void DeassignStaff(ServiceOrder order, Staff staff)
        {

        }
        #endregion

    }
    /// <summary>
    /// 订单状态变更控制.
    /// </summary>
    public class OrderServiceFlow
    {
        ServiceOrder order;
        Model.Enums.enum_OrderStatus targetStatus;

        public OrderServiceFlow(ServiceOrder order, Model.Enums.enum_OrderStatus targetStatus)
        {
            this.order = order;
            this.targetStatus = targetStatus;
        }
        public void ChangeStatus()
        {

            bool validated = dictAvailabelStatus[targetStatus].Contains(order.OrderStatus);
            if (validated)
            {
                order.OrderStatus = targetStatus;
            }
           


        }
        /// <summary>
        /// 确保目标状态是可以执行的.
        /// </summary>

        //状态对应表. key:状态, value:该状态可以从哪些状态转变而来.
        static Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> dictAvailabelStatus = new Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> {
            { enum_OrderStatus.Payed,new List<enum_OrderStatus>() {enum_OrderStatus.Created }},
             { enum_OrderStatus.Negotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Payed }},
              { enum_OrderStatus.Assigned,new List<enum_OrderStatus>() {enum_OrderStatus.Negotiate }},
              { enum_OrderStatus.Begin,new List<enum_OrderStatus>() {enum_OrderStatus.Assigned }},
               { enum_OrderStatus.IsEnd,new List<enum_OrderStatus>() {enum_OrderStatus.Begin }},
                { enum_OrderStatus.Ended,new List<enum_OrderStatus>() {enum_OrderStatus.IsEnd , enum_OrderStatus.Begin}},
                { enum_OrderStatus.Finished,new List<enum_OrderStatus>() {enum_OrderStatus.Ended }},

        };
    }


}
