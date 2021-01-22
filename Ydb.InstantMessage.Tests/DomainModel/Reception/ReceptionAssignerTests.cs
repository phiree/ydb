using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage;
using FizzWare.NBuilder;
using Rhino.Mocks;

namespace Ydb.InstantMessage.DomainModel.Reception.Tests
{


    [TestFixture()]
    public class ReceptionAssignerTests
    {

       

        #region AssignCustomerLogin
        /*匹配列表中有一个:{在线,离线}
         匹配列表中有多个;
         没有匹配:{只有点点:还有其他客服} */

        /// <summary>
        /// 匹配列表中一条数据，匹配客服在线
        /// </summary>
        [Test]
        public void AssignCustomerLogin_OneExistedReception_CSOnline()
        {
       
            string customerId = Guid.NewGuid().ToString();
            string customerServiceId = Guid.NewGuid().ToString();
            string areaId = "a1";
            MemberArea onlineCS = new MemberArea(customerServiceId, areaId);
            MemberArea customerToAssign = new MemberArea(customerId, areaId);
            ReceptionStatus rs = Builder<ReceptionStatus>.CreateNew()
                .With(x => x.CustomerId = customerId)
                .With(x => x.CustomerServiceId = customerServiceId)
                .With(x=>x.AreaCode=areaId)
                .Build();
            IList<ReceptionStatus> existedReception = new List<ReceptionStatus> { rs };

         
            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
            iSession.Stub(x => x.IsUserOnline(customerServiceId)).Return(true);


            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));
            string assignedCustomerServiceId = Assigner.AssignCustomerLogin(existedReception, customerToAssign, new List<MemberArea> { onlineCS });
            Console.WriteLine(assignedCustomerServiceId);
            Assert.AreEqual(customerServiceId, assignedCustomerServiceId);
        }
        /// <summary>
        /// 匹配列表中一条数据，匹配客服不在线
        /// </summary>
        [Test]
        public void AssignCustomerLogin_OneExistedReception_CSOffline()
        {
            string customerId = Guid.NewGuid().ToString();
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            string areaId = "a1";
            var onlineCS =new List<MemberArea> { new MemberArea(customerServiceId, areaId) };
            MemberArea customerToAssign = new MemberArea(customerId, areaId);
            ReceptionStatus rs = Builder<ReceptionStatus>.CreateNew()
                .With(x => x.CustomerId = customerId)
                .With(x => x.CustomerServiceId = customerServiceId)

                .Build();
            IList<ReceptionStatus> existedReception = new List<ReceptionStatus> { rs };

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
           
            iSession.Stub(x => x.IsUserOnline(diandianId)).Return(true);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            string assignedCustomerServiceId = Assigner.AssignCustomerLogin(existedReception, customerToAssign, new List<MemberArea>());
            Console.WriteLine(assignedCustomerServiceId);
            Assert.AreEqual(diandianId,assignedCustomerServiceId);
        }
        /// <summary>
        /// 匹配列表有多条数据
        /// </summary>
        [Test]
        public void AssignCustomerLogin_NotExistedReception()
        {
            string areaId = "a1";
            string diandian= Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            string customerId = Guid.NewGuid().ToString();
            ReceptionStatus rs = Builder<ReceptionStatus>.CreateNew()
                .With(x => x.CustomerId = customerId)
                .With(x=>x.AreaCode=areaId)
                .Build();
            IList<ReceptionStatus> existedReception = Builder<ReceptionStatus>.CreateListOfSize(4).Build();
            existedReception.Add(rs);

         //   var onlineCslist = new List<MemberArea> { new MemberArea { },new MemberArea { } };

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
           // iSession.Stub(x => x.IsUserOnline(customerId)).Return(true);
            iSession.Stub(x => x.IsUserOnline(diandian)).Return(true);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            string assignedCustomerServiceId = Assigner.AssignCustomerLogin(existedReception, new MemberArea(customerId,areaId), new List<MemberArea>());
            Console.WriteLine(assignedCustomerServiceId);
            Assert.AreEqual(diandian, assignedCustomerServiceId);
        }

        #endregion

        #region AssignCSLoginTest
        /// <summary>
        /// 匹配给点点的数据，模拟3条数据
        /// </summary>
        [Test()]
        public void AssignCSLoginTest_ExistedReception_Three()
        {
            string areaId = "area1";
            string customerId = Guid.NewGuid().ToString();
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

            IList<ReceptionStatus> existedReception = Builder<ReceptionStatus>.CreateListOfSize(3)
                .TheFirst(3).With(x => x.CustomerServiceId = diandianId)
                .With(x=>x.AreaCode= areaId)
                .Build();

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();




            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            Dictionary<string, string> assignedDic = Assigner.AssignCSLogin(existedReception,new MemberArea( customerServiceId, areaId));
            Console.WriteLine(customerServiceId);
            Assert.AreEqual(3, assignedDic.Count);
            foreach (var item in assignedDic.Values.ToList())
            {
                Console.WriteLine("customerId:" + item);
                Assert.AreEqual(customerServiceId, item);
            }
        }

        #endregion

        #region AssignCSLogoffTest
        /// <summary>
        /// 通过客服查询到的数据列表，模拟有5条数据，3个客服在线，点点在线
        /// </summary>
        [Test()]
        public void AssignCSLogoffTest_existedReceptions_CSOnline_DDOnline()
        {
            string areaId = "area1";
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

            IList<ReceptionStatus> existReceptions = Builder<ReceptionStatus>.CreateListOfSize(5)
                .TheFirst(5).With(x => x.CustomerServiceId == customerServiceId).With(x=>x.AreaCode=areaId) .Build();
           
            var csOnline = new List<MemberArea> {new MemberArea(customerServiceId,areaId),new MemberArea("cs1",areaId),new MemberArea("cs2",areaId) };
            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
         
            iSession.Stub(x => x.IsUserOnline(diandianId)).Return(true);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            Dictionary<string, string> assignList = Assigner.AssignCSLogoff(existReceptions,csOnline);

            Assert.AreEqual(5, assignList.Count);
            foreach (var item in assignList)
            {
                Assert.IsTrue(csOnline.Select(x => x.MemberId).ToList().Contains(item.Value));
                Console.WriteLine(item.Key + "," + item.Value);
            }
        }

        /// <summary>
        /// 通过客服查询到的数据列表，模拟有5条数据，3个客服在线，点点不在线
        /// </summary>
        [Test()]
        public void AssignCSLogoffTest_existedReceptions_CSOnline_DDOffline()
        {
            string areaId = "area1";
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

            IList<ReceptionStatus> existReceptions = Builder<ReceptionStatus>.CreateListOfSize(5)
                .TheFirst(5).With(x => x.CustomerServiceId == customerServiceId)
                .With(x=>x.AreaCode=areaId)
                .Build();
             

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
           
            iSession.Stub(x => x.IsUserOnline(diandianId)).Return(false);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            var csOnline = new List<MemberArea> { new MemberArea(customerServiceId, areaId), new MemberArea("cs1", areaId), new MemberArea("cs2", areaId) };

            Dictionary<string, string> assignList = Assigner.AssignCSLogoff(existReceptions, csOnline);

            Assert.AreEqual(5, assignList.Count);
            foreach (var item in assignList)
            {
                Assert.IsTrue(csOnline.Select(x => x.MemberId).ToList().Contains(item.Value));
                Console.WriteLine(item.Key + "," + item.Value);
            }
        }

        /// <summary>
        /// 通过客服查询到的数据列表，模拟有5条数据，没有客服在线，点点在线
        /// </summary>
        [Test()]
        public void AssignCSLogoffTest_existedReceptions_CSOffline_DDOnline()
        {
            string areaId = "area1";
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

            IList<ReceptionStatus> existReceptions = Builder<ReceptionStatus>.CreateListOfSize(5)
                .TheFirst(5).With(x => x.CustomerServiceId == customerServiceId).Build();

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
            iSession.Stub(x => x.GetOnlineSessionUser(Enums.XmppResource.YDBan_CustomerService)).Return(null);
            iSession.Stub(x => x.IsUserOnline(diandianId)).Return(true);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

            var csOnline = new List<MemberArea> { new MemberArea(customerServiceId, areaId), new MemberArea("cs1", areaId), new MemberArea("cs2", areaId) };

            Dictionary<string, string> assignList = Assigner.AssignCSLogoff(existReceptions, csOnline);

            Assert.AreEqual(5, assignList.Count);
            Assert.AreEqual(1, assignList.Values.Distinct().Count());
            Assert.AreEqual(diandianId, assignList.Values.Distinct().ToList()[0]);
            foreach (var item in assignList)
            {
                Console.WriteLine(item.Key + "," + item.Value);
            }
        }

        /// <summary>
        /// 通过客服查询到的数据列表，模拟有5条数据，没有客服在线，点点不在线
        /// </summary>
        [Test()]
        [ExpectedException(typeof(Exception))]
        public void AssignCSLogoffTest_existedReceptions_CSOffline_DDOffline()
        {
            string customerServiceId = Guid.NewGuid().ToString();
            string diandianId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");

            IList<ReceptionStatus> existReceptions = Builder<ReceptionStatus>.CreateListOfSize(5)
                .TheFirst(5).With(x => x.CustomerServiceId == customerServiceId).Build();

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
           // iSession.Stub(x => x.GetOnlineSessionUser(Enums.XmppResource.YDBan_CustomerService)).Return(null);
            iSession.Stub(x => x.IsUserOnline(diandianId)).Return(false);
            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom(iSession));

           Dictionary<string, string> assignList = Assigner.AssignCSLogoff(existReceptions,new List<MemberArea>());
        }
        #endregion
    }
}