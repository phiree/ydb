using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 接待列表
    /// </summary>
    public interface IViewIdentityList
    {
        event IdentityClick IdentityClick;
        //增加一个标志
        void AddIdentity(ServiceOrder  serviceOrder);
        //删除一个用户
        void RemoveIdentity(ServiceOrder serviceOrder);
        void UpdateIdentityBtnName(ServiceOrder oldOrder, ServiceOrder newOrder);
        //设置为未读
        void SetIdentityUnread(ServiceOrder serviceOrder, int messageAmount);
        //设置为已读
        void SetIdentityReaded(ServiceOrder serviceOrder);
        //
        void SetIdentityLoading(ServiceOrder serviceOrder);
        //用户离线
        void SetIdentityLogOff(ServiceOrder serviceOrder);
        void IdentityLogOffShowMsg(ServiceOrder serviceOrder);
        void IdentityLogOnShowMsg(ServiceOrder serviceOrder,string msg);
        void IdentityLogOnShowMsgAndTimer(ServiceOrder serviceOrder, string msg);

        void IdleTimerStart(Guid orderId);
        void IdleTimerStop(Guid orderId);
        event FinalChatTimerTick FinalChatTimerTick;
    }
    /// <summary>
    /// 点击用户按钮的委托.
    /// </summary>
    /// <param name="customer"></param>
    public delegate void IdentityClick(ServiceOrder serviceOrder);
    /// <summary>
    /// 客服发消息后在指定时间内触发的事件
    /// </summary>
    public delegate void FinalChatTimerTick(Guid orderId);
}
