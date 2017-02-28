using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common;

namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryIMUserStatus : NHRepositoryBase<IMUserStatus, Guid>, IRepositoryIMUserStatus
    {
        public IMUserStatus GetIMUSByUserId(Guid userId)
        {
            return FindOne(x => x.UserID == userId);
        }

        public IList<IMUserStatus> GetOnlineListByClientName(string name)
        {
            return Find(x => x.ClientName == name && x.Status == enum_UserStatus.available);
        }
    }
}
