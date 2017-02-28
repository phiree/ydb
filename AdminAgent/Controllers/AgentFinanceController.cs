using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application.Dto;
using Ydb.Finance.Application;

namespace AdminAgent.Controllers
{
    public class AgentFinanceController : Controller
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        IFinanceFlowService financeFlowService = Bootstrap.Container.Resolve<IFinanceFlowService>();
        IBalanceTotalService balanceTotalService= Bootstrap.Container.Resolve<IBalanceTotalService>();
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
        MemberDto memberAgent = new MemberDto();
        public ActionResult finance_account()
        {
            try
            {
                //接口
                //ViewData["SharedOrder"] = ordersService.GetOrdersCountByArea(areaList, true);
                //ViewData["NotSharedOrder"] = ordersService.GetOrdersCountByArea(areaList, false);
                //IList<FinanceFlowDto> financeFlowDtoList = financeFlowService.GetFinanceFlowList(areaList, memberAgent);
                //模拟数据
                ViewData["SharedOrder"] = MockData.SharedOrder;
                ViewData["NotSharedOrder"] = MockData.NotSharedOrder;
                IList<FinanceFlowDto> financeFlowDtoList = MockData.financeFlowDtoList;
                return View(financeFlowDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult finance_withdraw_apply()
        {
            try
            {
                //接口
                //ViewData["myAccountFinance"] = ordersService.GetOrdersCountByArea(areaList, true);
                //ViewData["myAliAccount"] = ordersService.GetOrdersCountByArea(areaList, false);
                //IList<FinanceFlowDto> financeFlowDtoList = financeFlowService.GetFinanceFlowList(areaList, memberAgent);
                //模拟数据
                ViewData["myAccountFinance"] = MockData.myAccountFinance;
                ViewData["myAliAccount"] = MockData.myAliAccount;
                IList<FinanceFlowDto> financeFlowDtoList = MockData.financeFlowDtoList;
                return View(financeFlowDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult finance_total_list()
        {
            try
            {
                //接口
                //BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(memberAgent.Id.ToString());
                //ViewData["myAccountFinance"] = balanceTotalDto.Total;
                //ViewData["myAliAccount"] = balanceTotalDto.AccountDto.Account;
                //IList<FinanceTotalDto> financeTotalDtoList = financeFlowService.GetFinanceTotalList(areaList);
                //模拟数据
                ViewData["myAccountFinance"] = MockData.myAccountFinance;
                ViewData["myAliAccount"] = MockData.myAliAccount;
                IList<FinanceTotalDto> financeTotalDtoList = MockData.financeTotalDtoList;
                return View(financeTotalDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

    }
}