using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using NHibernate;
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

        public IList<ReceptionChatDto> GetListByCustomerId(string customerId)
        {
            using (var t = session.BeginTransaction())
            {
                try
                {
                    

                    IList<ReceptionChat> list = repositoryChat.GetListByCustomerId(customerId);

                    IList<ReceptionChatDto> dtoList = new List<ReceptionChatDto>();
                    foreach (var item in list)
                    {
                        dtoList.Add(item.ToDto());
                    }
                    t.Commit();
                    return dtoList;
                }
                catch (Exception ex)
                {
                    t.Rollback();
                    throw ex;
                  
                }
                finally
                {
                    t.Dispose();  
                }
            }
        }

        /// <summary>
        /// 接收到消息之后进行的处理
        /// </summary>
        /// <param name="chat"></param>
        void IChatService.ReceiveMessage(ReceptionChat chat)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 接收到即使信息
        /// </summary>
        /// <param name="textChat"></param>
        

        bool IChatService.SendMessage(ReceptionChat chat)
        {
            throw new NotImplementedException();
        }

        
    }
}
