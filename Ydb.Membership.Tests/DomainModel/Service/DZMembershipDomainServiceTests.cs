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
namespace Ydb.Membership.DomainModel.Tests
{
    [TestFixture()]
    public class DZMembershipDomainServiceTests
    {
        IRepositoryDZMembership repositoryDZMembership;
        IRepositoryUserToken repositoryUserToken;
        IEmailService emailService;

        ILoginNameDetermine loginNameDetermine;
        [SetUp]
        public void Setup()
        {
            repositoryDZMembership = MockRepository.Mock<IRepositoryDZMembership>();
            repositoryUserToken = MockRepository.Mock<IRepositoryUserToken>();
            emailService = MockRepository.Mock<IEmailService>();
            loginNameDetermine = MockRepository.Mock<ILoginNameDetermine>();

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
            DZMembership membership = Builder<DZMembership>.CreateNew().Build();
            repositoryDZMembership.Stub(x => x.ValidateUser(username, "5f4dcc3b5aa765d61d8327deb882cf99")).Return(membership);

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, null);
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

            IDZMembershipDomainService mds = new DZMembershipDomainService(repositoryDZMembership, null, null, null);
            string errMsg;
            DZMembership m = mds.ValidateUser(username, password, false, out errMsg);
            Assert.AreEqual(null, m);
        }
    }
}