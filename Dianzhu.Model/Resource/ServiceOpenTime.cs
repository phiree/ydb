using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务每天参数设定
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
                throw new Exception("服务时间段不能重合.ID:" + this.Id + ";重合时间：" + period.TimeStart + "-" + period.TimeEnd);
            }
            else
            {
                this.OpenTimeForDay.Add(period);
            }
        }
        
    }
   


}
