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
    public class WithdrawApplyService: IWithdrawApplyService
    {
        IRepositoryWithdrawApply repositoryWithdrawApply;
        public WithdrawApplyService(IRepositoryWithdrawApply repositoryWithdrawApply)
        {
            this.repositoryWithdrawApply = repositoryWithdrawApply;
        }
    }
}
