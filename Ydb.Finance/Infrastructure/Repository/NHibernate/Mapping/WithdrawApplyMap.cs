using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
using FluentNHibernate.Mapping;
using Ydb.Common.Domain;

namespace Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping
{
    public class WithdrawApplyMap : ClassMap<WithdrawApply>
    {
        public WithdrawApplyMap()
        {
            Id(x => x.Id);
            Map(x => x.ApplyUserId);
            Map(x => x.ApplyAmount);
            Map(x => x.ApplyTime);
            References< BalanceAccount>(x => x.ReceiveAccount);
            Map(x => x.TransferAmount);
            Map(x => x.ServiceFee);
            Map(x => x.ApplyStatus);
            Map(x => x.ApplyRemark);
            Map(x => x.Rate);
            Map(x => x.PayUserId);
            Map(x => x.PayTime);
            Map(x => x.PayStatus);
            Map(x => x.PayRemark);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);
            Map(x => x.SerialNo);
        }
    }

}
