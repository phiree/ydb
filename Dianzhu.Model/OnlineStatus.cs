using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Dianzhu.Model
{
    /// <summary>
    /// 在线概况 当前在线接待情况
    /// </summary>
    public class OnlineStatus
    {
        public OnlineStatus()
        {
            
        }
        public virtual DZMembership CustomerService { get; set; }
        public virtual DZMembership Customer { get; set; }
     
    }

}
