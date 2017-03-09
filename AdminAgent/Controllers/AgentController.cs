using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminAgent.Controllers
{
    public class AgentController : AgentBaseController
    {
        // GET: Agent
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agent
        public ActionResult footer()
        {
            return View();
        }

    }
}