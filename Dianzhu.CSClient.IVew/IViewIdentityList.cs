using Dianzhu.CSClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 接待列表
    /// </summary>
    public interface IViewIdentityList
    {
        event IdentityClick IdentityClick;
        //增加一个标志
        void AddIdentity(VMIdentity vmIdentity);
        //删除一个用户
        void RemoveIdentity(string customerId);
        void UpdateIdentityBtnName(string customerId, VMIdentity vmIdentity);
        //设置为未读
        void SetIdentityUnread(string customerId, int messageAmount);
        //设置为已读
        void SetIdentityReaded(string customerId);

        void IdleTimerStart(string cusomterId);
        void IdleTimerStop(string customerId);

        event FinalChatTimerTick FinalChatTimerTick;

        /// <summary>
        /// 播放提示音
        /// </summary>
        void PlayVoice();
    }
    /// <summary>
    /// 点击用户按钮的委托.
    /// </summary>
    /// <param name="customer"></param>
    public delegate void IdentityClick(VMIdentity vmIdentity);
    /// <summary>
    /// 客服发消息后在指定时间内触发的事件
    /// </summary>
    public delegate void FinalChatTimerTick(string customerId);
}
