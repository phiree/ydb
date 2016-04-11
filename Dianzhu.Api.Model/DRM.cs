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
    public class RespDataDRM_deliveryObj
    {
        public string deliveryID { get; set; }
        public string orderID { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public RespDataDRM_svcOBj svcObj { get; set; }
        public RespDataDRM_workTimeObj workTimeObj { get; set; }
    }

    public class RespDataDRM_svcOBj
    {
        public string svcID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string introduce { get; set; }
        public string area { get; set; }
        public string startAt { get; set; }
        public string unitPrice { get; set; }
        public string deposit { get; set; }
        public string appointmentTime { get; set; }
        public string serviceTimes { get; set; }
        public string doorService { get; set; }
        public string serviceObject { get; set; }
        public string payWay { get; set; }
        public string tag { get; set; }
        public string open { get; set; }
        public string maxOrderString { get; set; }
        public RespDataDRM_svcOBj Adapt(ServiceSnapShotForOrder sssfOrder, DZService service, IList<DZTag> tags)
        {
            this.svcID = service.Id.ToString();
            this.name = sssfOrder.ServiceName ?? service.Name ?? string.Empty;
            this.type = service.ServiceType != null ? service.ServiceType.Name : string.Empty;
            this.introduce = sssfOrder.Description ?? service.Description ?? string.Empty;
            this.area = service.GetServiceArea();
            this.startAt = sssfOrder.MinPrice > 0 ? sssfOrder.MinPrice.ToString("0.00") : service.MinPrice.ToString("0.00");
            this.unitPrice = sssfOrder.UnitPrice > 0 ? sssfOrder.UnitPrice.ToString("0.00") : service.UnitPrice.ToString("0.00");
            this.deposit = sssfOrder.DepositAmount > 0 ? sssfOrder.DepositAmount.ToString("0.00") : service.DepositAmount.ToString("0.00");
            this.appointmentTime = service.OrderDelay.ToString();
            service.OpenTimes.ToList().ForEach(x => this.serviceTimes += x.Id + ",");
            this.serviceTimes = this.serviceTimes != null ? this.serviceTimes.TrimEnd(',') : "";
            this.doorService = sssfOrder.ServiceMode == enum_ServiceMode.ToHouse ? "Y" : "N";
            this.serviceObject = service.IsForBusiness ? "company" : "all";
            this.payWay = service.AllowedPayType != 0 ? service.AllowedPayType.ToString() : enum_PayType.None.ToString();
            tags.ToList().ForEach(x => this.tag += x.Text + ",");
            this.tag = this.tag != null ? this.tag.TrimEnd(',') : "";
            this.open = service.Enabled ? "Y" : "N";

            if (service.OpenTimes.Count > 0)
            {
                IList<ServiceOpenTime> openTimesList = service.OpenTimes.OrderBy(x => x.DayOfWeek).ToList();
                openTimesList.Add(openTimesList[0]);
                openTimesList.RemoveAt(0);
                foreach (ServiceOpenTime sot in openTimesList)
                {
                    this.maxOrderString += sot.MaxOrderForDay.ToString() + ",";
                }
                this.maxOrderString = this.maxOrderString.TrimEnd(',');
            }
            else
            {
                this.maxOrderString = "";
            }
            return this;
        }
    }

    public class RespDataDRM_workTimeObj
    {
        public string workTimeID { get; set; }
        public string tag { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string week { get; set; }
        public string open { get; set; }
        public string maxOrder { get; set; }
        public RespDataDRM_workTimeObj Adapt(ServiceOpenTimeForDaySnapShotForOrder sotfdssfOrder)
        {
            //this.workTimeID = sotDay.Id.ToString();
            this.tag = string.Empty;
            this.startTime = sotfdssfOrder.PeriodBegin > 0 ? sotfdssfOrder.PeriodBegin / 60 + ":" + sotfdssfOrder.PeriodBegin % 60 : string.Empty;
            this.endTime = sotfdssfOrder.PeriodEnd > 0 ? sotfdssfOrder.PeriodEnd / 60 + ":" + sotfdssfOrder.PeriodEnd % 60 : string.Empty;
            this.week = getWeekDay(sotfdssfOrder.Date.Year, sotfdssfOrder.Date.Month, sotfdssfOrder.Date.Day).ToString();
            //this.open = sotDay.Enabled ? "Y" : "N";
            this.maxOrder = sotfdssfOrder.MaxOrder > 0 ? sotfdssfOrder.MaxOrder.ToString() : string.Empty;

            return this;
        }

        private int getWeekDay(int y, int m, int d)
        {
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            return week;
        }
    }

    public class ReqDataDRM001006
    {
        public string orderID { get; set; }
    }

    public class RespDataDRM001006
    {
        public IList<RespDataDRM_deliveryObj> arrayData { get; set; }
    }
}
