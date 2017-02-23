using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminAgent.Controllers
{
    public class AgentNoticeController : Controller
    {
        // GET: PushMessage
        public ActionResult Index()
        {
            //获取所有公告列表

            return View();
        }
        /// <summary>
        /// 添加一条公告
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNotice()
        {


            return View();
        }
    }
}