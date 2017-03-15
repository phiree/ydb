using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Finance.Application;
using Ydb.Membership.Application;

namespace AdminAgent.Controllers
{
    public class AgentFinanceController : AgentBaseController
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        IFinanceFlowService financeFlowService = Bootstrap.Container.Resolve<IFinanceFlowService>();
        IBalanceTotalService balanceTotalService= Bootstrap.Container.Resolve<IBalanceTotalService>();
        IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<IWithdrawApplyService>();
        IBalanceAccountService balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();

        /// <summary>
        /// 财务管理分账记录
        /// </summary>
        /// <returns></returns>
        public ActionResult finance_account()
        {
            try
            {
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                ViewData["SharedOrder"] = ordersService.GetOrdersCountByArea(CurrentUser.AreaIdList, true);
                ViewData["NotSharedOrder"] = ordersService.GetOrdersCountByArea(CurrentUser.AreaIdList, false);
                MemberDto memberAgent = dzMembershipService.GetUserById(CurrentUser.UserId.ToString());
                IList<FinanceFlowDto> financeFlowDtoList = financeFlowService.GetFinanceFlowList(CurrentUser.AreaIdList, memberAgent);
                //模拟数据
                //ViewData["SharedOrder"] = MockData.SharedOrder;
                //ViewData["NotSharedOrder"] = MockData.NotSharedOrder;
                //IList<FinanceFlowDto> financeFlowDtoList = MockData.financeFlowDtoList;
                return View(financeFlowDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 提现申请输入提现金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult finance_withdraw_apply(string id)
        {
            try
            {
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(id);
                if (balanceTotalDto == null || balanceTotalDto.AccountDto==null)
                {
                    throw new Exception("请先绑定该用户的体现账户");
                }
                ViewData["myAccountFinance"] = balanceTotalDto.Total;
                ViewData["myAliAccount"] = balanceTotalDto.AccountDto.Account;
                //模拟数据
                //ViewData["myAccountFinance"] = MockData.myAccountFinance;
                //ViewData["myAliAccount"] = MockData.myAliAccount;
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 提交提现申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult finance_withdraw_apply(string id,string account, decimal amount)
        {
            try
            {
                //接口
                withdrawApplyService.SaveWithdrawApply(id, account, AccountTypeEnums.Alipay, amount);
                return RedirectToAction("finance_total_list");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 绑定账户输入界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult finance_account_bind(string id)
        {
            ViewBag.UserName = CurrentUser.UserName;
            BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(id);
            Response.Cookies["alipayAcc"].Value = "";
            Response.Cookies["alipayAcc"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["owner"].Value = "";
            Response.Cookies["owner"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["IDNumber"].Value = "";
            Response.Cookies["IDNumber"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["phone"].Value = "";
            Response.Cookies["phone"].Expires = DateTime.Now.AddHours(1);
            if (balanceTotalDto == null || balanceTotalDto.AccountDto == null)
            {
            }
            else
            {
                Response.Cookies["alipayAcc"].Value = balanceTotalDto.AccountDto.Account;
                Response.Cookies["owner"].Value = balanceTotalDto.AccountDto.AccountName;
                Response.Cookies["IDNumber"].Value = balanceTotalDto.AccountDto.AccountCode;
                Response.Cookies["phone"].Value = balanceTotalDto.AccountDto.AccountPhone;
            }
            return View();
        }

        /// <summary>
        /// 提交绑定账户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <param name="accountName"></param>
        /// <param name="accountCode"></param>
        /// <param name="accountPhone"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult finance_account_bind(string id, string account, string accountName, string accountCode, string accountPhone)
        {
            try
            {
                //接口
                BalanceAccountDto balanceAccountOld = balanceAccountService.GetAccount(id);
                BalanceAccountDto balanceAccountDto = new BalanceAccountDto();
                balanceAccountDto.UserId = id;
                balanceAccountDto.Account = account;
                balanceAccountDto.AccountName = accountName;
                balanceAccountDto.AccountType = AccountTypeEnums.Alipay;
                balanceAccountDto.AccountCode = accountCode;
                balanceAccountDto.AccountPhone = accountPhone;
                balanceAccountDto.flag = 1;
                if (balanceAccountOld == null)
                {
                    balanceAccountService.BindingAccount(balanceAccountDto);
                }
                else
                {
                    balanceAccountService.UpdateAccount(balanceAccountDto);
                }
                return RedirectToAction("finance_total_list");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
        
        /// <summary>
        /// 账户余额列表用于提现
        /// </summary>
        /// <returns></returns>
        public ActionResult finance_total_list()
        {
            try
            {
                //接口
                BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(CurrentUser.UserId.ToString());
                if (balanceTotalDto == null)
                {
                    ViewData["myAccountFinance"] = 0;
                    ViewData["myAliAccount"] = "未绑定";
                }
                else
                {
                    ViewData["myAccountFinance"] = balanceTotalDto.Total;
                    ViewData["myAliAccount"] = balanceTotalDto.AccountDto==null? "未绑定": balanceTotalDto.AccountDto.Account;
                }
                IList<FinanceTotalDto> financeTotalDtoList = financeFlowService.GetFinanceTotalList(CurrentUser.AreaIdList);
                ViewData["agentId"] = CurrentUser.UserId;
                //模拟数据
                //ViewData["myAccountFinance"] = MockData.myAccountFinance;
                //ViewData["myAliAccount"] = MockData.myAliAccount;
                //IList<FinanceTotalDto> financeTotalDtoList = MockData.financeTotalDtoList;
                return View(financeTotalDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 提现记录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult finance_withdraw_history()
        {
            try
            {
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                MemberDto memberAgent = dzMembershipService.GetUserById(CurrentUser.UserId.ToString());
                FinanceWithdrawTotalDto financeWithdrawTotalDto = financeFlowService.GetFinanceWithdrawList(CurrentUser.AreaIdList, memberAgent);
                //模拟数据
                //FinanceWithdrawTotalDto financeWithdrawTotalDto = MockData.financeWithdrawTotalDto;
                return View(financeWithdrawTotalDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

    }
}