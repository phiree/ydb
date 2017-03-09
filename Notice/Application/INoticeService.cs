using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.Common.Application;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Application
{
    public interface INoticeService
    {
        /// <summary>
        /// 新建一个通知.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="htmlBody"></param>
        /// <param name="authorId"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        M.Notice AddNotice(string title, string htmlBody, Guid authorId, enum_UserType targetUserType);
        /// <summary>
        /// 获取通知列表
        /// </summary>
        /// <param name="authorId">作者Id</param>
        /// <returns></returns>
        IList<M.Notice> GetNoticeForAuther(Guid authorId);

        void AddNoticeToUser(string noticeId, string userId);
        /// <summary>
        /// 用户获取推送列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readed">是否已读,null表示全部</param>
        /// <returns></returns>
        IList<M.Notice> GetNoticeForUser(Guid userId,  bool? readed);


        /// <summary>
        /// 用户获取一条通知,设置为已读状态.
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        ActionResult UserReadNotice(string userId, string noticeId);


        /// <summary>
        /// 获取一条通知的详情
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        M.Notice GetOne(string noticeId);

        IList<M.Notice> GetAll();
      
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="noticeId">通知ID</param>
        /// <param name="checkerId">审核者Id</param>
        /// <returns>审核结果</returns>
        void CheckPass(string noticeId, string checkerId);

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="checherId">审核者Id</param>
        /// <param name="refuseReason">拒绝原因</param>
        void CheckRefuse(string noticeId, string checherId, string refuseReason);
    }
}