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
using Ydb.Common.Domain;
using Ydb.InstantMessage.DomainModel.DataStatistics;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
    public class ChatService:IChatService
    {
        IRepositoryChat repositoryChat;
        IStatisticsInstantMessage statisticsInstantMessage;
        public ChatService(IRepositoryChat repositoryChat, IStatisticsInstantMessage statisticsInstantMessage)
        {
            this.repositoryChat = repositoryChat;
            this.statisticsInstantMessage = statisticsInstantMessage;
        }

        [ UnitOfWork]
        public void Add(ReceptionChat chat)
        {
            repositoryChat.Add(chat);
        }

        [UnitOfWork]
        public ReceptionChatDto GetChatById(string Id)
        {
            var item = repositoryChat.FindById(new Guid(Id));
            ReceptionChatDto itemDto = null;
            switch (item.GetType().Name)
            {
                case "ReceptionChat":
                    itemDto = item.ToDto();
                    break;
                case "ReceptionChatMedia":
                    itemDto = ((ReceptionChatMedia)item).ToDto();
                    break;
                case "ReceptionChatPushService":
                    itemDto = ((ReceptionChatPushService)item).ToDto();
                    break;
                case "ReceptionChatDidichuxing":
                    itemDto = ((ReceptionChatDidichuxing)item).ToDto();
                    break;
                default:
                    throw new Exception("未知chat类型");
            }
            return itemDto;
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
                    case "ReceptionChatDidichuxing":
                        dtoList.Add(((ReceptionChatDidichuxing)item).ToDto());
                        break;
                    default:
                        //throw new Exception("未知chat类型");
                        
                        break;
                }
            }

            return dtoList;
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatListByCustomerId(string customerId, int pageSize)
        {
            int rouCount;
            var list = repositoryChat.GetReceptionChatList(customerId, string.Empty, string.Empty, DateTime.Now.AddYears(-1), DateTime.Now, 0, pageSize, ChatTarget.cer, out rouCount);
            
            return ToDto(list);
        }
        /// <summary>
        /// 初始化聊天消息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<ReceptionChatDto> GetInitChatList(string customerId, int pageSize)
        {
            var list = repositoryChat.GetInitChatList(customerId, pageSize).OrderBy(x=>x.SavedTimestamp).ToList();
            return ToDto(list);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatListByTargetId(string customerId, int pageSize, string targetChatId, string low)
        {
            ReceptionChat targetChat = repositoryChat.FindById(new Guid( targetChatId));
            var list = repositoryChat.GetReceptionChatListByTargetId(customerId, string.Empty, string.Empty, DateTime.Now.AddYears(-1), DateTime.Now, pageSize, targetChat.SavedTimestamp, low, ChatTarget.cer);

            return ToDto(list);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetReceptionChatList(string fromId, string toId, string orderId, DateTime timeBegin, DateTime timeEnd, int pageIndex, int pageSize, string target, out int rowCount)
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
        public IList<ReceptionChatDto> GetChats(TraitFilter filter, string type, string fromTarget, string orderID, string userID, string userType)
        {
            var list = repositoryChat.GetChats(filter, type, fromTarget, orderID, userID, userType);

            return ToDto(list);
        }


        [UnitOfWork]
        public IList<StatisticsInfo<ReceptionChatDto>> GetChatTimeLine(string orderID)
        {
            var list = repositoryChat.GetChatByOrder(orderID);
            return statisticsInstantMessage.GetChatTimeLine(ToDto(list));
        }

        [UnitOfWork]
        public long GetChatsCount(string type, string fromTarget, string orderID, string userID, string userType)
        {
            return repositoryChat.GetChatsCount(type, fromTarget, orderID, userID, userType);
        }

        [UnitOfWork]
        public IList<ReceptionChatDto> GetUnreadChatsAndSetReaded(string userID)
        {
            return ToDto(repositoryChat.GetUnreadChatsAndSetReaded(userID));
        }

        [UnitOfWork]
        public long GetUnreadChatsCount(string userID)
        {
            return repositoryChat.GetUnreadChatsCount(userID);
        }

        [UnitOfWork]
        public void Update(ReceptionChat chat)
        {
            repositoryChat.Update(chat);
        }
        [UnitOfWork]
        public void SetChatUnread(string chatId)
        {
            ReceptionChat chat = repositoryChat.FindById(new Guid(chatId));
            chat.SetUnread();
        }
    }
}
