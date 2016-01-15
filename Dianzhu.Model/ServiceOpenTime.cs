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
        public ServiceOpenTime()
        {
            OpenTimeForDay = new List<ServiceOpenTimeForDay>();
            Enabled = true;
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 该日最大接大量
        /// </summary>
        public virtual int MaxOrderForDay { get; set; }
        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>
        public virtual DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool Enabled { get; set; }
        public virtual IList<ServiceOpenTimeForDay> OpenTimeForDay { get; set; }
        /// <summary>
        /// 增加服务时间段. 判断增加的时间段是否重合.
        /// </summary>
        /// <param name="period"></param>
        public virtual void AddServicePeriod(ServiceOpenTimeForDay period)
        {
            bool isConflict = false;
            foreach (ServiceOpenTimeForDay d in this.OpenTimeForDay)
            {
                if (!( period.PeriodStart > d.PeriodEnd || period.PeriodEnd < d.PeriodStart))
                {
                    isConflict = true;
                }
            }
            if (isConflict)
            {
                throw new Exception("服务时间段不能重合.ID:" + this.Id);
            }
            else
            {
                this.OpenTimeForDay.Add(period);
            }
        }
        
    }
    public class ServiceOpenTimeForDay
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 该事件段内的最大接单数量
        /// </summary>
        public virtual int MaxOrderForOpenTime { get; set; }
        public virtual bool Enabled { get; set; }
        string timeStart;
        string timeEnd;
        int periodStart, periodEnd;
        public virtual string TimeStart
        {
            get {
              //  periodStart = TimeStringToPeriod(timeStart); 
                return timeStart; }
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
            get { 
               // periodEnd = TimeStringToPeriod(timeEnd); 
                return timeEnd; }
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
        public virtual int PeriodEnd { get { return periodEnd; } set { periodEnd=value;} }

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
    }

}
