using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.Common;
using Ydb.Notice.Application;
using M = Ydb.Notice.DomainModel;
using AdminAgent.Models;
namespace AdminAgent.Controllers
{
    public class AgentNoticeController : AgentBaseController
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
            ViewBag.UserName = CurrentUser.UserName;
            //获取所有公告列表
            IList<M.Notice> allNotice = noticeService.GetNoticeForAuther(CurrentUser.UserId);
            
            return View(allNotice);
        }
        public ActionResult AddNotice()
        {
            ViewBag.UserName = CurrentUser.UserName;
            AddNoticeModel model = new AddNoticeModel();
            Array enumList = Enum.GetValues(typeof(enum_UserType));
                ViewBag.EnumList = enumList;
            return View(model);
        }
        /// <summary>
        ///     添加一条公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNotice(AddNoticeModel notice)
        {
            ViewBag.UserName = CurrentUser.UserName;
            noticeService.AddNotice(notice.Title, notice.Body, CurrentUser.UserId, ParseString(Request["UserType"]));
            return View();
        }
        /// <summary>
        /// 字符串转换为enum值
        /// </summary>
        /// <param name="enumValues">逗号隔开的enum值 如, '1,2,4,8'</param>
        /// <returns></returns>
        private enum_UserType ParseString(string enumValues)
        {
          return (enum_UserType)enumValues.Split(',').Sum(x=>Convert.ToInt32(x));
             
        }

        public ActionResult NoticeDetail(string noticeId)
        {
            ViewBag.UserName = CurrentUser.UserName;
            M.Notice notice = noticeService.GetOne(noticeId);
            return View(notice);
        }

        public ActionResult SendNotice(string noticeId)
        {
            ViewBag.UserName = CurrentUser.UserName;
            // agentNoticeService.SendNotice(noticeId);
            return View();
        }

        public ActionResult TestSendNotice()
        {
            Guid userId = Guid.Empty;
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            userId =CurrentUser.UserId;
            var notice = noticeService.AddNotice("title11", "<h1>title11</h1>",
               userId,  enum_UserType.customer | enum_UserType.customerservice);
            Ydb.InstantMessage.Application.IInstantMessage imService =
                (Ydb.InstantMessage.Application.IInstantMessage) HttpContext.Application["im"];

            agentNoticeService.SendNotice(imService, notice.Id.ToString(),true);
            return Content("fasongchenggong");
        }
    }
}