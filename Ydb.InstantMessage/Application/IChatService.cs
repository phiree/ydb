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
        /// 接收到即使信息
        /// </summary>
        /// <param name="textChat"></param>
        void ReceiveMessage(ReceptionChat chat);
        bool SendMessage(ReceptionChat chat);
    }
}
