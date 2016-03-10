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
        public RespDataORM_Order Adap(ServiceOrder order)
        {
            if (order != null)
            {
                this.orderObj = new RespDataORM_orderObj().Adap(order);
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
        public string money { get; set; }
        public string address { get; set; }
        public string km { get; set; }
        public string deliverySum { get; set; }
        // public string paylink { get; set; }
        public RespDataORM_svcObj svcObj { get; set; }
        public RespDataORM_UserObj userObj { get; set; }
        public RespDataORM_storeObj storeObj { get; set; }

        public RespDataORM_orderObj Adap(ServiceOrder order)
        {
            this.orderID = order.Id.ToString();
            //todo: serviceorder change
            this.title = order.Title ?? string.Empty;
            this.status = order.OrderStatus.ToString() ?? string.Empty;
            if (order.OrderCreated > DateTime.MinValue)
            {
                this.startTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderCreated);
            }
            else
            {
                this.startTime = string.Empty;
            }
            if (order.OrderFinished > DateTime.MinValue)
            {
                this.endTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderCreated);
            }
            else
            {
                this.endTime = string.Empty;
            }
            this.exDoc = order.Details[0].Description ?? string.Empty;
            this.money = order.OrderAmount.ToString("0.00");
            this.address = order.TargetAddress ?? string.Empty;
            this.km = string.Empty;

            if (order != null)
            {
                this.svcObj = new RespDataORM_svcObj().Adap(order);
            }
            if (order.Customer != null)
            {
                this.userObj = new RespDataORM_UserObj().Adap(order.Customer);
            }
            //todo,这里只能获取系统内订单
            if (order.Service != null)
            {
                this.storeObj = new RespDataORM_storeObj().Adap(order.Service.Business);
            }

            //todo:出货记录还没有加
            //this.deliverySum

            return this;
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
        public RespDataORM_svcObj Adap(ServiceOrder order)
        {
            this.svcID = order.Service != null ? order.Service.Id.ToString() : order.Id.ToString();
            this.name = order.Service != null ? order.Service.Name : order.Title;
            this.type = order.Service != null ? order.Service.ServiceType.ToString() : string.Empty;
            if (order.OrderServerStartTime > DateTime.MinValue)
            {
                this.startTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderServerStartTime);
            }
            else
            {
                this.startTime = string.Empty;
            }
            if (order.OrderServerFinishedTime > DateTime.MinValue)
            {
                this.endTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderServerFinishedTime);
            }
            else
            {
                this.endTime = string.Empty;
            }
            this.deposit = order.Service != null ? order.Service.DepositAmount.ToString() : string.Empty;

            return this;
        }

    }

    public class RespDataORM_orderStatusObj
    {
        public string status { get; set; }
        public string time { get; set; }
        public string lastStatus { get; set; }
        public RespDataORM_orderStatusObj Adap(ServiceOrderStateChangeHis orderHis)
        {
            this.status = orderHis.NewStatus.ToString();
            this.time = string.Format("{0:yyyyMMddHHmmss}", orderHis.CreatTime);
            this.lastStatus = orderHis.OldStatus.ToString() ?? string.Empty;

            return this;
        }
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

        public void AdapList(IList<ServiceOrder> serviceOrderList)
        {
            foreach (ServiceOrder order in serviceOrderList)
            {
                RespDataORM_orderObj adapted_order = new RespDataORM_orderObj().Adap(order);
                arrayData.Add(adapted_order);
            }
        }
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
            this.imgUrl = customerService.AvatarUrl ?? string.Empty;
            this.alias = customerService.DisplayName ?? string.Empty;
            this.userName = customerService.UserName ?? string.Empty;
            return this;
        }
    }
    #endregion

    #region ORM002002
    public class ReqDataORM002002
    {

        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }


    }
    public class RespDataORM002002
    {
        public RespDataORM002002()
        {
            orderID = string.Empty;
        }
        public string orderID { get; set; }
        public RespDataORM002002_payObj payObj { get; set; }

    }
    public class RespDataORM002002_payObj
    {
        public string type { get; set; }

        public string url { get; set; }
        public RespDataORM002002_payObj Adap(ServiceOrder order)
        {
            this.type = "alipay";
           //todo: use payment.buildpaylink instead;
           // this.url = order.p BuildPayLink(Dianzhu.Config.Config.GetAppSetting("PayServer"), enum_PayTarget.Deposit);

            return this;
        }
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
}
