using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALIMUserStatus : NHRepositoryBase<IMUserStatus, Guid>, IDAL.IDALIMUserStatus
    {

        public IMUserStatus GetIMUSByUserId(Guid userId)
        {
            using (var tr = Session.BeginTransaction())
            {
                var result = Session.QueryOver<IMUserStatus>().Where(x => x.UserID == userId).SingleOrDefault();

                tr.Commit();
                return result;
            }


        }

        public IList<IMUserStatus> GetOnlineListByClientName(string name)
        {
            using (var tr = Session.BeginTransaction())
            {
                var result = Session.QueryOver<IMUserStatus>().Where(x => x.ClientName == name).And(x => x.Status == Model.Enums.enum_UserStatus.available).List();

                tr.Commit();
                return result;
            }

        }

    }
}
