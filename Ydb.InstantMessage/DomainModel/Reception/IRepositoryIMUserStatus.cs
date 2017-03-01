using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    public interface IRepositoryIMUserStatus : IRepository<IMUserStatus, Guid>
    {

        IMUserStatus GetIMUSByUserId(Guid userId);

        IList<IMUserStatus> GetOnlineListByClientName(string name);
    }
}
