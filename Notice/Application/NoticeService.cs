using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using M = Ydb.Notice.DomainModel;
using Ydb.Notice.DomainModel.Repository;

namespace Ydb.Notice.Application
{
    public class NoticeService : INoticeService
    {
        private IRepositoryNotice repoNotice;

        public NoticeService(IRepositoryNotice repoNotice)
        {
            this.repoNotice = repoNotice;
        }

        public M.Notice AddNotice(string title, string htmlBody, Guid authorId, enum_UserType targetUserType)
        {
            M.Notice notice = new DomainModel.Notice { Title = title, Body = htmlBody, AuthorId = authorId, TargetUserType = targetUserType };

            repoNotice.Add(notice);
            return notice;
        }

        public void CheckPass(string noticeId, string checkerId)
        {
            throw new NotImplementedException();
        }

        public void CheckRefuse(string noticeId, string checherId, string refuseReason)
        {
            throw new NotImplementedException();
        }

        public IList<M.UserNotice> GetNoticeForUser(Guid userId, enum_UserType targetUserType, bool? readed)
        {
            throw new NotImplementedException();
        }

        public M.Notice GetOne(string noticeId)
        {
            return repoNotice.FindById(new Guid(noticeId));
        }

        public M.Notice ReadOne(string noticeId)
        {
            throw new NotImplementedException();
        }
    }
}