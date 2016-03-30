using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
namespace Dianzhu.Push.JPush
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>

    public class JPushResponse  
    {
        public string msg_id { get; set; }
        public string sendno { get; set; }
    }



}
