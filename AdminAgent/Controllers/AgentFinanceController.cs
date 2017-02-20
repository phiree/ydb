using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application.Dto;

namespace AdminAgent.Controllers
{
    public class AgentFinanceController : Controller
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        IFinanceFlowService financeFlowService = Bootstrap.Container.Resolve<IFinanceFlowService>();
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
        MemberDto memberAgent = new MemberDto();
        public ActionResult finance_account()
        {
            ViewData["SharedOrder"] = ordersService.GetOrdersCountByArea(areaList,true);
            ViewData["NotSharedOrder"] = ordersService.GetOrdersCountByArea(areaList, false);
            IList<FinanceFlowDto> financeFlowDtoList = financeFlowService.GetFinanceFlowList(areaList, memberAgent);
            return View(financeFlowDtoList);
        }
        
    }
}