using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Order.DomainModel;

namespace Ydb.Order.Application
{
    public interface IClaimsDetailsService
    {
        IList<ClaimsDetails> GetRefundStatus(Guid orderID, TraitFilter filter, enum_RefundAction action);
    }
}