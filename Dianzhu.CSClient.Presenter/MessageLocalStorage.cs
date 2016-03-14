using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.Presenter
{

    public interface IMessageStorage {

        void SaveToLocal(ReceptionChat chat);
        ReceptionChat GetFromLocal(Guid chatId);
    }
    /// <summary>
    /// 将消息存储到客户端本地
    /// </summary>
    public class MessageLocalStorage:IMessageStorage
    {

        public void SaveToLocal(ReceptionChat chat)
        {

        }
        public ReceptionChat GetFromLocal(Guid receptionChatId)
        {
            throw new Exception();
        }
    }
}
