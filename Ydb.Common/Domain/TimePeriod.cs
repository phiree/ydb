using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Domain
{
    /// <summary>
    /// 服务时间段,如果是跨日的时间段??比如:22:00至次日03:00
    /// </summary>
    public class TimePeriod
    {
        public Time StartTime { get; set; }
        public Time EndTime { get; set; }
        internal TimePeriod() { }
        public TimePeriod(Time start, Time end)
        {
            if (end < start)
            {
                throw new Exception("开始时间应该小于结束时间");
            }
            StartTime = start;
            EndTime = end;
        }
        public override string ToString()
        {
            return string.Format("{0}-{1}", StartTime, EndTime);
        }
        public static bool operator ==(TimePeriod period, TimePeriod otherPeriod)
        {
            return period.StartTime == otherPeriod.StartTime && period.EndTime == otherPeriod.EndTime;
        }
        public static bool operator !=(TimePeriod period, TimePeriod otherPeriod)
        {
            return period.StartTime != otherPeriod.StartTime || period.EndTime != otherPeriod.EndTime;
        }
        public override bool Equals(object obj)
        {
            TimePeriod otherPeriod = obj as TimePeriod;
            return  StartTime == otherPeriod.StartTime && EndTime == otherPeriod.EndTime;
        }
    }
}
