using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jd = JdSoft.Apple.Apns.Notifications;
namespace Ydb.Push
{
    /// <summary>
    /// 客户端推送消息
    /// </summary>
    public interface IPushApi
    {
       
        /// <summary>
        /// 推送接口
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="target">目标地址.</param>
        /// <returns>推送结果 </returns>
        string Push(PushTargetClient pushType, PushMessage message, string target,int amount);
    }
   
    
    
}
