using System;
using System.Collections.Generic;
using Ydb.Common;
using Ydb.Notice.DomainModel.Repository;
using Ydb.Notice.Infrastructure.YdbNHibernate.UnitOfWork;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Application
{
    public class NoticeService : INoticeService
    {
        private readonly IRepositoryNotice repoNotice;

        public NoticeService(IRepositoryNotice repoNotice)
        {
            this.repoNotice = repoNotice;
        }

        [UnitOfWork]
        public M.Notice AddNotice(string title, string htmlBody, Guid authorId, enum_UserType targetUserType)
        {
            var notice = new M.Notice
            {
                Title = title,
                Body = htmlBody,
                AuthorId = authorId,
                TargetUserType = targetUserType
            };

            repoNotice.Add(notice);
            return notice;
        }

        public IList<M.Notice> GetNoticeForAuther(Guid authorId)
        {
           return repoNotice.Find(x => x.AuthorId == authorId);
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