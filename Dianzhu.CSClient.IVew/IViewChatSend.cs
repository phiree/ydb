﻿using System;
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

        string MessageText { get; set; }

        string MessageTimer { get; set; }
    }
    public delegate void SendTextClick();
    public delegate void SendMediaClick(byte[] fileData, string domainType, string mediaType);
    public delegate void FinalChatTimerSend();
   // public delegate void SendImageClick(byte[] fileData, string domainType, string mediaType);



}
