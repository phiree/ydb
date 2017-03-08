using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Notice.DomainModel.Repository;
using Ydb.Common.Repository;
using M = Ydb.Notice.DomainModel;
using Ydb.Notice.DomainModel;
using System.Linq.Expressions;
using Ydb.Common.Specification;
namespace Ydb.Notice.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryUserNotice : YdbNHibernate.Repository.NHRepositoryBase<M.UserNotice, Guid>, IRepositoryUserNotice
    {
        public void AddNoticeToUser(M.Notice notice, string userId)
        {
            DomainModel.UserNotice userNotice = new DomainModel.UserNotice(new Guid( userId), notice);
            SaveOrUpdate(userNotice);
        }
        public IList<UserNotice> FindNoticeToUser(string userId, bool? isReaded)
        {
            var where = Common.Specification.PredicateBuilder.True<UserNotice>();
            where = where.And(x => x.UserId == new Guid(userId));
            if (isReaded.HasValue)
            {
                where = where.And(x => x.IsReaded == isReaded.Value);
            }
          return  Find(where);
        }
        public UserNotice FindOneNoticeToUser(string userId, M.Notice notice)
        {
            var userNotice = FindOne(x => x.Notice == notice && x.UserId == new Guid(userId));
            if (userNotice == null)
            {
                string errMsg= "没有找到对应的用户通知";
                Log.Error(errMsg);
                throw new Exception(errMsg);

            }
            return userNotice;
           

        }
    }
}