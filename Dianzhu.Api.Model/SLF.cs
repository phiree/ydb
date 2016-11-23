using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.Api.Model
{
    #region SLF001007
    public class ReqDataSLF001007
    {
        public string serviceId { get; set; }
        public string date { get; set; }
    }

    public class RespDataSLF00107_Obj
    {
        public Guid openTimeId { get; set; }
        public string date { get; set; }
        public int dayMaxOrder { get; set; }
        public int dayDoneOrder { get; set; }
        public bool dayEnable { get; set; }
        public RespDataSLF00107_Obj Adapt(DateTime date, ServiceOpenTime opentime)
        {
            this.openTimeId = opentime.Id;
            this.date = date.ToString("yyyy-MM-dd");
            this.dayMaxOrder = opentime.MaxOrderForDay;
            this.dayEnable = opentime.Enabled;
            return this;
        }
    }

    public class RespDataSLF001007
    {
        public IList<RespDataSLF00107_Obj> arrayData { get; set; }
    }
    #endregion

    #region SLF002003
    public class ReqDataSLF002003
    {
        public string openTimeForDayId { get; set; }

        public ReqDataSLF002003_PostData postData { get; set; }
    }

    public class ReqDataSLF002003_PostData
    {
        public bool timeEnable { get; set; }
        public int maxNum { get; set; }
    }
    #endregion

    #region SLF002006
    public class ReqDataSLF002006
    {
        public string serviceId { get; set; }
        public string date { get; set; }
    }
    public class RespDataSLF002006
    {
        public IList<RespDataSLF00206_arrayData> arrayData { get; set; }


    }
    public class RespDataSLF00206_arrayData
    {
        public RespDataSLF00206_arrayData()
        {
            arrayGoods = new List<RespDataSLF00206_arrayOrder>();
        }
        public Guid Id { get; set; }
        public string date { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public int maxNum { get; set; }
        public int doneNum { get; set; }
        public bool timeEnable { get; set; }
        public RespDataSLF00206_arrayData Adap(DateTime date, ServiceOpenTimeForDay openTime, IList<ServiceOrder> orders)
        {
            this.Id = openTime.Id;
            this.date = date.ToShortDateString();
            this.timeEnd = date.AddMinutes(openTime.TimePeriod.EndTime.TimeValue).ToString("yyyy-MM-dd hh:mm:ss");
            this.timeStart = date.AddMinutes(openTime.TimePeriod.StartTime.TimeValue).ToString("yyyy-MM-dd hh:mm:ss");
            this.maxNum = openTime.MaxOrderForOpenTime;
            this.timeEnable = openTime.Enabled;

            foreach (ServiceOrder order in orders)
            {
                arrayGoods.Add(new RespDataSLF00206_arrayOrder().Adap(order));
            }

            this.doneNum = arrayGoods.Count;
            return this;

        }
        public IList<RespDataSLF00206_arrayOrder> arrayGoods { get; set; }

    }
    public class RespDataSLF00206_arrayOrder
    {
        public string orderId { get; set; }
        public string status { get; set; }
        public string createdTime { get; set; }
        public RespDataSLF00206_arrayOrder Adap(ServiceOrder order)
        {
            this.orderId = order.Id.ToString();
            this.status = order.OrderStatus.ToString();
            this.createdTime = order.OrderCreated.ToString("yyyy-MM-dd hh:mm:ss");
            return this;
        }
    }
    #endregion
}
