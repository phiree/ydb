using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class ServiceOpenTimeForDay
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 该事件段内的最大接单数量
        /// </summary>
        public virtual int MaxOrderForOpenTime { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual ServiceOpenTime ServiceOpenTime { get; set; }
        string timeStart;
        string timeEnd;
        int periodStart, periodEnd;
        public virtual string TimeStart
        {
            get
            {
                //  periodStart = TimeStringToPeriod(timeStart); 
                return timeStart;
            }
            set
            {
                timeStart = value;
                periodStart = TimeStringToPeriod(timeStart);
                //if (periodEnd>0&& periodStart >=periodEnd)
                //{
                //    throw new Exception("开始时间不能大于等于结束时间.ID:"+this.Id);
                //}

            }
        }
        public virtual string TimeEnd
        {
            get
            {
                // periodEnd = TimeStringToPeriod(timeEnd); 
                return timeEnd;
            }
            set
            {
                timeEnd = value;
                periodEnd = TimeStringToPeriod(timeEnd);
                //if (periodStart!=null&& periodEnd<=periodStart)
                //{
                //    throw new Exception("结束时间不能小于等于开始时间.ID:" + this.Id);
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int PeriodStart { get { return periodStart; } set { periodStart = value; } }
        public virtual int PeriodEnd { get { return periodEnd; } set { periodEnd = value; } }

        /// <summary>
        /// 文本格式的时间,转换成分钟,用于计算两个时间之间的间隔分数.
        /// 4:30 等于 4*60+30=270分钟.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="newSotForDay"></param>
        public virtual void CopyTo(ServiceOpenTimeForDay newSotForDay)
        {
            newSotForDay.Id = Id;
            newSotForDay.MaxOrderForOpenTime = MaxOrderForOpenTime;
            newSotForDay.Enabled = Enabled;
            newSotForDay.ServiceOpenTime = ServiceOpenTime;
            newSotForDay.TimeStart = TimeStart;
            newSotForDay.TimeEnd = TimeEnd;
        }
    }
}
