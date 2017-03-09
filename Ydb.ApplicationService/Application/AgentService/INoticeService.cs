using Ydb.Common.Application;
using Ydb.InstantMessage.Application;

namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IAgentNoticeService
    {
        /// <summary>
        ///     发送通知.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        ActionResult SendNotice(IInstantMessage imService,string noticeId);

        /// <summary>
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="isDebug">true:苹果推送的沙箱环境,false:正式环境</param>
        /// <returns></returns>
        ActionResult SendNotice(IInstantMessage imService,string noticeId, bool isDebug);

       
    }
}