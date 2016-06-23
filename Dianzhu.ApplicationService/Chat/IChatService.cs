using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Chat
{
    public interface IChatService
    {
        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        IList<chatObj> GetChats(string orderID, common_Trait_Filtering filter, common_Trait_ChatFiltering chatfilter);

        /// <summary>
        /// 统计聊天信息的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        countObj GetChatsCount(string orderID, common_Trait_ChatFiltering chatfilter);
    }
}
