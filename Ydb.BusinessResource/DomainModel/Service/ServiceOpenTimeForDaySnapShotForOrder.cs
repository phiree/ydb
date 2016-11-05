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
        public int PeriodBegin { get; protected set; }
        public int PeriodEnd { get; protected set; }
        public ServiceOpenTimeForDaySnapShotForOrder() { }
        public ServiceOpenTimeForDaySnapShotForOrder(int maxOrder,DateTime date,int periodBegin,int periodEnd,Guid id)
        {
            MaxOrder = maxOrder;
            Date = date;
            PeriodBegin = periodBegin;
            PeriodEnd = periodEnd;
            Id = id;
        }
        
    }
}
