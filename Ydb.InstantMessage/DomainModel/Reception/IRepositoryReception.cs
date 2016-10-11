using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.InstantMessage.DomainModel.Reception;
namespace Ydb.InstantMessage.DomainModel.Reception
{
   public interface IRepositoryReception: IRepository<ReceptionStatus,Guid>
    {
        IList<ReceptionStatus> FindByCustomerId(string customerId);
    }
}
