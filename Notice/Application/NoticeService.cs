using System;
using System.Collections.Generic;
using Ydb.Common;
using System.Linq;
using Ydb.Common.Application;
using Ydb.Notice.DomainModel.Repository;
using Ydb.Notice.Infrastructure.YdbNHibernate.UnitOfWork;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Application
{
    public class NoticeService : INoticeService
    {
        private readonly IRepositoryNotice repoNotice;
        private readonly IRepositoryUserNotice repoUserNotice;
        public NoticeService(IRepositoryNotice repoNotice, IRepositoryUserNotice repoUserNotice)
        {
            this.repoNotice = repoNotice;
            this.repoUserNotice = repoUserNotice;
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
        [UnitOfWork]
        public IList<M.Notice> GetNoticeForAuther(Guid authorId)
        {
            return repoNotice.Find(x => x.AuthorId == authorId);
        }
        [UnitOfWork]
        public void CheckPass(string noticeId, string checkerId)
        {
            M.Notice notice = repoNotice.FindById(new Guid(noticeId));
            notice.SetApproved(new Guid(checkerId));

        }
        [UnitOfWork]
        public void AddNoticeToUser(string noticeId, string userId)
        {
            M.Notice notice = repoNotice.FindById(new Guid(noticeId));
            var existed = repoUserNotice.FindOneNoticeToUser(userId, notice);
            if (null==existed)
            {
                repoUserNotice.AddNoticeToUser(notice, userId);
            }

        }
        [UnitOfWork]
        public void CheckRefuse(string noticeId, string checherId, string refuseReason)
        {
            M.Notice notice = repoNotice.FindById(new Guid(noticeId));
            notice.SetRefused(new Guid(checherId), refuseReason);
        }

        public IList<M.Notice> GetNoticeForUser(Guid userId, bool? readed)
        {
            IList<DomainModel.UserNotice> userNotices = repoUserNotice.FindNoticeToUser(userId.ToString(), readed);
            return userNotices.Select(x => x.Notice).ToList();
        }

        public M.Notice GetOne(string noticeId)
        {
            return repoNotice.FindById(new Guid(noticeId));
        }
        [UnitOfWork]
        public ActionResult UserReadNotice(string userId, string noticeId)
        {
            ActionResult result = new ActionResult();
            try
            {
                M.Notice notice = repoNotice.FindById(new Guid(noticeId));
                M.UserNotice userNotice = repoUserNotice.FindOneNoticeToUser(userId, notice);
                userNotice.UserReaded();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = ex.ToString();
            }
            return result;

        }

        public IList<M.Notice> GetAll()
        {
            return repoNotice.Find(x => true);
        }
    }
}