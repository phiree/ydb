using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataSHM_snapshotObj
    {

        public RespDataSHM_snapshotObj()
        {
            maxOrderDic = new List<RespDataSHM_maxOrderItem>();
            workTimeDic = new List<RespDataSHM_workTimeDicItem>();
            OrderDic = new List<RespDataSHM_orderDicItem>();
        }
        public IList<RespDataSHM_maxOrderItem> maxOrderDic { get; set; }
        public IList< RespDataSHM_workTimeDicItem> workTimeDic { get; set; }
        public IList<RespDataSHM_orderDicItem> OrderDic { get; set; }

        public RespDataSHM_snapshotObj Adap(IList<ServiceOrder> orderList)
        {
            IList<DateTime> dates = orderList.Select(x => x.OrderCreated.Date).ToList();

            foreach (DateTime date in dates)
            {
                IList<ServiceOrder> orderInDateList = orderList.Where(x => x.OrderCreated.Date == date).ToList();
                //接单量平均值
                int maxOrder = (int)orderInDateList.Average(x => x.Details[0].OpenTimeSnapShot.MaxOrder);

                RespDataSHM_maxOrderItem maxOrderItem = new RespDataSHM_maxOrderItem(date, maxOrder, orderInDateList.Count);
                maxOrderDic.Add(maxOrderItem);

                //工作时间快照

                IList<ServiceOpenTimeForDaySnapShotForOrder> snaps = orderInDateList.Select(x => x.Details[0].OpenTimeSnapShot).ToList();

                this.workTimeDic.Add(new RespDataSHM_workTimeDicItem(date,snaps).Adap());
                //订单快照
                this.OrderDic.Add(new RespDataSHM_orderDicItem(orderInDateList,date).Adap());

            }
            return this;
        }
    }

    public class RespDataSHM_maxOrderItem
    {
        public RespDataSHM_maxOrderItem(DateTime date,int maxOrder,int reOrder)
        {
            this.date = date.ToString("yyyyMMdd");
            this.maxOrder = maxOrder.ToString();
            this.reOrder = reOrder.ToString();
        }

        public string date { get; private set; }
        public string maxOrder { get; private set; }
        /// <summary>
        /// 当天的订单数量
        /// </summary>
        public string reOrder { get; private set; }
    }
 
    public class RespDataSHM_workTimeDicItem
    {
        IList<ServiceOpenTimeForDaySnapShotForOrder> snapList;
        public RespDataSHM_workTimeDicItem(DateTime date, IList<ServiceOpenTimeForDaySnapShotForOrder> snapList)
        {
            this.date = date.ToString("yyyyMMdd");
            this.snapList = snapList;
            workTimeObj = new List<RespDataSHM_workTimeObjItem>();
        }
        public string date { get; set; }
        public IList<RespDataSHM_workTimeObjItem> workTimeObj { get; set; }
        public RespDataSHM_workTimeDicItem Adap()
        {
            foreach (ServiceOpenTimeForDaySnapShotForOrder snap in snapList)
            {
                RespDataSHM_workTimeObjItem item = new RespDataSHM_workTimeObjItem().Adap(snap);
                workTimeObj.Add(item);
            }
            return this;
        }

    }
 public class RespDataSHM_workTimeObjItem
{
    public string workTimeID { get; set; }// "6F9619FF-8B86-D011-B42D-00C04FC964FF",
    public string tag { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string week { get; set; }
    public string open { get; set; }
        public RespDataSHM_workTimeObjItem Adap(ServiceOpenTimeForDaySnapShotForOrder opentimeforday)
        {
            this.workTimeID = opentimeforday.Id.ToString();
            this.startTime = opentimeforday.PeriodBegin.ToString();
            this.endTime = opentimeforday.PeriodEnd.ToString();
            this.week = opentimeforday.Date.DayOfWeek.ToString();

            return this;
        }
    }

 
    public class RespDataSHM_orderDicItem
    {
        IList<ServiceOrder> orderList;
        public RespDataSHM_orderDicItem(IList<ServiceOrder> orderList,DateTime date)
        {
            OrderObj = new List<RespDataSHM_orderObjItem>();
            this.orderList = orderList;
            this.date = date.ToString("yyyyMMdd");
        }
        public string date { get; set; }
        public IList<RespDataSHM_orderObjItem> OrderObj { get; set; }
        public RespDataSHM_orderDicItem Adap()
        {
            foreach (ServiceOrder order in orderList)
            {
                this.OrderObj.Add(new RespDataSHM_orderObjItem().Adap(order));
            }
            return this;
        }
    }
    public class RespDataSHM_orderObjItem
    {
        public string orderID { get; set; }
                           public string title { get; set; }
        public string status { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string exDoc { get; set; }
        public string money { get; set; }
        public string address { get; set; }
        public string km { get; set; }//todo:?
        public RespDataSHM_orderObjItem_svcObj svcObj { get; set; }
        public RespDataSHM_orderObjItem_userObj userObj { get; set; }
        public RespDataSHM_orderObjItem_storeObj storeObj { get; set; }

        public RespDataSHM_orderObjItem Adap(ServiceOrder order)
        {
            this.orderID = order.Id.ToString();
            this.status = order.GetStatusTitleFriendly(order.OrderStatus);
            this.startTime = order.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.endTime=order.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.exDoc = order.Description;
            this.money = order.NegotiateAmount.ToString();
            this.address = order.TargetAddress;
            this.svcObj = new RespDataSHM_orderObjItem_svcObj().Adap(order.Details[0]);
            this.userObj = new RespDataSHM_orderObjItem_userObj().Adap(order.Customer);
            this.storeObj = new RespDataSHM_orderObjItem_storeObj().Adap(order.Business);

            return this;
        }
    }
    public class RespDataSHM_orderObjItem_svcObj
    {
        public string svcID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string startTime { get; set; }//todo: ?
        public string endTime { get; set; }//todo: ?
        public RespDataSHM_orderObjItem_svcObj Adap(ServiceOrderDetail orderDetail)
        {

            this.name = orderDetail.ServieSnapShot.ServiceName;
            this.type = orderDetail.OriginalService.ServiceType.ToString();
            this.svcID = orderDetail.OriginalService.Id.ToString();
            return this;
        }
    }
    public class RespDataSHM_orderObjItem_userObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string imgUrl { get; set; }
        public RespDataSHM_orderObjItem_userObj Adap(DZMembership customer)
        {

            this.userID = customer.Id.ToString();
            this.alias = customer.DisplayName;
            this.imgUrl = customer.AvatarUrl;
            return this;
        }
    }
    public class RespDataSHM_orderObjItem_storeObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string imgUrl { get; set; }
        public RespDataSHM_orderObjItem_storeObj Adap(Business business)
        {

            this.userID = business.Owner.Id.ToString();
            this.alias = business.Name;
            this.imgUrl = business.BusinessAvatar.ImageName ;
            return this;
        }
    }
    public class ReqDataSHM001007
{
    public string stratTime { get; set; }
    public string endTime { get; set; }
    public string serviceId { get; set; }
}

public class RespDataSHM001007
{
    public RespDataSHM_snapshotObj snapshotObj { get; set; }
}
}
