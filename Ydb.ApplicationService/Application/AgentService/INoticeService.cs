using Ydb.Common.Application;

namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IAgentNoticeService
    {
        /// <summary>
        ///     发送通知.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        ActionResult SendNotice(string noticeId);

        /// <summary>
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="isDebug">true:苹果推送的沙箱环境,false:正式环境</param>
        /// <returns></returns>
        ActionResult SendNotice(string noticeId, bool isDebug);

        /// <summary>
        ///     设置为已读. 如果未读, 则不设置.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="userId"></param>
        void SetNoticeReaded(string noticeId, string userId);
    }
}