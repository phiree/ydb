using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web.Security;
namespace Dianzhu.AdminBusinessMvc.Controllers
{
    public class DZServiceController : Controller
    {
        DZMembershipProvider dzp = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();

         
         
        public ActionResult List(Guid businessId)
        {
            return null;
        }
        [HttpGet]
        public ActionResult Edit(Guid serviceId)
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Edit(string serviceId)
        {
            return null;
        }

         
    }
}
