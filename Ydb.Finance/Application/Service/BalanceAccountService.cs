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

namespace Ydb.Finance.Application
{
    public class BalanceAccountService: IBalanceAccountService
    {
        IRepositoryBalanceAccount repositoryBalanceAccount;
        public BalanceAccountService(IRepositoryBalanceAccount repositoryBalanceAccount)
        {
            this.repositoryBalanceAccount = repositoryBalanceAccount;
        }

        /// <summary>
        /// 绑定提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要绑定的账号信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void BindingAccount(BalanceAccountDto balanceAccountDto)
        {
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceAccountNow != null)
            {
                throw new Exception("该用户已经绑定了提现账号！");
            }
            balanceAccountNow = Mapper.Map<BalanceAccountDto, BalanceAccount>(balanceAccountDto);
            balanceAccountNow.flag = 1;
            repositoryBalanceAccount.Add(balanceAccountNow);
        }

        /// <summary>
        /// 修改提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要修改的账号信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void UpdateAccount(BalanceAccountDto balanceAccountDto)
        {
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceAccountNow == null)
            {
                throw new Exception("该用户还没有绑定提现账号！");
            }
            if (balanceAccountNow.AccountPhone != balanceAccountDto.AccountPhone)
            {
                throw new Exception("输入的身份证和绑定的身份证不一致！");
            }
            BalanceAccount balanceAccountOld = repositoryBalanceAccount.GetOneByAccount(balanceAccountDto.Account,balanceAccountDto.AccountType.ToString());
            if (balanceAccountOld != null)
            {
                balanceAccountDto.Id = balanceAccountOld.Id;
            }
            balanceAccountOld = Mapper.Map<BalanceAccountDto, BalanceAccount>(balanceAccountDto);
            repositoryBalanceAccount.SaveOrUpdate(balanceAccountNow);
        }

        /// <summary>
        /// 根据用户获取该用户的提现账号
        /// </summary>
        /// <param name="userId" type="string"用户账号ID</param>
        /// <returns type="BalanceAccountDto">提现账号信息</returns>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public BalanceAccountDto GetAccount(string userId)
        {
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(userId);
            return Mapper.Map<BalanceAccount, BalanceAccountDto>(balanceAccountNow);
        }
    }
}
