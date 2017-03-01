using NUnit.Framework;
using Ydb.Membership.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Ydb.Common.Application;
using Ydb.Common.Infrastructure;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.DomainModelTests
{
    [TestFixture()]
    public class DZMembershipDomainServiceTests
    {
        IRepositoryDZMembership repositoryDZMembership;
        IRepositoryUserToken repositoryUserToken;
        IEmailService emailService;
        IEncryptService encryptService;

        ILoginNameDetermine loginNameDetermine;
        [SetUp]
        public void Setup()
        {
            repositoryDZMembership = MockRepository.Mock<IRepositoryDZMembership>();
            repositoryUserToken = MockRepository.Mock<IRepositoryUserToken>();
            emailService = MockRepository.Mock<IEmailService>();
            loginNameDetermine = MockRepository.Mock<ILoginNameDetermine>();
            encryptService = new Ydb.Infrastructure.EncryptService();

        }
        [Test()]
        public void ChangePasswordTest()
        {
            string oldpassword = "oldpassword";
            string newpassword_less_then_6 = "12345";
            string newpassword_equalto_old = oldpassword;
            string newpassword_valid = "newpassword";
            DZMembership member = FizzWare.NBuilder.Builder<DZMembership>
              .CreateNew()
             .With(x => x.Password = "oldpassword")
              .Build();
            ActionResult result = member.ChangePassword(oldpassword, newpassword_less_then_6, newpassword_less_then_6);

            Assert.AreEqual(false, result.IsSuccess);

            result = member.ChangePassword(oldpassword, newpassword_equalto_old, newpassword_equalto_old);
            Assert.AreEqual(false, result.IsSuccess);

            result = member.ChangePassword(oldpassword, newpassword_valid, newpassword_valid);

            Assert.AreEqual(true, result.IsSuccess);

        }

        [Test()]
        public void ValidateUserTestWithCorrectPair()
        {
            string username = "user", password = "password";
            DZMembership membership = Builder<DZMembership>.CreateNew().With(x => x.UserName = username).With(x => x.Password = password).Build();
            repositoryDZMembership.Stub(x => x.ValidateUser(username, "5f4dcc3b5aa765d61d8327deb882cf99")).Return(membership);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            string errMsg;
            DZMembership m = mds.ValidateUser(username, password, false, out errMsg);

            Assert.AreEqual(membership, m);
        }
        [Test]
        public void ValidateUserTestWithWrongPair()
        {
            string username = "user", password = "password1";
            DZMembership membership = Builder<DZMembership>.CreateNew().Build();
            repositoryDZMembership.Stub(x => x.ValidateUser(username, "25f4dcc3b5aa765d61d8327deb882cf99")).Return(membership);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            string errMsg;
            DZMembership m = mds.ValidateUser(username, password, false, out errMsg);
            Assert.AreEqual(null, m);
        }

        [Test()]
        public void DZMembershipDomainService_GetDZMembershipCustomerServiceByName_NotException_Test()
        {
            string username = "username";
            DZMembershipCustomerService membership = Builder<DZMembershipCustomerService>.CreateNew().Build();
            repositoryDZMembership.Stub(x => x.GetMemberByName(username)).Return(membership);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            DZMembershipCustomerService m = mds.GetDZMembershipCustomerServiceByName(username);
            Assert.AreEqual(membership, m);
        }
        [Test()]
        public void DZMembershipDomainService_GetDZMembershipCustomerServiceByName_MemberNotExists_Test()
        {
            string username = "username";
            DZMembershipCustomerService membership = Builder<DZMembershipCustomerService>.CreateNew().Build();
            repositoryDZMembership.Stub(x => x.GetMemberByName(username)).Return(null);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            DZMembershipCustomerService m = null;
            try
            {
                m = mds.GetDZMembershipCustomerServiceByName(username);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsNull(m);
                Assert.AreEqual("该助理不存在", ex.Message);
            }
        }

        [Test()]
        public void DZMembershipDomainService_GetDZMembershipCustomerServiceById_NotException_Test()
        {
            Guid memberId = Guid.NewGuid();
            DZMembershipCustomerService membership = Builder<DZMembershipCustomerService>.CreateNew().With(x => x.Id = memberId).Build();
            repositoryDZMembership.Stub(x => x.GetMemberById(memberId)).Return(membership);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            DZMembershipCustomerService m = mds.GetDZMembershipCustomerServiceById(memberId.ToString());
            Assert.AreEqual(membership, m);
        }
        [Test()]
        public void DZMembershipDomainService_GetDZMembershipCustomerServiceById_MemberNotExists_Test()
        {
            Guid memberId = Guid.NewGuid();
            DZMembershipCustomerService membership = Builder<DZMembershipCustomerService>.CreateNew().With(x => x.Id = memberId).Build();
            repositoryDZMembership.Stub(x => x.GetMemberById(memberId)).Return(null);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            DZMembershipCustomerService m = null;
            try
            {
                m = mds.GetDZMembershipCustomerServiceById(memberId.ToString());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsNull(m);
                Assert.AreEqual("该助理不存在", ex.Message);
            }
        }

        [Test()]
        public void DZMembershipDomainService_GetDZMembershipCustomerServiceByArea_Test()
        {
            IList<DZMembership> memberList = new List<DZMembership> {
                new DZMembership() { AreaId = "1", UserType = UserType.customerservice, UserName = "UserName0" },
                new DZMembershipCustomerService() { AreaId = "1", UserType = UserType.customerservice, UserName = "UserName1" },
            };
            IList<string> areaList = new List<string> { "1", "2" };
            repositoryDZMembership.Stub(x => x.GetUsersByArea(areaList,DateTime.MinValue,DateTime.MinValue,UserType.customerservice)).Return(memberList);
            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, encryptService);
            IList<DZMembershipCustomerService> memberCustomerServiceList = mds.GetDZMembershipCustomerServiceByArea(areaList);
            Assert.AreEqual(1, memberCustomerServiceList.Count);
            Assert.AreEqual("1", memberCustomerServiceList[0].AreaId);
            Assert.AreEqual("UserName1", memberCustomerServiceList[0].UserName);
        }


    }
}