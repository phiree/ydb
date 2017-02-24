using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Application;

namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IAgentNoticeService
    {
        /// <summary>
        /// 发送通知.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        ActionResult SendNotice(string noticeId);

        /// <summary>
        /// 设置为已读. 如果未读, 则不设置.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="userId"></param>
        void SetNoticeReaded(string noticeId, string userId);
    }
}