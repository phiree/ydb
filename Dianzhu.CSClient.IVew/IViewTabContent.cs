using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    public interface IViewTabContent
    {
        IViewSearch ViewSearch { get; }
        IViewSearchResult ViewSearchResult { get; }
        IViewChatList ViewChatList { get; }
        IViewChatSend ViewChatSend { get; }
        IViewOrderHistory ViewOrderHistory { get; }
        IViewToolsControl ViewToolsControl { get; }

        string Identity { get; set; }

        event IdleTimerOut IdleTimerOut;
        /// <summary>
        /// 客服回复后开始计时
        /// </summary>
        void StartFinalChatTimer();
        /// <summary>
        /// 用户回复后停止计时
        /// </summary>
        void StopFinalChatTimer();
    }

    public delegate void IdleTimerOut(string customerId);
}
