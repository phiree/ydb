using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;
namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 服务时间项的快照
    /// </summary>
   public  class ServiceOpenTimeForDaySnapShotForOrder: Entity<Guid>
    {
        /// <summary>
        /// 最大接单量
        /// </summary>
        public DateTime SnapshotDate { get; protected set; }
        public int MaxOrder { get; protected set; }
        public DateTime Date { get; protected set; }
        public TimePeriod TimePeriod { get; protected set; }
      
        public ServiceOpenTimeForDaySnapShotForOrder() { }
        public ServiceOpenTimeForDaySnapShotForOrder(Guid id, int maxOrder,DateTime date, TimePeriod timePeriod)
        {
            MaxOrder = maxOrder;
            Date = date;
            TimePeriod = timePeriod;
            this.Id = id;
        }
        
    }
}
