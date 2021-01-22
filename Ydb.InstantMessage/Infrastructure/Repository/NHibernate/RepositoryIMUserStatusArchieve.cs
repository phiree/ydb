using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryIMUserStatusArchieve : NHRepositoryBase<IMUserStatusArchieve, Guid>, IRepositoryIMUserStatusArchieve
    {
        public IList<IMUserStatusArchieve> GetAllUserStatusArchieveById(string userId)
        {
            return Find(x => x.UserID == new Guid(userId)).OrderBy(x => x.ArchieveTime).ToList();
        }
    }
}
