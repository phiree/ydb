using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Application
{
    public class IMUserStatusService: IIMUserStatusService
    {
        IRepositoryIMUserStatus repositoryIMUserStatus;
        public IMUserStatusService(IRepositoryIMUserStatus repositoryIMUserStatus)
        {
            this.repositoryIMUserStatus = repositoryIMUserStatus;
        }

        public void Save(IMUserStatus im)
        {
            repositoryIMUserStatus.Add(im);
        }

        public void Update(IMUserStatus im)
        {
            repositoryIMUserStatus.Update(im);
        }

        public IMUserStatus GetIMUSByUserId(Guid userId)
        {
            return repositoryIMUserStatus.GetIMUSByUserId(userId);
        }

        public IList<IMUserStatus> GetOnlineListByClientName(string name)
        {
            return repositoryIMUserStatus.GetOnlineListByClientName(name);
        }
    }
}
