using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.DomainModel
{
    public class ServiceOpenTimeForDay :  Entity<Guid>
    {
        public ServiceOpenTimeForDay()
        {
            Enabled = true;
        }
        public ServiceOpenTimeForDay(string tag, int maxOrderForOpenTime, ServiceOpenTime sot, TimePeriod timePeriod) : this()
        {
            this.Tag = tag;
            this.MaxOrderForOpenTime = maxOrderForOpenTime;
            this.ServiceOpenTime = sot;
            this.TimePeriod = timePeriod;

        }
        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Tag { get; set; }

        /// <summary>
        /// 该事件段内的最大接单数量
        /// </summary>
        public virtual int MaxOrderForOpenTime { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual ServiceOpenTime ServiceOpenTime { get; set; }

        protected string timeStart;
        protected string timeEnd;
        public virtual TimePeriod TimePeriod
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        //public virtual int PeriodStart { get { return TimeStringToPeriod(TimeStart); } protected set { } }
        //public virtual int PeriodEnd { get { return TimeStringToPeriod(TimeEnd); } protected set { } }

        ///// <summary>
        ///// 文本格式的时间,转换成分钟,用于计算两个时间之间的间隔分数.
        ///// 4:30 等于 4*60+30=270分钟.
        ///// </summary>
        ///// <param name="time"></param>
        ///// <returns></returns>
        //private int TimeStringToPeriod(string time)
        //{
        //    if (time == null)
        //    {
        //        int c = 0;
        //    }
        //    string[] arr = time.Split(new char[] { ':', '：' });
        //    if (arr.Length != 2)
        //    {
        //        throw new FormatException("时间格式不正确");
        //    }
        //    int hourPart = int.Parse(arr[0]);
        //    int minitePart = int.Parse(arr[1]);
        //    return hourPart * 60 + minitePart;
        //}

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="newSotForDay"></param>
        public virtual void CopyTo(ServiceOpenTimeForDay newSotForDay)
        {
            newSotForDay.Id = Id;
            newSotForDay.Tag = Tag;
            newSotForDay.MaxOrderForOpenTime = MaxOrderForOpenTime;
            newSotForDay.Enabled = Enabled;
            newSotForDay.ServiceOpenTime = ServiceOpenTime;
            newSotForDay.TimePeriod = TimePeriod;

        }

        //给定时间在该范围内?
        public virtual bool IsIn(Time time)
        {
            return TimePeriod.StartTime <=time && time <= TimePeriod.EndTime;

        }

        /// <summary>
        /// todo:refactor:快照~咔~~嚓!快照初始化应该是在订单领域..
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        //public virtual ServiceOpenTimeForDaySnapShotForOrder GetSnapShop(DateTime datetime)
        //{
        //    return new ServiceOpenTimeForDaySnapShotForOrder(
        //           this.Id,
        //        this.MaxOrderForOpenTime,
        //        datetime.Date,
        //       TimePeriod

        //        );
        //}


    }
}
