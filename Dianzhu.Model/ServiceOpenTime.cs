using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务时段
    /// </summary>
    public class ServiceOpenTime
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>
        public virtual int Day { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool Enabled { get; set; }
        public virtual IList<ServiceOpenTimeForDay> OpenTimeForDay { get; set; }
    }
    public class ServiceOpenTimeForDay
    {
        public virtual Guid Id { get; set; }
        string timeStart;
        string timeEnd;
        int period;
        public virtual string TimeStart
        {
            get { return timeStart; }
            set
            {
                timeStart = value;
                period = TimeStringToPeriod(timeStart);

            }
        }
        public virtual string TimeEnd
        {
            get { return timeEnd; }
            set
            {
                timeEnd = value;
                period = TimeStringToPeriod(timeEnd);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int Period { get; set; }

        private int TimeStringToPeriod(string time)
        {
            string[] arr = time.Split(new char[] { ':', '：' });
            if (arr.Length != 2)
            {
                throw new FormatException("时间格式不正确");
            }
            int hourPart = int.Parse(arr[0]);
            int minitePart = int.Parse(arr[1]);
            return hourPart * 60 + minitePart;
        }
    }

}
