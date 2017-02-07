using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using Ydb.PayGateway.DomainModel;
using Ydb.PayGateway.DomainModel.Repository;

namespace Ydb.PayGateway.Infrastructure.Nhibernate.Repository
{
    public class RepositoryPaymentLog : NHRepositoryBase<PaymentLog, Guid>,IRepositoryPaymentLog
    {

    }
}
