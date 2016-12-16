using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;
using Ydb.Order.DomainModel;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryRefund : IRepository<Refund,Guid>
    {
        Refund GetRefundByPlatformTradeNo(string platformTradeNo);
    }
}
