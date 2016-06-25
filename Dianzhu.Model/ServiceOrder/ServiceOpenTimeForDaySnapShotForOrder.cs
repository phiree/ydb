using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务时间项的快照
    /// </summary>
   public  class ServiceOpenTimeForDaySnapShotForOrder:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 最大接单量
        /// </summary>
        public int MaxOrder { get; protected set; }
        public DateTime Date { get; protected set; }
        public int PeriodBegin { get; protected set; }
        public int PeriodEnd { get; protected set; }
        public ServiceOpenTimeForDaySnapShotForOrder() { }
        public ServiceOpenTimeForDaySnapShotForOrder(int maxOrder,DateTime date,int periodBegin,int periodEnd)
        {
            MaxOrder = maxOrder;
            Date = date;
            PeriodBegin = periodBegin;
            PeriodEnd = periodEnd;
        }
        
    }
}
