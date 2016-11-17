using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Ydb.Common;

namespace Dianzhu.DAL
{
    public class DALIMUserStatus : NHRepositoryBase<IMUserStatus, Guid>, IDAL.IDALIMUserStatus
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
