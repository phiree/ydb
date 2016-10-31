using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Infrastructure.Repository;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.DomainModel.Reception
{
   public interface IRepositoryReception: IRepository<ReceptionStatus,Guid>
    {
        IList<ReceptionStatus> FindByCustomerId(string customerId);
        IList<ReceptionStatus> FindByCustomerServiceId(string csId);
        IList<ReceptionStatus> FindByDiandian(string diandianId, int amount);
    }
}
