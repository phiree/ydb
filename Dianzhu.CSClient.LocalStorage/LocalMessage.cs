using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;

namespace Dianzhu.CSClient.LocalStorage
{
    /// <summary>
    /// 本地化存储
    /// </summary>
    public interface LocalChatManager
    {
         Dictionary<string, IList<ReceptionChat>> LocalChats { get;  }   
        void Add(string key, ReceptionChat chatData);

        IList<ReceptionChat> InitChatList(Guid customerId, Guid customerServiceId, Guid serviceOrderId);
    }

    public class ChatManagerInMemory : LocalChatManager
    {
        IDALReceptionChat idalReceptionChat;

        public ChatManagerInMemory(IDALReceptionChat idalReceptionChat)
        {
            LocalChats = new Dictionary<string, IList<ReceptionChat>>();
            this.idalReceptionChat = idalReceptionChat;
        }
        public Dictionary<string, IList<ReceptionChat>> LocalChats
        {
            get;
             
        }
        /// <summary>
        /// 增加一条新记录.如果不存在则获取初始化数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="chatData"></param>
        public void Add(string key, ReceptionChat chatData)
        {
            if (LocalChats.ContainsKey(key))
            {
                LocalChats[key].Add(chatData);
            }
            else
            {
                
                //initdata 
                IList<ReceptionChat> initChatList = InitChatList(chatData.From.Id, chatData.To.Id, chatData.ServiceOrder.Id);
                //是否包含当前记录.  插件保存记录 和 xmpp接收 的顺序无法控制, 加此判断以保证列表中包含该消息.
                if (initChatList.Where(x => x.Id == chatData.Id).Count() == 0)
                {
                    LocalChats[key].Add(chatData);
                }

               
            }
            
        }
         
        /// <summary>
        /// 获取一个聊天列表,如果不存在则初始化.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerServiceId"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        public IList<ReceptionChat> InitChatList(Guid customerId, Guid customerServiceId, Guid serviceOrderId)
        {
            string key = customerId.ToString();
            if (LocalChats.ContainsKey(key))
            {
                return LocalChats[key];
            }
            else
            {
                int rowCount;
                //initdata 
                IList<ReceptionChat> initChatList = idalReceptionChat.GetReceptionChatList(customerId, customerServiceId, serviceOrderId, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1),
                      0, 10, enum_ChatTarget.cer, out rowCount
                     );
               LocalChats.Add(key, initChatList);

            }
            return LocalChats[key];
        }
    }

}
