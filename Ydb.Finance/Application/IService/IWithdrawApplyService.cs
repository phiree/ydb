using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Specification;

namespace Ydb.Finance.Application
{
    public interface IWithdrawApplyService
    {
        /// <summary>
        /// 保存申请提现
        /// </summary>
        /// <param name="userId" type="string">申请人用户Id</param>
        /// <param name="account" type="string">收款账号</param
        /// <param name="accountType" type="Ydb.Finance.Application.AccountTypeEnums">收款账号类型</param>
        /// <param name="amount" type="decimal">提现金额</param>
        /// <returns type="Ydb.Finance.Application.WithdrawApplyDto">提现申请单信息</returns>
        WithdrawApplyDto SaveWithdrawApply(string userId, string account, AccountTypeEnums accountType, decimal amount);

        /// <summary>
        /// 取消申请提现
        /// </summary>
        /// <param name="applyId" type="System.Guid">提现申请Id</param>
        void ConcelWithdrawApply(Guid applyId);

        /// <summary>
        /// 根据条件获取提现申请
        /// </summary>
        /// <param name="traitFilter" type="Ydb.Common.Specification.TraitFilter">通用筛选器分页、排序等</param>
        /// <param name="withdrawApplyFilter" type="Ydb.Finance.Application.WithdrawApplyFilter">提现申请的查询筛选条件</param>
        /// <returns type="IList<WithdrawApplyDto>">提现申请信息列表</returns>
        IList<WithdrawApplyDto> GetWithdrawApplyList(TraitFilter traitFilter, WithdrawApplyFilter withdrawApplyFilter);

        /// <summary>
        /// 根据条件获取提现申请数量
        /// </summary>
        /// <param name="traitFilter" type="Ydb.Common.Specification.TraitFilter">通用筛选器分页、排序等</param>
        /// <param name="withdrawApplyFilter" type="Ydb.Finance.Application.WithdrawApplyFilter">提现申请的查询筛选条件</param>
        /// <returns type="IList<WithdrawApplyDto>">提现申请信息列表</returns>
        long GetWithdrawApplyCount(TraitFilter traitFilter, WithdrawApplyFilter withdrawApplyFilter);

        /// <summary>
        /// 获取用户ID获取体现累计金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        decimal GetTotalWithdrawApplySeccussByUserId(string userId);

        /// <summary>
        /// 支付操作
        /// </summary>
        /// <param name="withdrawApplyIds" type="IList<Guid>">要支付的提现申请Id列表</param>
        /// <param name="payUserId" type="string">支付的操作用户Id</param>
        /// <param name="errStr" type="string">返回的错误信息</param>
        /// <returns type="IList<Ydb.Finance.Application.WithdrawCashDto>"></returns>
        IList<WithdrawCashDto> PayByWithdrawApply(IList<Guid> withdrawApplyIds, string payUserId, string paySerialNo, out string errStr);

        /// <summary>
        /// 支付成功回调处理
        /// </summary>
        /// <param name="success_details" type="string">转账成功的详细信息</param>
        void PayWithdrawSuccess(string success_details);

        /// <summary>
        /// 支付失败回调处理
        /// </summary>
        /// <param name="fail_details" type="string">转账失败的详细信息</param>
        void PayWithdrawFail(string fail_details);

        /// <summary>
        /// 获取代理及其助理的所有提现信息
        /// </summary>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        IList<WithdrawApplyDto> GetWithdrawApplyListByArea(IList<string> userIdList);
    }
}
