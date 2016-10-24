using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
   public  interface IChatService
    {
        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetChatByOrder(string orderId);


        /// <summary>
        /// 获取聊天记录列表
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="orderId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="target"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetReceptionChatListByCustomerId(Guid customerId, int pageSize);

        /// <summary>
        /// 根据用户的聊天记录的时间戳向上或向下获取数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="targetChatSavedTimestamp"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetReceptionChatListByTargetId(Guid customerId, int pageSize, Guid targetChatId, string low);
    }
}
