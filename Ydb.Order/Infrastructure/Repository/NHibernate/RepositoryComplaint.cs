using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;

namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
    public class RepositoryComplaint : NHRepositoryBase<Complaint, Guid>, IRepositoryComplaint
    {

    }
}
