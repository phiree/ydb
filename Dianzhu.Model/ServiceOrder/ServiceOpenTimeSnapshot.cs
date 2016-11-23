using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Dianzhu.Model
{
    /// <summary>
    /// 下单时 服务的工作时间快照 包括:当天最大接单量,当时所属时间段的 最大接单量, 开始/结束时间/
    /// </summary>
 
    public class ServiceOpenTimeSnapshot
 
    {
        
        /// <summary>
        /// 该日最大接大量
        /// </summary>
        public virtual int MaxOrderForDay { get; set; }
        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>

        public DateTime SnapshotDate { get;   set; }
        public int MaxOrderForPeriod { get;   set; }
        public DateTime Date { get;   set; }
        public int PeriodBegin { get;   set; }
        public int PeriodEnd { get;   set; }
        public bool Enabled { get; set; }
       

    }
   


}
