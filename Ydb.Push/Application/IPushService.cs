using Ydb.Common.Application;

namespace Ydb.Push.Application
{
    public interface IPushService
    {
        /// <summary>
        /// 推送
        /// </summary>
        /// <param name="fromUser"></param>
        /// <param name="toUser"></param>
        /// <param name="orderId"></param>
        /// <param name="messageContent"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        ActionResult Push(string chatMessage, string chatType, string toUserId, string fromClient, string fromUserName,
                          string orderId, string orderSerialNo, string orderStatus, string orderStatusStr,
                          string businessName, string toClient);
    }
}