using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.InstantMessage.DomainModel.Chat;
 
namespace   Ydb.InstantMessage.Application
{
    public delegate void IMReceivedMessage(ReceptionChatDto dto);
    public delegate void IMLogined(string jidUser);
    public delegate void IMAuthError();
    /// <summary>
    /// 状态监视事件
    /// </summary>
    /// <param name="userFrom"></param>
    /// <param name="presentType"> 
    /// available = -1,	 
   ///  subscribe, subscribed, unsubscribe, unsubscribed,
    /// 	 unavailable, invisible, error, probe
    /// </param>
    public delegate void IMPresent(string userFrom, int presentType);
    public delegate void IMError(string error);
    public delegate void IMConnectionError(string error);
    public delegate void IMClosed();
    public delegate void IMIQ();
    public delegate void IMStreamError();
    /// <summary>
    /// 通讯接口定义
    /// </summary>
    public interface IInstantMessage
    {

        string Server { get; }
        string Domain { get; }
        void OpenConnection(string userName, string password);
        void OpenConnection(string userName, string password, string resource);
        event IMClosed IMClosed;
        event IMLogined IMLogined;
        event IMPresent IMPresent;
        event IMAuthError IMAuthError;
        event IMError IMError;
        void Close();
        event IMConnectionError IMConnectionError;
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageBody"></param>
        /// <param name="to"></param>
        /// <param name="toResource"></param>
        /// <param name="sessionId"></param>
        void SendMessageText(Guid messageId, string messageBody, string to,string toResource, string sessionId);
        /// <summary>
        /// 发送多媒体消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="mediaUrl"></param>
        /// <param name="mediaType"></param>
        /// <param name="to"></param>
        /// <param name="sessionId"></param>
        /// <param name="toResource"></param>
        void SendMessageMedia(Guid messageId, string mediaUrl, string mediaType,string to,string sessionId,string toResource);
        /// <summary>
        /// 发送订单推送消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="serviceInfos"></param>
        /// <param name="messageBody"></param>
        /// <param name="to"></param>
        /// <param name="toResource"></param>
        /// <param name="sessionId"></param>
        void SendMessagePushService(Guid messageId, IList<PushedServiceInfo> serviceInfos, string messageBody, string to, string toResource, string sessionId);
        /// <summary>
        /// 客服上线通知点点
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageBody"></param>
        /// <param name="to"></param>
        /// <param name="toResource"></param>
        /// <param name="sessionId"></param>
        void SendCSLoginMessageToDD(Guid messageId, string messageBody, string to, string toResource, string sessionId);
        /// <summary>
        /// 客服下线通知点点
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageBody"></param>
        /// <param name="to"></param>
        /// <param name="toResource"></param>
        /// <param name="sessionId"></param>
        void SendCSLogoffMessageToDD(Guid messageId, string messageBody, string to, string toResource, string sessionId);
        /// <summary>
        /// 发送订单状态变更消息
        /// </summary>
        /// <param name="orderTilte"></param>
        /// <param name="orderStatus"></param>
        /// <param name="orderType"></param>
        /// <param name="messageId"></param>
        /// <param name="messageBody"></param>
        /// <param name="to"></param>
        /// <param name="toResource"></param>
        /// <param name="sessionId"></param>
        void SendOrderChangeStatusNotify(string orderTilte, string orderStatus, string orderType,
            Guid messageId, string messageBody, string to, string toResource, string sessionId);
        /// <summary>
        /// IM,通知, 用户状态改变. 
        /// </summary>
        /// <param name="customerId"></param>
        void SendCustomLoginMessage(string userId, string userStatus,
            Guid messageId, string messageBody, string to, string toResource, string sessionId);

        void SendMessage(string xml);
        event IMReceivedMessage IMReceivedMessage;
        event IMIQ IMIQ;
        event IMStreamError IMStreamError;//登录相同账号冲突错误
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
