using System;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.Common;
using Ydb.Notice.Application;
using M = Ydb.Notice.DomainModel;

namespace AdminAgent.Controllers
{
    public class AgentNoticeController : Controller
    {
        private readonly IAgentNoticeService agentNoticeService;
        private readonly INoticeService noticeService;

        public AgentNoticeController(IAgentNoticeService agentNoticeService)
        {
            this.agentNoticeService = agentNoticeService;
        }

        // GET: PushMessage
        public ActionResult Index()
        {
            //获取所有公告列表

            return View();
        }

        /// <summary>
        ///     添加一条公告
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNotice()
        {
            return View();
        }

        public ActionResult SendNotice(string noticeId)
        {
            agentNoticeService.SendNotice(noticeId);
            return View();
        }

        public ActionResult TestSendNotice()
        {
            var notice = noticeService.AddNotice("title11", "<h1>title11</h1>",
                new Guid("71df3fe7-73da-11e6-99ac-02004c4f4f50"), enum_UserType.customer | enum_UserType.customerservice);

            agentNoticeService.SendNotice(notice.Id.ToString());
            return View();
        }
    }
}