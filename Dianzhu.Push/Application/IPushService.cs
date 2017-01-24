namespace Dianzhu.Push.Application
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
        string Push(string fromUser, string toUser,string orderId, string messageContent, string messageType);
    }
}