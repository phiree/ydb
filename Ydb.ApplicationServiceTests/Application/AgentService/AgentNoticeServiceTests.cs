using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Ydb.Common;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Notice.Application;
using Ydb.Push.Application;
using M = Ydb.Notice.DomainModel;

namespace Ydb.ApplicationService.Application.AgentService.Tests
{
    [TestFixture]
    public class AgentNoticeServiceTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }

        [Test]
        public void SendNoticeTest()
        {
            //mock 通知创建
            var noticeService = MockRepository.Mock<INoticeService>();

            var noticeId = Guid.NewGuid();
            var notice = new M.Notice
            {
                Title = "title",
                Id = noticeId,
                AuthorId = Guid.NewGuid(),
                Body = "body",
                TargetUserType = enum_UserType.customer
            };
            noticeService.Stub(x => x.GetOne(noticeId.ToString())).Return(notice);
            //mock 代理区域
            var agentDto = new MemberDto { Id = notice.AuthorId, AreaId = "1" };
            var memberService = MockRepository.Mock<IDZMembershipService>();
            memberService.Stub(x => x.GetUserById(agentDto.Id.ToString())).Return(agentDto);
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            IList<MemberDto> users = new List<MemberDto> { new MemberDto { Id = user1Id }, new MemberDto { Id = user2Id } };
            memberService.Stub(x => x.GetMembershipsByArea(new List<string> { "1" }, UserType.customer)).Return(users);

            var pushService = MockRepository.Mock<IPushService>();
            foreach (var m in users)
                pushService.Stub(x => x.Push(notice.Title, "ReceptionChatNoticeSys", m.Id.ToString(), string.Empty
                    , users[0].UserName, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    string.Empty));
            IAgentNoticeService agentNoticeService = new AgentNoticeService(noticeService, pushService, memberService, null,
                null);

            agentNoticeService.SendNotice(notice.Id.ToString());
            Assert.Fail();
        }
    }
}