using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 服务每天参数设定
    /// </summary>
 
    public class ServiceOpenTime : Entity<Guid>
 
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model.ServiceOpenTime");
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

           
            TimePeriodList periodList = new TimePeriodList(OpenTimeForDay.Select(x => x.TimePeriod).ToList());


            if (periodList.IsConflict(period.TimePeriod))
            {

                OpenTimeForDay.Add(period);
            }
            else {
                throw new Exception("时间段冲突");
            }


        }
       
        /// <summary>
        /// 包含给定时间的工作时间段.
        /// </summary>
        /// <param name="datetime">HH:mm</param>
        /// <returns></returns>
        public virtual ServiceOpenTimeForDay GetItem(string  datetime)
        {
            Time time = new Time(datetime);
            var timeItems = OpenTimeForDay.Where(x => x.IsIn(time));
            int c = timeItems.Count();
            string errMsg = "给定时间没有对应的时间项,传入时间为:"+datetime;
            System.Diagnostics.Debug.Assert(c == 1, errMsg);
            if (c !=1)
            {
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            return timeItems.ToList()[0];

        }

        /// <summary>
        /// 获取指定起止时间的工作时间段
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public virtual ServiceOpenTimeForDay GetItem(TimePeriod period)
        {
          var list=  OpenTimeForDay.Where(x => x.TimePeriod == period);

               if (list.Count() == 1)
            {
                return list.ElementAt(0);
            }
           else  
            {
                throw new Exception("给定时间段的工作时间不等于1");
            }
               
           
        }
        
    }
   


}
