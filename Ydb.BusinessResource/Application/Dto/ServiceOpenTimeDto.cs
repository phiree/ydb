using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ydb.BusinessResource.Application
{
    /// <summary>
    /// 服务每天参数设定
    /// </summary>
 
    public class ServiceOpenTimeDto
 
    {
        
        /// <summary>
        /// 该日最大接大量
        /// </summary>
        public   int MaxOrderForDay { get; set; }

        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>
        public virtual DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool Enabled { get; set; }
        public virtual IList<ServiceOpenTimeForDayDto> OpenTimeForDay { get; set; }
        ///// <summary>
        ///// 1:星期一,2:星期二.........
        ///// </summary>
        //    /// <summary>
        //    /// 最大接单量
        //    /// </summary>
        //    public DateTime SnapshotDate { get;   set; }
        //    public int MaxOrderForPeriod { get;   set; }
        //    public DateTime Date { get;   set; }
        //    public int PeriodBegin { get;   set; }
        //    public int PeriodEnd { get;   set; }





    }
   


}
