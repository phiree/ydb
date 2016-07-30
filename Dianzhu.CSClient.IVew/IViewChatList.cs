using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天信息列表
    /// </summary>
    public interface IViewChatList
    {
        
        IList<ReceptionChat> ChatList { set;  get; }
        void AddOneChat(ReceptionChat chat);
        void InsertOneChat(ReceptionChat chat);
        /// <summary>
        /// 当前助理. 
        /// 用来确定消息的显示格式.
        /// </summary>
        DZMembership CurrentCustomerService { get; set; }
        event BtnMoreChat BtnMoreChat;

        void ShowMoreLabel();
        void ShowNoMoreLabel();

        void ShowLoadingMsg();
        void HideLoadingMsg();

        string ChatListCustomerName { get; set; }
        void ClearUCData();        
    }
    public delegate void AudioPlay(object audioTag, IntPtr handler);

    public delegate void BtnMoreChat();

    public delegate void TimerTick();
}
