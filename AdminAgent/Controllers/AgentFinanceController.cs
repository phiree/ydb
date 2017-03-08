﻿using System;
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
    public class AgentFinanceController : AgentBaseController
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        IFinanceFlowService financeFlowService = Bootstrap.Container.Resolve<IFinanceFlowService>();
        IBalanceTotalService balanceTotalService= Bootstrap.Container.Resolve<IBalanceTotalService>();
        IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<IWithdrawApplyService>();
        IBalanceAccountService balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
        
        IList<string> areaList = new List<string> { "2445", "2446", "2447", "2448", "2449", "2450" };
        MemberDto memberAgent = new MemberDto() { Id=new Guid("002b0b23-e069-4e9b-95ef-d8fb1827cce0") };
        /// <summary>
        /// 财务管理分账记录
        /// </summary>
        /// <returns></returns>
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
                //BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(id);
                //ViewData["myAccountFinance"] = balanceTotalDto.Total;
                //ViewData["myAliAccount"] = balanceTotalDto.AccountDto.Account;
                //模拟数据
                ViewData["myAccountFinance"] = MockData.myAccountFinance;
                ViewData["myAliAccount"] = MockData.myAliAccount;
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
                //BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(memberAgent.Id.ToString());
                //ViewData["myAccountFinance"] = balanceTotalDto.Total;
                //ViewData["myAliAccount"] = balanceTotalDto.AccountDto.Account;
                //IList<FinanceTotalDto> financeTotalDtoList = financeFlowService.GetFinanceTotalList(areaList);
                ViewData["agentId"] = memberAgent.Id;
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

        /// <summary>
        /// 提现记录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult finance_withdraw_history()
        {
            try
            {
                //接口
                //FinanceWithdrawTotalDto financeWithdrawTotalDto = financeFlowService.GetFinanceWithdrawList(areaList, memberAgent);
                //模拟数据
                FinanceWithdrawTotalDto financeWithdrawTotalDto = MockData.financeWithdrawTotalDto;
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