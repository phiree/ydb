using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using Ydb.Common.Repository;
using NHibernate;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
 
using Ydb.Common.Specification;
using Ydb.InstantMessage.Infrastructure;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
    public class ChatService:IChatService
    {
        IRepositoryChat repositoryChat;
      
        public ChatService(IRepositoryChat repositoryChat)
        {
         this.   repositoryChat = repositoryChat;
        }

        [ UnitOfWork]
        public void Add(ReceptionChat chat)
        {
            repositoryChat.Add(chat);
        }

 
        [ UnitOfWork]
 
        public IList<ReceptionChatDto> GetChatByOrder(string orderId)
        {
            var list = repositoryChat.GetChatByOrder(orderId);

            IList<ReceptionChatDto> dtoList = new List<ReceptionChatDto>();
            foreach (var item in list)
            {
                dtoList.Add(item.ToDto());
            }

            return dtoList;
        }
        
        private IList<ReceptionChatDto> ToDto(IList<ReceptionChat> list)
        {
            IList<ReceptionChatDto> dtoList = new List<ReceptionChatDto>();

            foreach (var item in list)
            {
                switch (item.GetType().Name)
                {
                    case "ReceptionChat":
                        dtoList.Add(item.ToDto());
                        break;
                    case "ReceptionChatMedia":
                        dtoList.Add(((ReceptionChatMedia)item).ToDto());
                        break;
                    case "ReceptionChatPushService":
                        dtoList.Add(((ReceptionChatPushService)item).ToDto());
                        break;
                    default:
                        throw new Exception("未知chat类型");
                }
            }

            return dtoList;
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatListByCustomerId(Guid customerId, int pageSize)
        {
            int rouCount;
            var list = repositoryChat.GetReceptionChatList(customerId, Guid.Empty, Guid.Empty, DateTime.Now.AddYears(-1), DateTime.Now, 0, pageSize, ChatTarget.cer, out rouCount);
            
            return ToDto(list);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatListByTargetId(Guid customerId, int pageSize, Guid targetChatId, string low)
        {
            ReceptionChat targetChat = repositoryChat.FindById(targetChatId);
            var list = repositoryChat.GetReceptionChatListByTargetId(customerId, Guid.Empty, Guid.Empty, DateTime.Now.AddYears(-1), DateTime.Now, pageSize, targetChat.SavedTimestamp, low, ChatTarget.cer);

            return ToDto(list);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd, int pageIndex, int pageSize, string target, out int rowCount)
        {
            ChatTarget chatTarget;
            if(!Enum.TryParse(target, out chatTarget))
            {
                throw new Exception("target有误");
            }
            var list = repositoryChat.GetReceptionChatList(fromId, toId, orderId, timeBegin, timeEnd, pageIndex, pageSize, chatTarget, out rowCount);

            return ToDto(list);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetChats(TraitFilter filter, string type, string fromTarget, Guid orderID, Guid userID, string userType)
        {
            var list = repositoryChat.GetChats(filter, type, fromTarget, orderID, userID, userType);

            return ToDto(list);
        }

        [UnitOfWork]
        public long GetChatsCount(string type, string fromTarget, Guid orderID, Guid userID, string userType)
        {
            return repositoryChat.GetChatsCount(type, fromTarget, orderID, userID, userType);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetUnreadChatsAndSetReaded(Guid userID)
        {
            return ToDto(repositoryChat.GetUnreadChatsAndSetReaded(userID));
        }

        [UnitOfWork]
        public long GetUnreadChatsCount(Guid userID)
        {
            return repositoryChat.GetUnreadChatsCount(userID);
        }

        [UnitOfWork]
        public void Update(ReceptionChat chat)
        {
            repositoryChat.Update(chat);
        }
    }
}
