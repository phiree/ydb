using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.Order.DomainModel;

namespace Dianzhu.Api.Model
{
    public class RespDataRMM_remindObj
    {
        public string remindID { get; set; }
        public string title { get; set; }
        public string time { get; set; }
        public string content { get; set; }
        public string open { get; set; }
        public string orderID { get; set; }
        public RespDataRMM_remindObj Adapt(ServiceOrderRemind remind)
        {
            this.remindID = remind.Id.ToString();
            this.title = remind.Title ?? string.Empty;
            this.time = remind.RemindTime.ToString("yyyyMMddHHmmss");
            this.content = remind.Content ?? string.Empty;
            this.open = remind.Open == true ? "Y" : "N";
            this.orderID = remind.OrderId.ToString();

            return this;
        }
    }

    public class ReqDataRMM001003
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string remindID { get; set; }
        public string open { get; set; }
    }

    public class ReqDataRMM001004
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }

    public class RespDataRMM001004
    {
        public string sum { get; set; }
    }

    public class ReqDataRMM001005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string remindID { get; set; }
    }

    public class RespDataRMM001005
    {
        public RespDataRMM_remindObj remindObj { get; set; }
    }

    public class ReqDataRMM001006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }

    public class RespDataRMM001006
    {
        public IList<RespDataRMM_remindObj> arrayData { get; set; }
        public RespDataRMM001006 AdaptList(IList<ServiceOrderRemind> remindList)
        {
            this.arrayData = new List<RespDataRMM_remindObj>();
            RespDataRMM_remindObj obj;
            foreach (ServiceOrderRemind remind in remindList)
            {
                obj = new RespDataRMM_remindObj().Adapt(remind);
                this.arrayData.Add(obj);
            }

            return this;
        }
    }
}
