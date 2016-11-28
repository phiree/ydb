using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Specification;
using Ydb.InstantMessage.DomainModel.Chat;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
   public  interface IChatService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="chat"></param>
        void Add(ReceptionChat chat);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="chat"></param>
        void Update(ReceptionChat chat);

        /// <summary>
        /// 根据id获取聊天记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ReceptionChatDto GetChatById(string Id);

        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetChatByOrder(string orderId);

        IList<ReceptionChatDto> GetReceptionChatList(string fromId, string toId, string orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, string target, out int rowCount);


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
        IList<ReceptionChatDto> GetReceptionChatListByCustomerId(string customerId, int pageSize);

        /// <summary>
        /// 根据用户的聊天记录的时间戳向上或向下获取数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="targetChatSavedTimestamp"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetReceptionChatListByTargetId(string customerId, int pageSize, string targetChatId, string low);

        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetChats(TraitFilter filter, string type, string fromTarget, string orderID, string userID, string userType);

        /// <summary>
        /// 统计聊天信息的数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        long GetChatsCount(string type, string fromTarget, string orderID, string userID, string userType);

        /// <summary>
        /// 条件读取未读聊天记录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IList<ReceptionChatDto> GetUnreadChatsAndSetReaded(string userID);

        /// <summary>
        /// 统计未读聊天信息的数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        long GetUnreadChatsCount(string userID);
    }
}
