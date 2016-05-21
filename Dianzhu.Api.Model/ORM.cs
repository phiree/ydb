using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Dianzhu.Model.Enums;

namespace Dianzhu.Api.Model
{
    #region orm接口公用的类
    public class RespDataORM_Order
    {
        public RespDataORM_orderObj orderObj { get; set; }
        public RespDataORM_Order Adap(ServiceOrder order, ServiceOrderPushedService pushService)
        {
            if (order != null)
            {
                this.orderObj = new RespDataORM_orderObj().Adap(order, pushService);
            }
            return this;
        }
    }
    public class RespDataORM_orderObj
    {
        public string orderID { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string exDoc { get; set; }
        public string orderAmount { get; set; }
        public string negotiateAmount { get; set; }
        public string address { get; set; }
        public string km { get; set; }
        public string deliverySum { get; set; }
        public RespDataORM_orderStatusObj orderStatusObj { get; set; }
        // public string paylink { get; set; }
        public RespDataORM_svcObj svcObj { get; set; }
        public RespDataORM_UserObj userObj { get; set; }
        public RespDataORM_storeObj storeObj { get; set; }

        public RespDataORM_orderObj Adap(ServiceOrder order, ServiceOrderPushedService pushSevice)
        {
            this.orderID = order.Id.ToString();
            //todo: serviceorder change
            this.title = order.Title ?? string.Empty;
            this.status = order.OrderStatus.ToString() ?? string.Empty;
            if (order.OrderConfirmTime > DateTime.MinValue)
            {
                this.startTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderConfirmTime);
            }
            else
            {
                this.startTime = string.Empty;
            }
            if (order.OrderFinished > DateTime.MinValue)
            {
                this.endTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderFinished);
            }
            else
            {
                this.endTime = string.Empty;
            }
            this.exDoc = order.Description ?? string.Empty;
            this.orderAmount = order.OrderAmount.ToString("0.00");
            this.negotiateAmount = order.NegotiateAmount.ToString("0.00");
            this.address = order.TargetAddress ?? string.Empty;
            this.km = string.Empty;
            this.deliverySum = "0";//todo:出货记录不明确

            if (order.Details.Count > 0)
            {
                this.svcObj = new RespDataORM_svcObj().Adap(order.Details[0], null);
                this.storeObj = new RespDataORM_storeObj().Adap(order.Details[0].OriginalService.Business);

                if (order.NegotiateAmount <= 0)
                {
                    this.orderAmount = this.negotiateAmount = (order.Details[0].UnitAmount * order.Details[0].OriginalService.UnitPrice).ToString("0.00");
                }
            }
            else if (order.Details.Count == 0 && pushSevice != null)
            {
                this.svcObj = new RespDataORM_svcObj().Adap(null, pushSevice);
                this.storeObj = new RespDataORM_storeObj().Adap(pushSevice.OriginalService.Business);

                if (order.NegotiateAmount <= 0)
                {
                    this.orderAmount = this.negotiateAmount = (pushSevice.UnitAmount * pushSevice.OriginalService.UnitPrice).ToString("0.00");
                }
            }
            else
            {
                this.svcObj = null;
                this.storeObj = null;
            }
            if (order.Customer != null)
            {
                this.userObj = new RespDataORM_UserObj().Adap(order.Customer);
            }
            //todo,这里只能获取系统内订单
            //if (order.Service != null)
            //{
            //    this.storeObj = new RespDataORM_storeObj().Adap(order.Service.Business);
            //}

            //todo:出货记录还没有加
            //this.deliverySum

            return this;
        }

        public RespDataORM_orderObj SetOrderStatusObj(RespDataORM_orderObj orderObj, ServiceOrderStateChangeHis orderHis)
        {
            if (orderHis != null)
            {
                orderObj.orderStatusObj = new RespDataORM_orderStatusObj().Adap(orderHis);
            }
            return orderObj;
        }
    }

    public class RespDataORM_UserObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string imgUrl { get; set; }
        public RespDataORM_UserObj Adap(DZMembership member)
        {
            this.userID = member.Id.ToString();
            this.alias = member.NickName ?? string.Empty;
            this.imgUrl = string.IsNullOrEmpty(member.AvatarUrl) ? string.Empty : (Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + member.AvatarUrl);
            return this;
        }

    }

    public class RespDataORM_storeObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string imgUrl { get; set; }
        public RespDataORM_storeObj Adap(Business business)
        {
            this.userID = business.Id.ToString();
            this.alias = business.Name ?? string.Empty;
            //this.imgUrl = business.BusinessAvatar.ImageName;
            this.imgUrl = string.IsNullOrEmpty(business.BusinessAvatar.ImageName) ? string.Empty : (Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + business.BusinessAvatar.ImageName);
            return this;
        }
    }

    public class RespDataORM_svcObj
    {
        public string svcID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string deposit { get; set; }
        public string unitPrice { get; set; }
        public string introduce { get; set; }
        public string area { get; set; }
        public string startAt { get; set; }
        public string appointmentTime { get; set; }
        public string tag { get; set; }
        public string chargeUnit { get; set; }
        public RespDataORM_svcObj Adap(ServiceOrderDetail orderDetail,ServiceOrderPushedService pushService)
        {
            if (orderDetail != null)
            {
                if (orderDetail.OriginalService != null)
                {
                    this.svcID = orderDetail.OriginalService.Id.ToString();
                    this.type = orderDetail.OriginalService.ServiceType.ToString();
                    this.introduce = orderDetail.OriginalService.Description;
                    this.area = orderDetail.OriginalService.GetServiceArea();
                    this.startAt = orderDetail.OriginalService.MinPrice.ToString("0.00");
                    this.appointmentTime = orderDetail.OriginalService.OrderDelay.ToString();
                    this.chargeUnit = orderDetail.OriginalService.ChargeUnitFriendlyName;
                }
                else
                {
                    this.svcID = string.Empty;
                    this.type = string.Empty;
                    this.introduce = string.Empty;
                    this.area = string.Empty;
                    this.startAt = "0";
                    this.appointmentTime = string.Empty;
                    this.tag = string.Empty;
                    this.chargeUnit = string.Empty;
                }
                
                this.name = orderDetail.ServieSnapShot.ServiceName ?? string.Empty;
                if (orderDetail.TargetTime > DateTime.MinValue)
                {
                    this.startTime = string.Format("{0:yyyyMMddHHmmss}", orderDetail.TargetTime);
                }
                else
                {
                    this.startTime = string.Empty;
                }
                this.endTime = string.Empty;
                this.deposit = orderDetail.ServieSnapShot.DepositAmount.ToString("0.00");
                this.unitPrice = orderDetail.ServieSnapShot != null ? orderDetail.ServieSnapShot.UnitPrice.ToString("0.00") : string.Empty;
            }
            else if(orderDetail == null && pushService != null)
            {
                if (pushService.OriginalService != null)
                {
                    this.svcID = pushService.OriginalService.Id.ToString();
                    this.type = pushService.OriginalService.ServiceType.ToString();
                    this.introduce = pushService.OriginalService.Description;
                    this.area = pushService.OriginalService.GetServiceArea();
                    this.startAt = pushService.OriginalService.MinPrice.ToString("0.00");
                    this.appointmentTime = pushService.OriginalService.OrderDelay.ToString();                    
                    this.chargeUnit = pushService.OriginalService.ChargeUnitFriendlyName;
                }
                else
                {
                    this.svcID = string.Empty;
                    this.type = string.Empty;
                    this.introduce = string.Empty;
                    this.area = string.Empty;
                    this.startAt = "0";
                    this.appointmentTime = string.Empty;
                    this.tag = string.Empty;
                    this.chargeUnit = string.Empty;
                }
                
                this.name = pushService.ServiceName ?? string.Empty;
                if (pushService.TargetTime > DateTime.MinValue)
                {
                    this.startTime = string.Format("{0:yyyyMMddHHmmss}", pushService.TargetTime);
                }
                else
                {
                    this.startTime = string.Empty;
                }
                this.endTime = string.Empty;
                this.deposit = pushService.DepositAmount.ToString("0.00");
                this.unitPrice = pushService.UnitPrice.ToString("0.00");
            }
            else
            {
                return null;
            }

            return this;
        }
        public RespDataORM_svcObj SetTag(RespDataORM_svcObj svcObj, IList<DZTag> tags)
        {
            if (svcObj != null && tags.Count > 0)
            {
                foreach (DZTag tag in tags)
                {
                    this.tag += tag.Text + ",";
                }
                this.tag = this.tag.TrimEnd(',');
            }
            else
            {
                this.tag = string.Empty;
            }
            return this;
        }
    }

    public class RespDataORM_orderStatusObj
    {
        public string status { get; set; }
        public string time { get; set; }
        public string lastStatus { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public RespDataORM_orderStatusObj Adap(ServiceOrderStateChangeHis orderHis)
        {
            this.status = orderHis.NewStatus.ToString();
            this.time = string.Format("{0:yyyyMMddHHmmss}", orderHis.CreatTime);
            this.lastStatus = orderHis.OldStatus.ToString() ?? string.Empty;
            this.title = orderHis.Order.GetStatusTitleFriendly(orderHis.NewStatus);
            this.context = orderHis.Order.GetStatusContextFriendly(orderHis.NewStatus);

            return this;
        }
    }

    public class RespDataORM_refundObj
    {
        public string refundID { get; set; }
        public string orderID { get; set; }
        public string context { get; set; }
        public string amount { get; set; }
        public string resourcesUrl { get; set; }
    }

    public class RespDataORM_refundStatusObj
    {
        //public string refundStatusID { get; set; }
        //public string orderID { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public string amount { get; set; }
        public string resourcesUrl { get; set; }
        public string time { get; set; }
        public string orderStatus { get; set; }
        public string target { get; set; }
    }

    public enum enum_refundAction
    {
        reject=1,           //拒绝理赔
        askPay =2,          //要求支付赔偿金
        refund =4,          //同意理赔要求
        agree =8,           //同意商户处理
        cancel =16,         //放弃理赔
        intervention =32,   //要求介入
    }

    #endregion

    #region ORM001004
    public class ReqDataORM001004
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string target { get; set; }

    }
    public class RespDataORM001004
    {
        public string sum { get; set; }
    }
    #endregion

    #region ORM001005
    public class ReqDataORM001005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }
    #endregion

    #region ORM001006
    public class ReqDataORM001006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string target { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }

    }
    public class RespDataORM001006
    {
        public IList<RespDataORM_orderObj> arrayData { get; set; }

        public RespDataORM001006()
        {
            arrayData = new List<RespDataORM_orderObj>();
        }

        public void AdapList(Dictionary<ServiceOrder,ServiceOrderPushedService> serviceOrderList)
        {
            foreach (KeyValuePair<ServiceOrder,ServiceOrderPushedService> item in serviceOrderList)
            {
                RespDataORM_orderObj adapted_order = new RespDataORM_orderObj().Adap(item.Key,item.Value);
                arrayData.Add(adapted_order);
            }
        }
    }
    #endregion

    #region ORM001007

    public class ReqDataORM001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
    }

    public class RespDataORM001007
    {
        public IList<RespDataSVC_svcObj> arrayData { get; set; }
        public RespDataORM001007 AdaptList(Dictionary<DZService,IList<DZTag>> dic)
        {
            this.arrayData = new List<RespDataSVC_svcObj>();
            RespDataSVC_svcObj svcObj = new RespDataSVC_svcObj();
            foreach (KeyValuePair<DZService,IList<DZTag>> item in dic)
            {
                svcObj = new RespDataSVC_svcObj().Adapt(item.Key, item.Value);
                this.arrayData.Add(svcObj);
            }
            return this;
        }
    }

    #endregion

    #region ORM001008

    public class ReqDataORM001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string svcID { get; set; }
    }

    public class RespDataORM001008
    {
        public RespDataORM_orderObj orderObj { get; set; }
    }

    #endregion

    #region ORM002001
    public class ReqDataORM002001
    {

        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string manualAssignedCsId { get; set; }


    }
    public class RespDataORM002001
    {
        public RespDataORM002001()
        {
            orderID = string.Empty;
        }
        public string orderID { get; set; }
        public RespDataORM002001_cerObj cerObj { get; set; }

    }
    public class RespDataORM002001_cerObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string userName { get; set; }
        public string imgUrl { get; set; }
        public RespDataORM002001_cerObj Adap(DZMembership customerService)
        {
            this.userID = customerService.Id.ToString();
            this.imgUrl = string.Empty;
             
            this.alias = customerService.DisplayName ?? string.Empty;
            this.userName = customerService.UserName ?? string.Empty;
            return this;
        }
    }
    #endregion
    
    #region ORM002003

    public class ReqDataORM002003
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataORM002003
    {
        public string targetID { get; set; }
        public RespDataORM_storeObj storeObj { get; set;}
    }

    #endregion

    #region ORM003005
    public class ReqDataORM003005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataORM003005
    {
        public string orderID { get; set; }
        public RespDataORM_orderStatusObj orderStatusObj { get; set; }
    }
    #endregion

    #region ORM003006
    public class ReqDataORM003006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataORM003006
    {
        public string orderID { get; set; }
        public IList<RespDataORM_orderStatusObj> arrayData { get; set; }

        public RespDataORM003006()
        {
            arrayData = new List<RespDataORM_orderStatusObj>();
        }

        public void AdapList(IList<RespDataORM_orderStatusObj> serviceOrderList)
        {
            foreach (RespDataORM_orderStatusObj order in serviceOrderList)
            {
                arrayData.Add(order);
            }
        }
    }
    #endregion

    #region ORM003007
    public class ReqDataORM003007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string status { get; set; }
    }
    #endregion

    #region ORM003008

    public class ReqDataORM003008
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string negotiateAmount { get; set; }
    }

    public class RespDataORM003008
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM003009

    public class ReqDataORM003009_item
    {
        public enum_ChatTarget target { get; set; }
        public decimal value { get; set; }
    }

    public class ReqDataORM003009
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string target { get; set; }
        public string appraiseValue { get; set; }
        public string appraiseDocs { get; set; }
    }

    public class RespDataORM003009
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM005001

    public class ReqDataORM005001
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public RespDataORM_refundObj refundObj { get; set; }
    }

    public class RespDataORM005001
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM005007

    public class ReqDataORM005007
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public RespDataORM_refundObj refundObj { get; set; }
        public string refundAction { get; set; }
    }

    public class RespDataORM005007
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM005008

    public class ReqDataORM005008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string refundAction { get; set; }
    }

    public class RespDataORM005008
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM005009

    public class ReqDataORM005009
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataORM005009
    {
        public string resultStatus { get; set; }
    }

    #endregion

    #region ORM005010

    public class ReqDataORM005010
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
    }

    public class RespDataORM005010
    {
        public IList<RespDataORM_refundStatusObj> arrayData { get; set; }
    }

    #endregion

}
