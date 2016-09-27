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
        void RemoveIdentity(Guid serviceOrderId);
        void UpdateIdentityBtnName(Guid oldOrderId, VMIdentity vmIdentity);
        //设置为未读
        void SetIdentityUnread(string  orderId, int messageAmount);
        //设置为已读
        void SetIdentityReaded(Guid serviceOrderId);

        void IdleTimerStart(Guid orderId);
        void IdleTimerStop(Guid orderId);

        event FinalChatTimerTick FinalChatTimerTick;

        //当前订单临时变量
        Guid IdentityOrderTempId { get; set; }

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
    public delegate void FinalChatTimerTick(Guid orderId);
}
