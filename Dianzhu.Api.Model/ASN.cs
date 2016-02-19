using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataASN_orderObj
    {
        public string orderId { get; set; }
        public string createTime { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string exDoc { get; set; }
        public string clientName { get; set; }
        public IList<string> assignStaffId { get; set; }
        public RespDataASN_orderObj()
        {
            this.assignStaffId = new List<string>();
        }

        public RespDataASN_orderObj Adapt(ServiceOrder order,IList<OrderAssignment> oaList)
        {
            this.orderId = order.Id.ToString();
            this.createTime = order.OrderCreated.ToString("yyyyMMddHHmmss");
            this.type = order.Service.ServiceType.ToString();
            this.address = order.TargetAddress;
            this.exDoc = order.Memo;
            this.clientName = order.CustomerName == null ? order.Customer.DisplayName : order.CustomerName;            
            foreach (OrderAssignment oa in oaList)
            {
                this.assignStaffId.Add(oa.AssignedStaff.Id.ToString());
            }

            return this;
        }
    }

    public class RespDataASN_staffObj
    {
        public string staffId { get; set; }
        public string nickName { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public IList<string> assignOrderId { get; set; }
        public RespDataASN_staffObj Adapt(Staff staff,IList<OrderAssignment> oaList)
        {
            this.staffId = staff.Id.ToString();
            this.nickName = staff.NickName;
            this.name = staff.Name;
            this.gender = staff.Gender;
            this.phone = staff.Phone;
            if (oaList != null)
            {
                foreach(OrderAssignment oa in oaList)
                {
                    this.assignOrderId.Add(oa.Order.Id.ToString());
                }
            }
            else
            {
                this.assignOrderId = new List<string>();
            }

            return this;
        }
    }

    public class RespDataASN_assignObj
    {
        public string assigned { get; set; }
        public RespDataASN_orderObj orderObj { get; set; }
        public RespDataASN_assignObj Adapt(Staff staff, IList<OrderAssignment> oaList)
        {
            if (oaList.Count > 0)
            {
                foreach(OrderAssignment oa in oaList)
                {
                    if (oa.AssignedStaff == staff)
                    {
                        this.assigned = "1";
                        break;
                    }
                }
            }
            else
            {
                this.assigned = "0";
            }

            return this;
        }
    }

    public class ReqDataASN001005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderId { get; set; }
        public string staffId { get; set; }
    }

    public class RespDataASN001005
    {
        public RespDataASN_assignObj assignObj { get; set; }
    }

    public class ReqDataASN001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string staffId { get; set; }
        public IList<string> arrayOrderId { get; set; }
    }

    public class ReqDataASN002007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string orderId { get; set; }
        public IList<string> arrayStaffId { get; set; }
    }
}
