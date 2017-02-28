using System;
using System.Linq;
using System.Security.Claims;
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

        public AgentNoticeController()
        {
            this.agentNoticeService = Bootstrap.Container.Resolve<IAgentNoticeService>();
            this.noticeService = Bootstrap.Container.Resolve<INoticeService>();
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
            Guid userId = Guid.Empty;
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            userId =new Guid( claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var notice = noticeService.AddNotice("title11", "<h1>title11</h1>",
               userId,  enum_UserType.customer | enum_UserType.customerservice);

            agentNoticeService.SendNotice(notice.Id.ToString(),true);
            return Content("fasongchenggong");
        }
    }
}