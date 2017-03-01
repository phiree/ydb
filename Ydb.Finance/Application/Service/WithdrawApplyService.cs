using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Collections;
using Ydb.Finance.DomainModel.Enums;
using Ydb.Finance.DomainModel;
using AutoMapper;
using Ydb.Common.Specification;
using Ydb.Common.Infrastructure;

namespace Ydb.Finance.Application
{
    public class WithdrawApplyService: IWithdrawApplyService
    {
        IRepositoryWithdrawApply repositoryWithdrawApply;
        IRepositoryBalanceAccount repositoryBalanceAccount;
        IRepositoryBalanceTotal repositoryBalanceTotal;
        IRepositoryBalanceFlow repositoryBalanceFlow;
        ICountServiceFee countServiceFee;
        ISerialNoBuilder serialNoBuilder;
        public WithdrawApplyService(IRepositoryWithdrawApply repositoryWithdrawApply, IRepositoryBalanceAccount repositoryBalanceAccount, IRepositoryBalanceTotal repositoryBalanceTotal, IRepositoryBalanceFlow repositoryBalanceFlow, ICountServiceFee countServiceFee, ISerialNoBuilder serialNoBuilder)
        {
            this.repositoryWithdrawApply = repositoryWithdrawApply;
            this.repositoryBalanceAccount = repositoryBalanceAccount;
            this.repositoryBalanceTotal = repositoryBalanceTotal;
            this.repositoryBalanceFlow = repositoryBalanceFlow;
            this.countServiceFee = countServiceFee;
            this.serialNoBuilder = serialNoBuilder;
            
        }

        /// <summary>
        /// 保存申请提现
        /// </summary>
        /// <param name="userId" type="string">申请人用户Id</param>
        /// <param name="account" type="string">收款账号</param
        /// <param name="accountType" type="Ydb.Finance.Application.AccountTypeEnums">收款账号类型</param>
        /// <param name="amount" type="decimal">提现金额</param>
        /// <returns type="Ydb.Finance.Application.WithdrawApplyDto">提现申请单信息</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public WithdrawApplyDto SaveWithdrawApply(string userId, string account, AccountTypeEnums accountType, decimal amount)
        {
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(userId);
            if (balanceAccountNow == null)
            {
                throw new Exception("该用户没有绑定提现的收款账号！");
            }
            if (balanceAccountNow.Account != account || balanceAccountNow.AccountType != accountType.ToString())
            {
                throw new Exception("指定的收款账号不正确！");
            }
            if (amount <= 1)
            {
                throw new Exception("最低提现金额至少要超过1元");
            }
            string strSerialNo = serialNoBuilder.GetSerialNo("AW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"), 2);
            repositoryBalanceTotal.FrozenBalance(userId, amount);
            WithdrawApply withdrawApply = new WithdrawApply();
            withdrawApply.ApplySerialNo = strSerialNo;
            withdrawApply.ApplyUserId = userId;
            withdrawApply.ApplyTime = DateTime.Now;
            withdrawApply.ApplyStatus = "ApplyWithdraw";
            withdrawApply.ApplyAmount = amount;
            withdrawApply.ReceiveAccount = balanceAccountNow;
            withdrawApply.Rate = "0.005";
            withdrawApply.ServiceFee = countServiceFee.CountServiceFee(amount, "0.5%");
            withdrawApply.TransferAmount = amount - withdrawApply.ServiceFee;
            repositoryWithdrawApply.Add(withdrawApply);
            return Mapper.Map<WithdrawApply, WithdrawApplyDto>(withdrawApply);
        }

        /// <summary>
        /// 取消申请提现
        /// </summary>
        /// <param name="applyId" type="System.Guid">提现申请Id</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void ConcelWithdrawApply(Guid applyId)
        {
            WithdrawApply withdrawApply = repositoryWithdrawApply.FindById(applyId);
            if (withdrawApply == null)
            {
                throw new Exception("该提现申请不存在！");
            }
            if (withdrawApply.ApplyStatus != "ApplyWithdraw")
            {
                throw new Exception("该申请已经付款成功或已被取消！");
            }
            repositoryBalanceTotal.ReleaseBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount);
            withdrawApply.ApplyStatus = "ApplyCancel";
            withdrawApply.UpdateTime = DateTime.Now;
            repositoryWithdrawApply.Update(withdrawApply);
        }

        /// <summary>
        /// 根据条件获取提现申请
        /// </summary>
        /// <param name="traitFilter" type="Ydb.Common.Specification.TraitFilter">通用筛选器分页、排序等</param>
        /// <param name="withdrawApplyFilter" type="Ydb.Finance.Application.WithdrawApplyFilter">提现申请的查询筛选条件</param>
        /// <returns type="IList<WithdrawApplyDto>">提现申请信息列表</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<WithdrawApplyDto> GetWithdrawApplyList(TraitFilter traitFilter, WithdrawApplyFilter withdrawApplyFilter)
        {
            var where = PredicateBuilder.True<WithdrawApply>();
            if (!string.IsNullOrEmpty(withdrawApplyFilter.ApplyUserId))
            {
                where = where.And(x => x.ApplyUserId == withdrawApplyFilter.ApplyUserId);
            }
            if (!string.IsNullOrEmpty(withdrawApplyFilter.PaySerialNo))
            {
                where = where.And(x => x.PaySerialNo == withdrawApplyFilter.PaySerialNo);
            }
            if (withdrawApplyFilter.ApplyStatus!=ApplyStatusEnums.None)
            {
                where = where.And(x => x.ApplyStatus == withdrawApplyFilter.ApplyStatus.ToString());
            }
            if (withdrawApplyFilter.AccountType!=AccountTypeEnums.None)
            {
                where = where.And(x => x.ReceiveAccount.AccountType == withdrawApplyFilter.AccountType.ToString());
            }
            if (!string.IsNullOrEmpty(withdrawApplyFilter.PayUserId))
            {
                where = where.And(x => x.PayUserId == withdrawApplyFilter.PayUserId);
            }
            if (withdrawApplyFilter.BeginApplyTime != DateTime.MinValue)
            {
                where = where.And(x => x.ApplyTime >= withdrawApplyFilter.BeginApplyTime);
            }
            if (withdrawApplyFilter.EndApplyTime != DateTime.MinValue)
            {
                where = where.And(x => x.ApplyTime <= withdrawApplyFilter.EndApplyTime);
            }
            if (withdrawApplyFilter.BeginPayTime != DateTime.MinValue)
            {
                where = where.And(x => x.PayTime >= withdrawApplyFilter.BeginPayTime);
            }
            if (withdrawApplyFilter.EndPayTime != DateTime.MinValue)
            {
                where = where.And(x => x.PayTime <= withdrawApplyFilter.EndPayTime);
            }
            WithdrawApply baseone = null;
            if (!string.IsNullOrEmpty(traitFilter.baseID))
            {
                try
                {
                    baseone = repositoryWithdrawApply.FindByBaseId(new Guid(traitFilter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long totalRecord;
            var list = traitFilter.pageSize == 0 ? repositoryWithdrawApply.Find(where, traitFilter.sortby, traitFilter.ascending, traitFilter.offset, baseone).ToList() : repositoryWithdrawApply.Find(where, traitFilter.pageNum, traitFilter.pageSize, out totalRecord, traitFilter.sortby, traitFilter.ascending, traitFilter.offset, baseone).ToList();

            return Mapper.Map<IList<WithdrawApply>, IList<WithdrawApplyDto>>(list);
        }

        /// <summary>
        /// 获取代理及其助理的所有提现信息
        /// </summary>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<WithdrawApplyDto> GetWithdrawApplyListByArea(IList<string> userIdList)
        {
            var where = PredicateBuilder.True<WithdrawApply>();
            if (userIdList.Count >0)
            {
                where = where.And(x => userIdList.Contains(x.ApplyUserId));
            }
            var list =  repositoryWithdrawApply.Find(where).ToList();
            return Mapper.Map<IList<WithdrawApply>, IList<WithdrawApplyDto>>(list);
        }

        /// <summary>
        /// 支付操作
        /// </summary>
        /// <param name="withdrawApplyIds" type="IList<Guid>">要支付的提现申请Id列表</param>
        /// <param name="payUserId" type="string">支付的操作用户Id</param>
        /// <param name="errStr" type="string">返回的错误信息</param>
        /// <returns type="IList<Ydb.Finance.Application.WithdrawCashDto>"></returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public IList<WithdrawCashDto> PayByWithdrawApply(IList<Guid> withdrawApplyIds, string payUserId,string paySerialNo,out string errStr)
        {
            errStr = "[";
            IList<WithdrawCashDto> withdrawCashDtoList = new List<WithdrawCashDto>();
            for (int i = 0; i < withdrawApplyIds.Count; i++)
            {
                string strErr = "";
                WithdrawApply withdrawApply = repositoryWithdrawApply.FindById(withdrawApplyIds[i]);
                if (withdrawApply == null)
                {
                    strErr = "提现申请" + withdrawApplyIds[i].ToString() + "不存在!";
                    errStr = errStr + "{\"errCode\":\"NoApply\",\"errMsg\":\"" + strErr + "\"";
                }
                if (withdrawApply.ApplyStatus != "ApplyWithdraw")
                {
                    strErr = "该申请单" + withdrawApplyIds[i].ToString() + "已是" + withdrawApply.ApplyStatus + "状态!";
                    errStr = errStr + "{\"errCode\":\"errStatus\",\"errMsg\":\"" + strErr + "\"";
                }
                withdrawApply.PaySerialNo = paySerialNo;
                withdrawApply.PayUserId = payUserId;
                withdrawApply.PayTime = DateTime.Now;
                withdrawApply.UpdateTime = DateTime.Now;
                if (strErr != "")
                {
                    withdrawApply.ApplyStatus = "Payfail";
                    withdrawApply.PayRemark = errStr;
                    repositoryBalanceTotal.ReleaseBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount);
                    continue;
                }
                //回调的时候再清除冻结账户
                //repositoryBalanceTotal.OutFrozenBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount);
                //回调的时候再插入流水
                //BalanceFlow flow = new BalanceFlow
                //{
                //    AccountId = withdrawApply.ApplyUserId,
                //    Amount = withdrawApply.ApplyAmount,
                //    RelatedObjectId = withdrawApply.Id.ToString(),
                //    SerialNo = withdrawApply.SerialNo,
                //    OccurTime = DateTime.Now,
                //    FlowType = FlowType.Withdrawals,
                //    Income = false
                //};
                //repositoryBalanceFlow.Add(flow);
                WithdrawCashDto withdrawCashDto = new WithdrawCashDto
                {
                    UserId = withdrawApply.ApplyUserId,
                    Amount = withdrawApply.TransferAmount,
                    Account = withdrawApply.ReceiveAccount.Account,
                    AccountName = withdrawApply.ReceiveAccount.AccountName,
                    AccountType = (AccountTypeEnums)Enum.Parse(typeof(AccountTypeEnums), withdrawApply.ReceiveAccount.AccountType),
                    Remark = withdrawApply.ApplyRemark,
                    PaySerialNo = paySerialNo,
                    ApplySerialNo = withdrawApply.ApplySerialNo
                };
                withdrawCashDtoList.Add(withdrawCashDto);
            }
            errStr = errStr+"]";
            return withdrawCashDtoList;
        }

        /// <summary>
        /// 支付成功回调处理
        /// </summary>
        /// <param name="success_details" type="string">转账成功的详细信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void PayWithdrawSuccess(string success_details)
        {
            IList<List<string>> DetailItems = AnalysisDetails(success_details);
            for (int i = 0; i < DetailItems.Count; i++)
            {
                WithdrawApply withdrawApply = repositoryWithdrawApply.FindOne(x=>x.ApplySerialNo== DetailItems[i][0]);
                if (withdrawApply != null)
                {
                    withdrawApply.ApplyStatus = "PaySeccuss";
                    withdrawApply.PayStatus = "BATCH_TRANS_NOTIFY";
                    withdrawApply.PayRemark = "转账成功";
                    withdrawApply.D3SerialNo = DetailItems[i][6];
                    withdrawApply.D3Time = DetailItems[i][7];
                    withdrawApply.UpdateTime = DateTime.Now;
                    repositoryWithdrawApply.Update(withdrawApply);
                    SaveBalanceFlow(withdrawApply);
                    repositoryBalanceTotal.OutFrozenBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount);
                }
            }
        }

        /// <summary>
        /// 支付失败回调处理
        /// </summary>
        /// <param name="fail_details" type="string">转账失败的详细信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void PayWithdrawFail(string fail_details)
        {
            IList<List<string>> DetailItems = AnalysisDetails(fail_details);
            for (int i = 0; i < DetailItems.Count; i++)
            {
                WithdrawApply withdrawApply = repositoryWithdrawApply.FindOne(x => x.ApplySerialNo == DetailItems[i][0]);
                if (withdrawApply != null)
                {
                    withdrawApply.ApplyStatus = "Payfail";
                    withdrawApply.PayStatus = DetailItems[i][5];
                    withdrawApply.PayRemark = string.Empty;// Dianzhu.Pay.PayCallBackAliBatch.TradeStatus[DetailItems[i][5]];
                    withdrawApply.D3SerialNo = DetailItems[i][6];
                    withdrawApply.D3Time = DetailItems[i][7];
                    withdrawApply.UpdateTime = DateTime.Now;
                    repositoryWithdrawApply.Update(withdrawApply);
                    //repositoryBalanceTotal.InBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount,"");
                    repositoryBalanceTotal.ReleaseBalance(withdrawApply.ApplyUserId, withdrawApply.ApplyAmount);
                }
            }
        }

        /// <summary>
        /// 保存提现流水
        /// </summary>
        /// <param name="withdrawApply" type="Ydb.Finance.DomainModel.WithdrawApply">提现申请信息</param>
        void SaveBalanceFlow(WithdrawApply withdrawApply)
        {
            BalanceFlow flow = new BalanceFlow
            {
                AccountId = withdrawApply.ApplyUserId,
                Amount = withdrawApply.ApplyAmount,
                RelatedObjectId = withdrawApply.Id.ToString(),
                SerialNo = withdrawApply.ApplySerialNo,
                OccurTime = DateTime.Now,
                FlowType = FlowType.Withdrawals,
                Income = false,
                AmountTotal = withdrawApply.ApplyAmount.ToString(),
                Rate = "0.005",
                AmountView = "-("+ String.Format("{0:F}", withdrawApply.TransferAmount)+"+"+ String.Format("{0:F}", withdrawApply.ServiceFee) + ")",
                
            };
            repositoryBalanceFlow.Add(flow);
        }

        /// <summary>
        /// 解析转账的详细信息
        /// </summary>
        /// <param name="details" type="string">转账成功的详细信息</param>
        /// <returns type="IList<List<string>>">解析后的数组数据</returns>
        IList<List<string>> AnalysisDetails(string details)
        {
            IList<List<string>> DetailItems = new List<List<string>>();
            string[] arrDetail = details.Split('|');
            for (int i = 0; i < arrDetail.Length; i++)
            {
                List<string> Items = arrDetail[i].Split('^').ToList();
                DetailItems.Add(Items);
            }
            return DetailItems;
        }
    }
}
