using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace Dianzhu.Web.RestfulApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public IEnumerable<string> Get()
        //{
        //    Thread.Sleep(600000);
        //    return new string[] { "测试时长：10分钟" };
        //}
    }
}
