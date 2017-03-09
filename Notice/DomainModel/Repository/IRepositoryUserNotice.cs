using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Notice.DomainModel.Repository
{
    public interface IRepositoryUserNotice: Ydb.Common.Repository.IRepository<UserNotice, Guid>
    {
        void AddNoticeToUser(DomainModel.Notice notice, string userId);
        IList<UserNotice> FindNoticeToUser(string userId, bool? isReaded);
        UserNotice FindOneNoticeToUser(string userId, Notice notice);
    }
}