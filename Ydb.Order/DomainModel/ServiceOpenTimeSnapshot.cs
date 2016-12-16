using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.Common.Domain;
namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 下单时 服务的工作时间快照 包括:当天最大接单量,当时所属时间段的 最大接单量, 开始/结束时间/
    /// </summary>
 
    public class WorkTimeSnapshot
 
    {
        
        /// <summary>
        /// 该日最大接大量
        /// </summary>
        public virtual int MaxOrderForWorkDay { get; set; }
        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>

       
        public int MaxOrderForWorkTime { get;   set; }
        
        public TimePeriod TimePeriod { get;   set; }
        
        public bool Enabled { get; set; }
       

    }
   


}
