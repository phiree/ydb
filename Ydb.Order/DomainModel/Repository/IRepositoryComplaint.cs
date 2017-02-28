using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IRepositoryComplaint : IRepository<Complaint, Guid>
    {
    }
}
