using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Repository;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryPaymentLog : IRepository<PaymentLog, Guid>
    {
         
    }
}
