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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model.ServiceOpenTimeSnapshot");
        public ServiceOpenTimeSnapshot()
        {
            
            
        }
        
        /// <summary>
        /// 该日最大接大量
        /// </summary>
        public virtual int MaxOrderForDay { get; set; }
        /// <summary>
        /// 1:星期一,2:星期二.........
        /// </summary>
 
        
        
    }
   


}
