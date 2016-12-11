using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Application;

namespace Dianzhu.Api.Model
{
    public class RespDataSHM_snapshots
    {

        IDZMembershipService memberService;
        IDZServiceService dzService;
        public RespDataSHM_snapshots(IDZMembershipService memberService, IDZServiceService dzService)
        {
            snapshots = new List<RespDataSHM_snapshots_item>();
            this.memberService = memberService;
            this.dzService = dzService;

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
                    new RespDataSHM_snapshots_item().Adap(date, orderInDateList, memberService, dzService)
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
        public RespDataSHM_snapshots_item Adap(DateTime date , IList<ServiceOrder> orderList, IDZMembershipService memberService,IDZServiceService dzService)
        {
            this.date = date.ToString("yyyyMMddHHmmss");
            foreach (ServiceOrder order in orderList)
            {
                orderSnapshots.Add(new RespDataSHM_snapshots_item_orderSnapshots_item().Adap(order,memberService,dzService));
            }
            return this;
        }
    }
    
    public class RespDataSHM_snapshots_item_orderSnapshots_item
    {
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj orderObj;
        public RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj worktimeObj;
        public RespDataSHM_snapshots_item_orderSnapshots_item Adap(ServiceOrder order, IDZMembershipService memberService,IDZServiceService dzServiceService)
        {
            this.orderObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj().Adap(order, memberService);
            var service = dzServiceService.GetOne2(new Guid(order.ServiceId));
           
            this.worktimeObj = new RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj()
                            .Adap(order,order.Details[0].ServiceOpentimeSnapshot);
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
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj Adap(ServiceOrder order,IDZMembershipService memberService)
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
            MemberDto member = memberService.GetUserById(order.CustomerId);
            this.svcObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_svcObj().Adap(order.Details[0]);
            this.userObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj().Adap(member);
            this.storeObj = new RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj().Adap(order,memberService);
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
           
            this.svcID = orderDetail.OriginalServiceId;
            this.name = orderDetail.ServiceSnapShot.Name;
            this.type = orderDetail.ServiceSnapShot.ServiceTypeName;
            
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
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_userObj Adap(MemberDto customer)
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
        public RespDataSHM_snapshots_item_orderSnapshots_item_orderObj_storeObj Adap(ServiceOrder order,IDZMembershipService memberService)
        {
            this.userID =order.ServiceBusinessOwnerId;

            MemberDto member = memberService.GetUserById(order.ServiceBusinessOwnerId);

            this.alias = member.NickName;
            this.imgUrl = member.AvatarUrl;
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
        public RespDataSHM_snapshots_item_orderSnapshots_item_worktimeObj Adap(ServiceOrder order, WorkTimeSnapshot snap)// ServiceOpenTimeForDaySnapShotForOrder snap)
        {
            this.startTime =snap.TimePeriod.StartTime.ToString();//.ToString();
            this.endTime = snap.TimePeriod.EndTime.ToString();//.ToString();
            this.week =((int)  Convert.ToDateTime(order.TargetTime).DayOfWeek) .ToString();
            this.open = "Y";
            this.maxOrder = snap.MaxOrderForWorkDay.ToString();
            return this;
        }
            }
   

    public class ReqDataSHM001007
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string svcID { get; set; }
    }
}
