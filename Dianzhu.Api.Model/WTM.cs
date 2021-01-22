using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dianzhu.Config;
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.Api.Model
{
    public class RespDataWTM_workTimeObj
    {
        public string workTimeID { get; set; }
        public string tag { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string week { get; set; }
        public string open { get; set; }
        public string maxOrder { get; set; }
        public RespDataWTM_workTimeObj Adapt(ServiceOpenTimeForDay sotDay)
        {
            this.workTimeID = sotDay.Id.ToString();
            this.tag = string.Empty;
            this.startTime = sotDay.TimePeriod.StartTime.ToString() ?? string.Empty;
            this.endTime = sotDay.TimePeriod.EndTime.ToString() ?? string.Empty;
            this.week = WeekToString(sotDay.ServiceOpenTime.DayOfWeek);
            this.open = sotDay.Enabled ? "Y" : "N";
            this.maxOrder = sotDay.MaxOrderForOpenTime.ToString();

            return this;
        }

        private string WeekToString(DayOfWeek week)
        {
            string str = string.Empty;
            switch (week)
            {
                case DayOfWeek.Monday: str = "1"; break;
                case DayOfWeek.Tuesday: str = "2"; break;
                case DayOfWeek.Wednesday: str = "3"; break;
                case DayOfWeek.Thursday: str = "4"; break;
                case DayOfWeek.Friday: str = "5"; break;
                case DayOfWeek.Saturday: str = "6"; break;
                case DayOfWeek.Sunday: str = "7"; break;
                default:break;
            }
            return str;
        }
    }

    #region WTM001001

    public class ReqDataWTM001001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string svcID { get; set; }
        public RespDataWTM_workTimeObj workTimeObj { get; set; }
        public string repeat { get; set; }
    }

    public class RespDataWTM001001
    {
        public IList<string> arrayData { get; set; }
    }

    #endregion

    #region WTM001002

    public class ReqDataWTM001002
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string workTimeID { get; set; }
        public string svcID { get; set; }
       
    }

    #endregion

    #region WTM001003

    public class ReqDataWTM001003
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string svcID { get; set; }


        public RespDataWTM_workTimeObj workTimeObj { get; set; }
    }

    public class RespDataWTM001003
    {
        public RespDataWTM_workTimeObj workTimeObj { get; set; }
        public RespDataWTM001003(string Id)
        {
            this.workTimeObj = new RespDataWTM_workTimeObj();
            this.workTimeObj.workTimeID = Id;
            this.workTimeObj.tag = null;
            this.workTimeObj.startTime = null;
            this.workTimeObj.endTime = null;
            this.workTimeObj.week = null;
            this.workTimeObj.open = null;
            this.workTimeObj.maxOrder = null;
        }
    }

    #endregion

    #region WTM001004

    public class ReqDataWTM001004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string svcID { get; set; }
        public string week { get; set; }
    }

    public class RespDataWTM001004
    {
        public string sum { get; set; }
    }

    #endregion

    #region WTM001005

    public class ReqDataWTM001005
    {
        public string workTimeID { get; set; }
    }

    public class RespDataWTM001005
    {
        public RespDataWTM_workTimeObj workTimeObj { get; set; }
    }

    #endregion

    #region WTM001006

    public class ReqDataWTM001006
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string svcID { get; set; }
        public string week { get; set; }
    }

    public class RespDataWTM001006
    {
        public IList<RespDataWTM_workTimeObj> arrayData { get; set; }
    }

    #endregion
}
