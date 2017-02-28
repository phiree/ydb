using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;


namespace Ydb.InstantMessage.Application
{
    public interface IIMUserStatusService
    {
        void Save(IMUserStatus im);

        void Update(IMUserStatus im);

        IMUserStatus GetIMUSByUserId(Guid userId);

        IList<IMUserStatus> GetOnlineListByClientName(string name);
    }
}
