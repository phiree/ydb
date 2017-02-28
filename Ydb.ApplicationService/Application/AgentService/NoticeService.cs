using System;
using System.Collections.Generic;
using System.Linq;
using OpenfireExtension;
using Ydb.Common;
using Ydb.Common.Application;
using Ydb.InstantMessage.Application;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Notice.Application;
using Ydb.Push.Application;
using M = Ydb.Notice.DomainModel;

namespace Ydb.ApplicationService.Application.AgentService
{
    public class AgentNoticeService : IAgentNoticeService
    {
        private readonly IInstantMessage imService;
        private readonly IDZMembershipService memberService;
        private readonly INoticeService noticeService;
        private readonly IOpenfireDbService openfireDbService;
        private readonly IPushService pushService;

        public AgentNoticeService(INoticeService noticeService,
            IPushService pushService,
            IDZMembershipService memberService,
            IOpenfireDbService openfireDbService,
            IInstantMessage imService)
        {
            this.noticeService = noticeService;
            this.pushService = pushService;
            this.memberService = memberService;
            this.openfireDbService = openfireDbService;
            this.imService = imService;
        }

        public ActionResult SendNotice(string noticeId)
        {
            return SendNotice(noticeId, false);
        }

        /// <summary>
        ///     发送一条推送
        /// </summary>
        /// <param name="noticeId"></param>
        /// <param name="isDebug">是否沙箱环境(IOS)</param>
        /// <returns></returns>
        public ActionResult SendNotice(string noticeId, bool isDebug)
        {
            var result = new ActionResult();
            var notice = noticeService.GetOne(noticeId);

            //发送系统推送 apns,小米推送
            //membership 根据代理商区域和推送目标 查询推送的用户列表
            var agent = memberService.GetUserById(notice.AuthorId.ToString());
            var areaId = agent.AreaId;
            var targetUserType = notice.TargetUserType;
            var targetUsers = memberService.GetMembershipsByArea(new List<string> { areaId },
                (UserType)(int)targetUserType); //todo: 统一Membership领域内的UserTYpe 和 Common里的enum_UserType

            //push 遍历用户发送推送消息
            foreach (var member in targetUsers)
                pushService.Push(notice.Title, "ReceptionChatNoticeSys", member.Id.ToString(), string.Empty,
                    member.UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    isDebug);
            //instantmessage 发送xmpp推送.
            // 将用户放到组内,根据用户类型分组

            var groupname = noticeId;
            var userIds = string.Join(",", targetUsers.Select(x => x.Id));
            openfireDbService.AddUsersToGroup(userIds, groupname);

            imService.SendMessageText(Guid.NewGuid(), notice.Title, groupname + "@broadcast",
               string.Empty, string.Empty);

            return result;
        }

        public void SetNoticeReaded(string noticeId, string userId)
        {
            //
            throw new NotImplementedException();
        }

        public IList<object> GetAllNotice(string userId)
        {
            //包含已读和未读
            throw new NotImplementedException();
        }
    }
}