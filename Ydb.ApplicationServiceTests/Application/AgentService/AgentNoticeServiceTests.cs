﻿using System.Collections.Generic;
using NUnit.Framework;
using Ydb.Common;
using Ydb.InstantMessage.Application;
using Ydb.Membership.Application;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Notice.Application;
using Ydb.Push.Application;
using Ydb.Push.DomainModel;
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
            //一些id

            var usernames = new[] {"user1", "user2"};
            var apptokens = new[]
            {
                "4eb8954fda72c9bb8d0c0f5f85d99ac0b324c84836806c53d0fe43352f047aa1" //QQ登录 CAT Debug证书
            };
            var areaId = "area1";
            var noticeService = Bootstrap.Container.Resolve<INoticeService>();
            var memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

            //注册代理
            var agentDto = memberService.RegisterMember("agent1", "aaaaaa", "aaaaaa", "agent", string.Empty);
            var agentId = agentDto.ResultObject.Id;
            memberService.UpdateArea(agentDto.ResultObject.Id.ToString(), areaId);

            //新建一条通知
            var notice = noticeService.AddNotice("title", "<h1>notice body</h1>", agentId,
                enum_UserType.customer);
            //注册用户
            foreach (var userName in usernames)
            {
                var memberDto = memberService.RegisterMember(userName, "aaaaaa", "aaaaaa", "customer", string.Empty);
                memberService.UpdateArea(memberDto.ResultObject.Id.ToString(), areaId);
            }
            var users = memberService.GetMembershipsByArea(new List<string> {areaId}, UserType.customer);

            //为用户绑定devicetoken
            var deviceBindService = Bootstrap.Container.Resolve<IDeviceBindService>();

            deviceBindService.Save(new DeviceBind
            {
                AppName = "ios",
                AppToken = apptokens[0],
                IsBinding = true,
                DZMembershipId = users[0].Id.ToString()
            });
            //登录openfire,准备发送消息

            var im = Bootstrap.Container.Resolve<IInstantMessage>();

            //= new Dianzhu.CSClient.XMPP.XMPP(server, domain,adapter, Dianzhu.enum_XmppResource.YDBan_IMServer.ToString());
            //login in
            var noticesenderId = agentId.ToString();

            var noticesenderPwd = "aaaaaa";

            im.OpenConnection(noticesenderId, noticesenderPwd, "YDBan_Agent");

            var agentNoticeService = Bootstrap.Container.Resolve<IAgentNoticeService>();

            agentNoticeService.SendNotice(notice.Id.ToString(), true);
            Assert.Fail();
        }
    }
}