﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;

namespace Dianzhu.CSClient.LocalStorage
{
    /// <summary>
    /// 聊天消息本地化存储
    /// </summary>
    public interface LocalChatManager
    {
        Dictionary<string,string> LocalCustomerAvatarUrls { get; }

        Dictionary<string, IList<ReceptionChat>> LocalChats { get; }
        void Add(string customerId, ReceptionChat chatData);
        void Remove(string customerId);

        IList<ReceptionChat> InitChatList(Guid customerId, Guid customerServiceId, Guid serviceOrderId);
    }    

    /// <summary>
    /// 聊天消息内存中实现
    /// </summary>
    public class ChatManagerInMemory : LocalChatManager
    {
        IDALReceptionChat idalReceptionChat;
        IDALMembership dalMembership;
        public ChatManagerInMemory(IDALReceptionChat idalReceptionChat,IDAL.IDALMembership dalMembership)
        {
            LocalChats = new Dictionary<string, IList<ReceptionChat>>();
            LocalCustomerAvatarUrls = new Dictionary<string, string>();
            this.idalReceptionChat = idalReceptionChat;
            this.dalMembership = dalMembership;
        }
        public Dictionary<string, IList<ReceptionChat>> LocalChats
        {
            get;
             
        }

        public Dictionary<string, string> LocalCustomerAvatarUrls
        {
            get;
        }

        /// <summary>
        /// 增加一条新记录.如果不存在则获取初始化数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="chatData"></param>
        public void Add(string customerId, ReceptionChat chatData)
        {
            if (LocalChats.ContainsKey(customerId))
            {
                LocalChats[customerId].Add(chatData);
            }
            else
            {
                
                //initdata 
                IList<ReceptionChat> initChatList = InitChatList(new Guid( chatData.FromId), Guid.Empty, Guid.Empty);
                //是否包含当前记录.  插件保存记录 和 xmpp接收 的顺序无法控制, 加此判断以保证列表中包含该消息.
                if (initChatList.Where(x => x.Id == chatData.Id).Count() == 0)
                {
                    LocalChats[customerId].Add(chatData);
                }
            }

            if (!LocalCustomerAvatarUrls.ContainsKey(customerId))
            {
                DZMembership from = dalMembership.FindById(new Guid(chatData.FromId));
                LocalCustomerAvatarUrls.Add(customerId, from.AvatarUrl);
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
