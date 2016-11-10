using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天信息列表
    /// </summary>
    public interface IViewChatList
    {
        
        IList<VMChat> ChatList { set;  get; }
        void AddOneChat(VMChat vmChat);
        void InsertOneChat(VMChat vmChat);
        /// <summary>
        /// 当前助理. 
        /// 用来确定消息的显示格式.
        /// </summary>
        Guid CurrentCustomerServiceId { get; set; }
        event BtnMoreChat BtnMoreChat;

        void ShowMoreLabel(string targetChatId);
        void ShowNoMoreLabel();

        void ShowLoadingMsg();
        void ShowNoChatMsg();

        string ChatListCustomerName { get; set; }
        void ClearUCData();

        void ShowChatImageNormalMask(Guid chatId);
        void RemoveChatImageNormalMask(Guid chatId);
    }
    public delegate void AudioPlay(object audioTag, IntPtr handler);

    public delegate void BtnMoreChat(string targetChatId);

    public delegate void TimerTick();
}
