using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
   public  class ChatService:IChatService
    {
        IRepositoryChat repositoryChat;
        public ChatService(IRepositoryChat repositoryChat)
        {
            this.repositoryChat = repositoryChat;
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
