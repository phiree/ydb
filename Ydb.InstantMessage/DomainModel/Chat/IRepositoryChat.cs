using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    public interface IRepositoryChat: IRepository<ReceptionChat,Guid>
    {
        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        IList<ReceptionChat> GetChatByOrder(string orderId);
        
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
        IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
                    int pageIndex, int pageSize, ChatTarget? target, out int rowCount);

        /// <summary>
        /// 根据chatId获取聊天记录列表
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="orderId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="pageSize"></param>
        /// <param name="targetChatSavedTimestamp"></param>
        /// <param name="low"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IList<ReceptionChat> GetReceptionChatListByTargetId(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
         int pageSize, double targetChatSavedTimestamp, string low, ChatTarget? target);
    }
}
