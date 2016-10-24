using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using NHibernate;
using Ydb.InstantMessage.DomainModel.Chat.Enums;

namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
    public class ChatService:IChatService
    {
        IRepositoryChat repositoryChat;
        ISession session;
        public ChatService(IRepositoryChat repositoryChat,ISession session)
        {
            this.repositoryChat = repositoryChat;
            this.session = session;
        }

        public IList<ReceptionChatDto> GetChatByOrder(string orderId)
        {
            using(var t = session.BeginTransaction())
            {
                try
                {
                    var list = repositoryChat.GetChatByOrder(orderId);

                    IList<ReceptionChatDto> dtoList = new List<ReceptionChatDto>();
                    foreach( var item in list)
                    {
                        dtoList.Add(item.ToDto());
                    }

                    t.Commit();

                    return dtoList;
                }
                catch (Exception ee)
                {
                    t.Rollback();
                    throw ee;
                }
                finally
                {
                    t.Dispose();
                }
            }
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

        public IList<ReceptionChatDto> GetReceptionChatListByCustomerId(Guid customerId, int pageSize)
        {
            using (var t = session.BeginTransaction())
            {
                try
                {
                    int rouCount;
                    var list = repositoryChat.GetReceptionChatList(customerId, Guid.Empty, Guid.Empty, DateTime.Now.AddYears(-1), DateTime.Now, 0, pageSize, ChatTarget.cer, out rouCount);
                    
                    t.Commit();

                    return ToDto(list);
                }
                catch (Exception ee)
                {
                    t.Rollback();
                    throw ee;
                }
                finally
                {
                    t.Dispose();
                }
            }
        }

        public IList<ReceptionChatDto> GetReceptionChatListByTargetId(Guid customerId, int pageSize, Guid targetChatId, string low)
        {
            using (var t = session.BeginTransaction())
            {
                try
                {
                    ReceptionChat targetChat = repositoryChat.FindById(targetChatId);
                    var list = repositoryChat.GetReceptionChatListByTargetId(customerId, Guid.Empty, Guid.Empty, DateTime.Now.AddYears(-1), DateTime.Now, pageSize, targetChat.SavedTimestamp, low, ChatTarget.cer);
                    
                    t.Commit();

                    return ToDto(list);
                }
                catch (Exception ee)
                {
                    t.Rollback();
                    throw ee;
                }
                finally
                {
                    t.Dispose();
                }
            }
        }
    }
}
