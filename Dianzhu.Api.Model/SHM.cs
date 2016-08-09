using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataSHM_snapshots
    {

        public RespDataSHM_snapshots()
        {
            snapshots = new List<RespDataSHM_snapshots_item>();

        }
        public IList<RespDataSHM_snapshots_item> snapshots;

        public RespDataSHM_snapshots Adap(IList<ServiceOrder> orderList)
        {
            IList<DateTime> dates = orderList.Select(x => x.OrderCreated.Date).Distinct().ToList();

            foreach (DateTime date in dates)
            {
                IList<ServiceOrder> orderInDateList = orderList.Where(x => x.OrderCreated.Date == date).ToList();
                //接单量平均值
             
                snapshots.Add(
                    new RespDataSHM_snapshots_item().Adap(date, orderInDateList)
                    );
            }
            return this;
        }



    }
    public class RespDataSHM_snapshots_item
    {
        public RespDataSHM_snapshots_item()
        {
            orderSnapshots = new List<RespDataSHM_snapshots_item_orderSnapshots_item>();
        }
        public string date { get; set; }
        public IList<RespDataSHM_snapshots_item_orderSnapshots_item> orderSnapshots;
        public RespDataSHM_snapshots_item Adap(DateTime date , IList<ServiceOrder> orderList)
        {
            this.date = date.ToString("yyyyMMddHHmmss");
            foreach (ServiceOrder order in orderList)
            {
                orderSnapshots.Add(new RespDataSHM_snapshots_item_orderSnapshots_item().Adap(order));
            }
            return this;
        }
    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item
    {
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj orderObj;
        public RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj worktimeObj;
        public RespDataSHM_snapshots_item_orderSnapshots_item Adap(ServiceOrder order)
        {
            this.orderObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj().Adap(order);
            this.worktimeObj = new RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj()
                            .Adap(order.Service.GetOpenTimeSnapShot(order.Details[0].TargetTime));
            return this;
        }

    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item_orderObj
    {
        public string orderID{ get; set; }
        public string title{ get; set; }
        public string status{ get; set; }
        /// <summary>
        /// 订单的下单时间
        /// </summary>
        public string startTime{ get; set; }
        /// <summary>
        /// 没有对应的数据, 返回空
        /// </summary>
        public string endTime{ get; set; }
        public string exDoc{ get; set; }
        public string money{ get; set; }
        public string address{ get; set; }
        public string km{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_svcObj svcObj{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj userObj{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj storeObj{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj Adap(ServiceOrder order)
        {
            this.orderID = order.Id.ToString();
            this.title = order.Title;
            this.status = order.GetStatusTitleFriendly(order.OrderStatus);
            this.startTime = order.OrderConfirmTime.ToString("yyyyMMddHHmmss");
            this.endTime = string.Empty;
            this.exDoc = order.Description;
            this.money = order.NegotiateAmount.ToString();
            this.address = order.TargetAddress;
            this.km = string.Empty;
            this.svcObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_svcObj().Adap(order.Details[0]);
            this.userObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj().Adap(order.Customer);
            this.storeObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj().Adap(order.Business);
            return this;
        }

    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_svcObj
    {
        public string svcID{ get; set; }
        public string name{ get; set; }
        public string type{ get; set; }
        /// <summary>
        /// 服务的预约时间
        /// </summary>
        public string startTime{ get; set; }
        /// <summary>
        /// 没有对应的数据,现返回空值.
        /// </summary>
        public string endTime{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_svcObj Adap(ServiceOrderDetail orderDetail)
        {
           
            this.svcID = orderDetail.OriginalService.Id.ToString();
            this.name = orderDetail.ServieSnapShot.ServiceName;
            this.type = orderDetail.OriginalService.ServiceType.ToString();
            
            this.startTime = orderDetail.TargetTime.ToString("yyyyMMddHHmmss");
            this.endTime = string.Empty;
            return this;
        }
    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj
    {
        public string userID{ get; set; }
        public string alias{ get; set; }
        public string imgUrl{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj Adap(DZMembership customer)
        {
            this.userID = customer.Id.ToString();
            this.alias = customer.NickName;
            this.imgUrl = customer.AvatarUrl;
            return this;
        }
    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj
    {
        public string userID{ get; set; }
        public string alias{ get; set; }
        public string imgUrl{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj Adap(Business b)
        {
            this.userID = b.Owner.Id.ToString();
            this.alias = b.Owner.NickName;
            this.imgUrl = b.Owner.AvatarUrl;
            return this;
        }

    }
    public class RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj
    {
        
        public string tag { get; set; }
        public string startTime{ get; set; }
        public string endTime{ get; set; }
        public string week{ get; set; }
        public string open{ get; set; }
        public string maxOrder{ get; set; }
        public RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj Adap(ServiceOpenTimeForDaySnapShotForOrder snap)
        {
            this.startTime = PHSuit.StringHelper.ConvertPeriodToTimeString(snap.PeriodBegin);//.ToString();
            this.endTime = PHSuit.StringHelper.ConvertPeriodToTimeString(snap.PeriodEnd);//.ToString();
            this.week = ((int) snap.Date.DayOfWeek).ToString();
            this.open = "Y";
            this.maxOrder = snap.MaxOrder.ToString();
            return this;
        }
            }
    /*
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


public class RespDataSHM001007
{
    public RespDataSHM_snapshotObj snapshotObj { get; set; }
}*/

    public class ReqDataSHM001007
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string svcID { get; set; }
    }
}
