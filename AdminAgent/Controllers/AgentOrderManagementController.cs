using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application;

namespace AdminAgent.Controllers
{
    public class AgentOrderManagementController : Controller
    {
        // GET: AgentOrderManagement
        public ActionResult OrderList()
        {
            
            return View();
        }
    }
}