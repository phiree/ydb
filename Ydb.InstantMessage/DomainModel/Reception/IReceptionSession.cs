using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 用户会话
    /// </summary>
    public interface IReceptionSession
    {
        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        /// <returns></returns>
        IList<OnlineUserSession> GetOnlineSessionUser(string xmppResource);
        /// <summary>
        /// 用户是否在线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsUserOnline(string userId);
    }
}
