using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Dianzhu.Model
{
    /// <summary>
    /// 服务每天参数设定
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
       

    }
   


}
