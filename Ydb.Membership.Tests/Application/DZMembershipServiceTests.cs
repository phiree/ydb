using NUnit.Framework;
using Ydb.Membership.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Tests;
using System.Transactions;

namespace Ydb.Membership.Application.Tests
{
    [TestFixture()]
    public class DZMembershipServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            AutoMapperConfiguration.Configure();
            dzmembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();

        }
        IDZMembershipService dzmembershipService;
        string userName = "132909201331";
        [Test()]

        public void RegisterBusinessUserTest()
        {

            
           
           Dto.RegisterResult registerResult= dzmembershipService.RegisterBusinessUser(userName,"123456", "123456","http://localhost/");
            Assert.AreEqual(string.Empty, registerResult.RegisterErrMsg);
            Assert.AreEqual(true, registerResult.RegisterSuccess);  // your test code here
            Dto.MemberDto memberDto = dzmembershipService.GetUserByName(userName);
            Assert.AreEqual(userName, memberDto.UserName);


        }
       
         
    }
}