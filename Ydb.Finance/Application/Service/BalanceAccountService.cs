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
        IRepositoryBalanceTotal repositoryBalanceTotal;
        public BalanceAccountService(IRepositoryBalanceAccount repositoryBalanceAccount, IRepositoryBalanceTotal repositoryBalanceTotal)
        {
            this.repositoryBalanceAccount = repositoryBalanceAccount;
            this.repositoryBalanceTotal = repositoryBalanceTotal;
        }

        bool CheckBalanceAccountDto(BalanceAccountDto balanceAccountDto ,out string errMsg)
        {
            errMsg = "";
            bool b = false;
            if (string.IsNullOrEmpty(balanceAccountDto.UserId))
            {
                b = true;
                errMsg = "要绑定的用户Id不能为空！";
            }
            if (string.IsNullOrEmpty(balanceAccountDto.Account))
            {
                b = true;
                errMsg = "要绑定的提现账号不能为空！";
            }
            if (string.IsNullOrEmpty(balanceAccountDto.AccountName))
            {
                b = true;
                errMsg = "要绑定的提现账号真实姓名不能为空！";
            }
            if (balanceAccountDto.AccountType== AccountTypeEnums.None)
            {
                b = true;
                errMsg = "要绑定的提现账号类型未指定！";
            }
            if (string.IsNullOrEmpty(balanceAccountDto.AccountCode))
            {
                b = true;
                errMsg = "要绑定的用户身份证不能为空！";
            }
            return b;
        }

        /// <summary>
        /// 绑定提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要绑定的账号信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void BindingAccount(BalanceAccountDto balanceAccountDto)
        {
            string errMsg = "";
            if (CheckBalanceAccountDto(balanceAccountDto, out errMsg))
            {
                throw new Exception(errMsg);
            }
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceAccountNow != null)
            {
                throw new Exception("该用户已经绑定了提现账号！");
            }

            BalanceTotal balanceTotal = repositoryBalanceTotal.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceTotal == null)
            {
                balanceTotal = new DomainModel.BalanceTotal { UserId = balanceAccountDto.UserId };
                repositoryBalanceTotal.Add(balanceTotal);
            }
            balanceAccountNow = Mapper.Map<BalanceAccountDto, BalanceAccount>(balanceAccountDto);
            balanceAccountNow.flag = 1;
            repositoryBalanceAccount.Add(balanceAccountNow);
            balanceTotal.Account = balanceAccountNow;
        }

        /// <summary>
        /// 修改提现账号
        /// </summary>
        /// <param name="balanceAccountDto" type="BalanceAccountDto">要修改的账号信息</param>
        [Ydb.Finance.Infrastructure.UnitOfWork]
        public void UpdateAccount(BalanceAccountDto balanceAccountDto)
        {
            string errMsg = "";
            if (CheckBalanceAccountDto(balanceAccountDto, out errMsg))
            {
                throw new Exception(errMsg);
            }
            BalanceAccount balanceAccountNow = repositoryBalanceAccount.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceAccountNow == null)
            {
                throw new Exception("该用户还没有绑定提现账号！");
            }
            if (balanceAccountNow.AccountCode != balanceAccountDto.AccountCode)
            {
                throw new Exception("输入的身份证和绑定的身份证不一致！");
            }
            
            BalanceAccount balanceAccountOld = repositoryBalanceAccount.GetOneByAccount(balanceAccountDto.Account,balanceAccountDto.AccountType.ToString(), balanceAccountDto.UserId);
            if (balanceAccountNow != balanceAccountOld)
            {
                balanceAccountNow.flag = 0;
            }

            if (balanceAccountOld != null)
            {
                balanceAccountOld.AccountPhone = balanceAccountDto.AccountPhone;
                balanceAccountOld.AccountName = balanceAccountDto.AccountName;
                balanceAccountOld.flag = 1;
            }
            else
            {
                balanceAccountOld = Mapper.Map<BalanceAccountDto, BalanceAccount>(balanceAccountDto);
                repositoryBalanceAccount.Add(balanceAccountOld);
            }

            BalanceTotal balanceTotal = repositoryBalanceTotal.GetOneByUserId(balanceAccountDto.UserId);
            if (balanceTotal == null)
            {
                balanceTotal = new DomainModel.BalanceTotal { UserId = balanceAccountDto.UserId };
                repositoryBalanceTotal.Add(balanceTotal);
            }
            balanceTotal.Account = balanceAccountOld;
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
