using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.DomainModel
{
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool Use24 { get; set; }
        internal Time() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeString">HH:mm</param>
        public Time(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
            {
                int c = 0;
            }
            string[] arr = timeString.Split(new char[] { ':', '：' });
            if (arr.Length != 2)
            {
                throw new FormatException("时间格式不正确");
            }
            int hourPart = int.Parse(arr[0]);
            int minitePart = int.Parse(arr[1]);
            if (hourPart > 24 || minitePart > 60)
            {
                throw new FormatException("时间格式不正确");
            }
            Hour = hourPart;
            Minute = minitePart;
        }
        public int TimeValue
        {
            get { return Hour * 60 + Minute; }
        }

        public static bool operator >(Time time, Time otherTime)
        {
            return time.TimeValue > otherTime.TimeValue;
        }
        public static bool operator <(Time time, Time otherTime)
        {
            return time.TimeValue < otherTime.TimeValue;
        }
        public static bool operator <=(Time time, Time otherTime)
        {
            return time.TimeValue <= otherTime.TimeValue;
        }
        public static bool operator >=(Time time, Time otherTime)
        {
            return time.TimeValue >= otherTime.TimeValue;
        }
        public static bool operator ==(Time time, Time otherTime)
        {
            return time.TimeValue == otherTime.TimeValue;
        }
        public static bool operator !=(Time time, Time otherTime)
        {
            return time.TimeValue != otherTime.TimeValue;
        }

        public static int operator -(Time time, Time otherTime)
        {
           return time.TimeValue - otherTime.TimeValue;
        }
       
        public override int GetHashCode()
        {
            return TimeValue.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            var otherTime = (Time)obj;
            return TimeValue == otherTime.TimeValue;

        }
        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}", Hour, Minute);
        }
    }
}
