using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Application;
using Ydb.Notice.Application;
using M = Ydb.Notice.DomainModel;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common;
using Ydb.Push.Application;
using Ydb.InstantMessage.Application;

namespace Ydb.ApplicationService.Application.AgentService
{
    public class AgentNoticeService : IAgentNoticeService
    {
        private INoticeService noticeService;
        private IPushService pushService;
        private IDZMembershipService memberService;
        private OpenfireExtension.IOpenfireDbService openfireDbService;
        private InstantMessage.Application.IInstantMessage imService;

        public AgentNoticeService(INoticeService noticeService,
          IPushService pushService,
          IDZMembershipService memberService,
          OpenfireExtension.IOpenfireDbService openfireDbService,
          InstantMessage.Application.IInstantMessage imService)
        {
            this.noticeService = noticeService;
            this.pushService = pushService;
            this.memberService = memberService;
            this.openfireDbService = openfireDbService;
            this.imService = imService;
        }

        /// <summary>
        /// 发送一条推送
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public ActionResult SendNotice(string noticeId)
        {
            ActionResult result = new ActionResult();
            M.Notice notice = noticeService.GetOne(noticeId);

            //发送系统推送 apns,小米推送
            //membership 根据代理商区域和推送目标 查询推送的用户列表
            MemberDto agent = memberService.GetUserById(notice.AuthorId.ToString());
            string areaId = agent.AreaId;
            enum_UserType targetUserType = notice.TargetUserType;
            IList<MemberDto> targetUsers = memberService.GetMembershipsByArea(new List<string> { areaId }, (UserType)(int)targetUserType); //todo: 统一Membership领域内的UserTYpe 和 Common里的enum_UserType

            //push 遍历用户发送推送消息
            foreach (var member in targetUsers)
            {
                pushService.Push(notice.Title, "ReceptionChatNoticeSys", member.Id.ToString(), string.Empty, member.UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
            //instantmessage 发送xmpp推送.
            // 将用户放到组内
            string groupname = "agentid_" + agent.Id;
            string userIds = string.Join(",", targetUsers.Select(x => x.Id));
            openfireDbService.AddUsersToGroup(userIds, groupname);
            imService.SendMessageText(Guid.NewGuid(), notice.Title, groupname + "@broadcast." + imService.Server, string.Empty, string.Empty);
            return result;
        }

        public IList<object> GetAllNotice(string userId)
        {
            //包含已读和未读
            throw new NotImplementedException();
        }

        public void SetNoticeReaded(string noticeId, string userId)
        {
            //
            throw new NotImplementedException();
        }
    }
}