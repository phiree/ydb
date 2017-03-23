using NUnit.Framework;
using Ydb.InstantMessage.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Tests;
using Ydb.InstantMessage.DomainModel.Reception;
using FizzWare.NBuilder;
using Rhino.Mocks;
using Castle.MicroKernel.Registration;
using Ydb.InstantMessage.DomainModel.Enums;

namespace Ydb.InstantMessage.Application.Tests
{
    [TestFixture()]
    public class ReceptionServiceTests
    {
         
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
           // Bootstrap.Container.Register(Component.For<IReceptionSession>().ImplementedBy<TestReceptionSession>());
           
         
        }


        /// <summary>
        /// 用户登录分配
        /// 没有地区匹配的历史接待记录
        ///     多个地区匹配的在线客服
        ///     一个地区匹配的在线客服
        /// 有地区匹配的历史接待记录
        ///     多个匹配
        ///     多个匹配的在线客服
        /// </summary>
        [Test()]
        public void AssignCustomerLoginTest_NoReceptionStatus_OnlineCs()
        { var receptionService = Bootstrap.Container.Resolve<IReceptionService>();
           
            var customerId = Guid.NewGuid().ToString();
            var customerAreaId = "area1";
            string errorMessage = string.Empty;
            //在线客服
            var csList = new List<MemberArea> { new MemberArea("cs1", customerAreaId), new MemberArea("cs2", customerAreaId) };
            int assignto1 = 0, assignto2 = 0;
            for (int i = 0; i < 300; i++)
            {
                var rs = receptionService.AssignCustomerLogin(customerId, customerAreaId, out errorMessage, csList);
                if (rs.CustomerServiceId == "cs1") assignto1++;
                else if (rs.CustomerServiceId == "cs2") assignto2++;

            }
            decimal r = (decimal)assignto1 / assignto2;
            Console.WriteLine(assignto1 + "," + assignto2);
            Assert.IsTrue(r > 0.8m && r < 1.3m);
        }
        [Test()]
        public void AssignCustomerLoginTest_NoReceptionStatus_OnlineCs_OneWithSameArea()
        {
            var receptionService = Bootstrap.Container.Resolve<IReceptionService>();
            var customerId = Guid.NewGuid().ToString();
            var customerAreaId = "area1";
            string errorMessage = string.Empty;
            //在线客服
            var csList = new List<MemberArea> { new MemberArea("cs1", customerAreaId), new MemberArea("cs2", "area2") };
            int assignto1 = 0, assignto2 = 0;
            for (int i = 0; i < 10; i++)
            {
                var rs = receptionService.AssignCustomerLogin(customerId, customerAreaId, out errorMessage, csList);
                if (rs.CustomerServiceId == "cs1") assignto1++;
                else if (rs.CustomerServiceId == "cs2") assignto2++;

            }
            Console.WriteLine(assignto1 + "," + assignto2);
            Assert.IsTrue(assignto1 == 10);
        }
        [Test()]
        public void AssignCustomerLoginTest_ReceptionStatus_OnlineCs()
        {
            Bootstrap.Container.Register(Component.For<IReceptionSession>().ImplementedBy<TestReceptionSession>());
            var repoRs = Bootstrap.Container.Resolve<IRepositoryReception>();
            var receptionService = Bootstrap.Container.Resolve<IReceptionService>();

            var customerId = Guid.NewGuid().ToString();
            var csIdInRs = Guid.NewGuid().ToString();
            var areaId = "area1";
            //需要模拟在线...
            //历史接待记录
            var res = new ReceptionStatus(customerId, csIdInRs, string.Empty, areaId);
            repoRs.Add(res);

            //在线客服
            var csList = new List<MemberArea> { new MemberArea("cs1", "area1"), new MemberArea("cs2", "area1") };

            string errorMessage = string.Empty;

            int assigntohis = 0, assigntoonline1 = 0, assigntoonline2 = 0;
            int total = 100;
            for (int i = 0; i < total; i++)
            {
                var rs = receptionService.AssignCustomerLogin(customerId, areaId, out errorMessage, csList);
                if (rs.CustomerServiceId == "cs1") assigntoonline1++;
                else if (rs.CustomerServiceId == "cs2") assigntoonline2++;
                else if (rs.CustomerServiceId == csIdInRs) assigntohis++;


            }

            Assert.AreEqual(100, assigntohis);
            Assert.AreEqual(0, assigntoonline1);
            Assert.AreEqual(0, assigntoonline2);

            Console.WriteLine(assigntoonline1 + "," + assigntoonline2);


        }


        string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

        [Test()]
        public void AssignCSLoginTest()
        {
            var repoRs = Bootstrap.Container.Resolve<IRepositoryReception>();
           
            var receptionService = Bootstrap.Container.Resolve<IReceptionService>();
            string csId = Guid.NewGuid().ToString();
            string areaId = "areaId";
            var rsList = Builder<ReceptionStatus>.CreateListOfSize(5)
                     .TheFirst(2).With(x => x.AreaCode = areaId)
                     .All().With(x => x.CustomerServiceId = diandianId)
                     .Build();
            ;
            foreach (var rs in rsList)
            {
                repoRs.Add(rs);
            }

            var list = receptionService.AssignCSLogin(csId, areaId, 3);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Customer " + i + ":" + list[i].CustomerId);
            }
            Assert.AreEqual(2, list.Count);
        }

        [Test()]
        public void AssignCSLogoffTest_HasSameAreaOnlineCs()
        {
            var repoRs = Bootstrap.Container.Resolve<IRepositoryReception>();
            var receptionService = Bootstrap.Container.Resolve<IReceptionService>();
            string areaId = "a1";
            string csId = Guid.NewGuid().ToString();
            IList<MemberArea> onlineCsList = new List<MemberArea> {
          new MemberArea("cs1", areaId),
           new MemberArea("cs2", areaId)
        };
            ReceptionStatus rs = new ReceptionStatus("customer1", csId, "", areaId);
            repoRs.Add(rs);
            receptionService.AssignCSLogoff(csId,onlineCsList);

          var reassignRs=   repoRs.FindByCustomerId("customer1");

            Assert.IsTrue(reassignRs.Count == 1);
            Assert.IsTrue(onlineCsList.Select(x => x.MemberId).Any(y=>y==reassignRs[0].CustomerServiceId));

        }
        [Test()]
        public void AssignCSLogoffTest_NoSameAreaOnlineCs()
        {
            Bootstrap.Container.Register(Component.For<IReceptionSession>().ImplementedBy<TestReceptionSession>());
            var repoRs = Bootstrap.Container.Resolve<IRepositoryReception>();
            var receptionService = Bootstrap.Container.Resolve<IReceptionService>();


            string areaId = "a1";
            string csId = Guid.NewGuid().ToString();
            IList<MemberArea> onlineCsList = new List<MemberArea> {
          new MemberArea("cs1", "a2"),
           new MemberArea("cs2", "a3")
        };
            ReceptionStatus rs = new ReceptionStatus("customer1", csId, "", areaId);
            repoRs.Add(rs);
            receptionService.AssignCSLogoff(csId, onlineCsList);

            var reassignRs = repoRs.FindByCustomerId("customer1");

            Assert.IsTrue(reassignRs.Count == 1);
            Assert.AreEqual(diandianId, reassignRs[0].CustomerServiceId);
        //    Assert.IsTrue(onlineCsList.Select(x => x.MemberId).Any(y => y == reassignRs[0].CustomerServiceId));

        }
        
    }

    public class TestReceptionSession : IReceptionSession
    {
        public IList<OnlineUserSession> GetOnlineSessionUser(XmppResource xmppResource)
        {
            throw new NotImplementedException();
        }

        public bool IsUserOnline(string userId)
        {
            return true;
        }
    }
}