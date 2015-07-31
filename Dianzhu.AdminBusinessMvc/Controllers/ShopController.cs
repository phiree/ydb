using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
namespace Dianzhu.AdminBusinessMvc.Controllers
{
    public class ShopController : Controller
    {
        DZMembershipProvider dzp = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        public ActionResult Index()
        {

            var member = (BusinessUser)dzp.GetUserByName(User.Identity.Name);
            Business b = member.BelongTo;
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View(b);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            Business b=bllBusiness.GetOne(new Guid(form["id"]));
            b.Name = form["Name"];
            bllBusiness.Updte(b);
            return View(b);
        }
        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
