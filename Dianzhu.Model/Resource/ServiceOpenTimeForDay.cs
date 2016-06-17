using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class ServiceOpenTimeForDay
    {
        public ServiceOpenTimeForDay()
        {
            Enabled = true;
        }
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Tag { get; set; }

        /// <summary>
        /// 该事件段内的最大接单数量
        /// </summary>
        public virtual int MaxOrderForOpenTime { get;   set; }
        public virtual bool Enabled { get; set; }
        public virtual ServiceOpenTime ServiceOpenTime { get;   set; }

        protected string timeStart;
        protected string timeEnd;
        public virtual string TimeStart
        {
            get { return timeStart; }
            set
            {
                timeStart = value;
                PeriodStart = TimeStringToPeriod(timeStart);
            }
        }
        public virtual string TimeEnd
        {
            get { return timeEnd; }
            set
            {
                timeEnd = value;
                PeriodEnd = TimeStringToPeriod(timeEnd);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual int PeriodStart { get { return TimeStringToPeriod(TimeStart); } protected set { } }
        public virtual int PeriodEnd { get { return TimeStringToPeriod(TimeEnd); } protected set { } }

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

        //给定时间在该范围内?
        public virtual bool IsIn(DateTime datetime)
        {
            string strTime = datetime.ToString("HH:MM");
            int timePeriod = TimeStringToPeriod(strTime);
            return timePeriod >= PeriodStart && timePeriod <= PeriodEnd;

        }
        /// <summary>
        /// 快照~咔~~嚓!
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public virtual ServiceOpenTimeForDaySnapShotForOrder GetSnapShop(DateTime datetime)
        {
            return new ServiceOpenTimeForDaySnapShotForOrder(
                this.MaxOrderForOpenTime,
                datetime.Date,
                PeriodStart,
                PeriodEnd
                );
        }
    }
}
