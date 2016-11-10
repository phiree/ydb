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
    public class BalanceAccountMap : ClassMap<BalanceAccount>
    {
        public BalanceAccountMap()
        {
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.Account);
            Map(x => x.AccountName);
            Map(x => x.AccountType);
            Map(x => x.AccountPhone);
            Map(x => x.AccountCode);
            Map(x => x.flag);
        }
    }
}
