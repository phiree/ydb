using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminAgent.Controllers
{
    public class AgentFinanceController : Controller
    {
        public ActionResult finance_account()
        {
            ViewData["SharedOrder"] = 1230;
            ViewData["NotSharedOrder"] = 30;
            return View();
        }
        
    }
}