using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.LocalStorage
{
    /// <summary>
    /// 聊天消息本地化存储
    /// </summary>
    public interface LocalChatManager
    {
        /// <summary>
        /// 用户此次登录时的头像，保证在此次接待中的头像保持一致
        /// </summary>
        Dictionary<string,string> LocalCustomerAvatarUrls { get; }

        Dictionary<string, IList<VMChat>> LocalChats { get; }
        void Add(string customerId, VMChat chatData);
        void InsertTop(string customerId, VMChat chatData);
        void Remove(string customerId);

        //IList<ReceptionChat> InitChatList(Guid customerId, Guid customerServiceId, Guid serviceOrderId);
    }    

    /// <summary>
    /// 聊天消息内存中实现
    /// </summary>
    public class ChatManagerInMemory : LocalChatManager
    {
        public ChatManagerInMemory()
        {
            LocalChats = new Dictionary<string, IList<VMChat>>();
            LocalCustomerAvatarUrls = new Dictionary<string, string>();
        }
        public Dictionary<string, IList<VMChat>> LocalChats
        {
            get;
             
        }
        /// <summary>
        /// 用户此次登录时的头像，保证在此次接待中的头像保持一致
        /// </summary>
        public Dictionary<string, string> LocalCustomerAvatarUrls
        {
            get;
        }

        /// <summary>
        /// 增加一条新记录.如果不存在则获取初始化数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="chatData"></param>
        public void Add(string customerId, VMChat chatData)
        {
            if (LocalChats.ContainsKey(customerId))
            {
                LocalChats[customerId].Add(chatData);
            }
            else
            {
                LocalChats[customerId] = new List<VMChat>();
                LocalChats[customerId].Add(chatData);

                //initdata 
                //IList<ReceptionChat> initChatList = InitChatList(new Guid( chatData.FromId), Guid.Empty, Guid.Empty);
                //是否包含当前记录.  插件保存记录 和 xmpp接收 的顺序无法控制, 加此判断以保证列表中包含该消息.

                //if (initChatList.Where(x => x.Id == chatData.Id).Count() == 0)
                //{
                //    LocalChats[customerId].Add(chatData);
                //}
            }

            if (!LocalCustomerAvatarUrls.ContainsKey(customerId))
            {
                LocalCustomerAvatarUrls.Add(customerId, string.Empty);
            }
        }


        public void InsertTop(string customerId, VMChat chatData)
        {
            if (LocalChats.ContainsKey(customerId))
            {
                LocalChats[customerId].Insert(0, chatData);
            }
        }

        /// <summary>
        /// 清除用户相应的聊天数据
        /// </summary>
        /// <param name="customerId"></param>
        public void Remove(string customerId)
        {
            if (LocalChats.ContainsKey(customerId))
            {
                LocalChats.Remove(customerId);
            }
            if (LocalCustomerAvatarUrls.ContainsKey(customerId))
            {
                LocalCustomerAvatarUrls.Remove(customerId);
            }
        }

        /// <summary>
        /// 获取一个聊天列表,如果不存在则初始化.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerServiceId"></param>
        /// <param name="serviceOrderId"></param>
        /// <returns></returns>
        //public IList<ReceptionChat> InitChatList(Guid customerId, Guid customerServiceId, Guid serviceOrderId)
        //{
        //    string key = customerId.ToString();
        //    if (LocalChats.ContainsKey(key))
        //    {
        //        return LocalChats[key];
        //    }
        //    else
        //    {
        //        int rowCount;
        //        //initdata 
        //        IList<ReceptionChat> initChatList = idalReceptionChat.GetReceptionChatList(customerId, customerServiceId, serviceOrderId, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 0, 10, enum_ChatTarget.cer, out rowCount);
        //        LocalChats.Add(key, initChatList);

        //        if (!LocalCustomerAvatarUrls.ContainsKey(key))
        //        {
        //            LocalCustomerAvatarUrls.Add(key, customer.AvatarUrl);
        //        }
        //    }
        //    return LocalChats[key];
        //}
    }
}
