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
    public class RespDataSVC_svcObj
    {
        //public string storeID { get; set; }
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
        public RespDataSVC_svcObj Adapt(DZService service, IList<DZTag> tags)
        {
            //this.storeID = service.Business.Id.ToString();
            this.svcID = service.Id.ToString();
            this.name = service.Name ?? "";
            this.type = service.ServiceType != null ? service.ServiceType.Name : "";
            this.introduce = service.Description ?? "";
            this.area = service.GetServiceArea();
            this.startAt = service.MinPrice.ToString("0.00");
            this.unitPrice = service.UnitPrice.ToString("0.00");
            this.deposit = service.DepositAmount.ToString("0.00");
            this.appointmentTime = service.OrderDelay.ToString();
            service.OpenTimes.ToList().ForEach(x => this.serviceTimes += x.Id + ",");
            this.serviceTimes = this.serviceTimes != null ? this.serviceTimes.TrimEnd(',') : "";
            this.doorService = service.ServiceMode == enum_ServiceMode.ToHouse ? "Y" : "N";
            this.serviceObject = service.IsForBusiness ? "company" : "all";
            this.payWay = service.AllowedPayType != 0 ? service.AllowedPayType.ToString() : enum_PayType.None.ToString();
            tags.ToList().ForEach(x => this.tag += x.Text + ",");
            this.tag = this.tag != null ? this.tag.TrimEnd(',') : "";
            this.open = service.Enabled ? "Y" : "N";
            //this.maxOrderString = service.MaxOrdersPerDay.ToString();
            if (service.OpenTimes.Count > 0)
            {
                IList<ServiceOpenTime> openTimesList = service.OpenTimes.OrderBy(x => x.DayOfWeek).ToList();
                openTimesList.Add(openTimesList[0]);
                openTimesList.RemoveAt(0);
                foreach (ServiceOpenTime sot in openTimesList)
                {
                    this.maxOrderString += sot.MaxOrderForDay.ToString()+",";
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

    #region SVC001001

    public class ReqDataSVC001001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public RespDataSVC_svcObj svcObj { get; set; }
    }

    public class RespDataSVC001001
    {
        public string svcID { get; set; }
    }

    #endregion

    #region SVC001002

    public class ReqDataSVC001002
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string svcID { get; set; }
    }

    #endregion

    #region SVC001003

    public class ReqDataSVC001003
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public RespDataSVC_svcObj svcObj { get; set; }
    }

    public class RespDataSVC001003
    {
        public RespDataSVC_svcObj svcObj { get; set; }
        public RespDataSVC001003(string svcID)
        {
            this.svcObj = new RespDataSVC_svcObj();
            this.svcObj.svcID = svcID;
            this.svcObj.name = null;
            this.svcObj.type = null;
            this.svcObj.introduce = null;
            this.svcObj.area = null;
            this.svcObj.startAt = null;
            this.svcObj.unitPrice = null;
            this.svcObj.unitPrice = null;
            this.svcObj.appointmentTime = null;
            this.svcObj.doorService = null;
            this.svcObj.serviceObject = null;
            this.svcObj.payWay = null;
            this.svcObj.tag = null;
            this.svcObj.open = null;
            this.svcObj.maxOrderString = null;
        }
    }

    #endregion

    #region SVC001004

    public class ReqDataSVC001004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
    }

    public class RespDataSVC001004
    {
        public string sum { get; set; }
    }

    #endregion

    #region SVC001005

    public class ReqDataSVC001005
    {
        public string svcID { get; set; }
    }

    public class RespDataSVC001005
    {
        public RespDataSVC_svcObj svcObj { get; set; }
    }

    #endregion

    #region SVC001006

    public class ReqDataSVC001006
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
    }

    public class RespDataSVC001006
    {
        public IList<RespDataSVC_svcObj> arrayData { get; set; }
    }

    #endregion


}
