using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    public interface IRepositoryIMUserStatusArchieve : IRepository<IMUserStatusArchieve, Guid>
    {
        IList<IMUserStatusArchieve> GetAllUserStatusArchieveById(string userId);
    }
}
