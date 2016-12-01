using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天消息发送
    /// </summary>
    public interface IViewChatSend
    {
        event SendTextClick SendTextClick;
        event SendMediaClick SendMediaClick;
        event FinalChatTimerSend FinalChatTimerSend;
        event SendDidichuxing SendDidichuxing;

        string MessageText { get; set; }
    }
    public delegate void SendDidichuxing();
    public delegate void SendTextClick();
    public delegate void SendMediaClick(byte[] fileData, string domainType, string mediaType);
    public delegate void FinalChatTimerSend();



}
