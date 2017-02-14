using NUnit.Framework;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.Tests;
using Ydb.Membership.DomainModel;

namespace Ydb.Membership.Infrastructure.Repository.NHibernateTests
{
    [TestFixture()]
    public class RepositoryMembershipLoginLogTests
    {
        IRepositoryMembershipLoginLog repositoryMembershipLoginLog;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            repositoryMembershipLoginLog = Bootstrap.Container.Resolve<IRepositoryMembershipLoginLog>();
        }


        [Test()]
        public void RepositoryMembershipLoginLog_GetMembershipLastLoginLog_Test()
        {
            DateTime dt1 = DateTime.Now;
            MembershipLoginLog loginLog = new MembershipLoginLog("1", enumLoginLogType.Login, "", Common.enum_appName.Android_Customer);
            repositoryMembershipLoginLog.Add(loginLog);
            System.Threading.Thread.Sleep(1000);
            DateTime dt2 = DateTime.Now;
            MembershipLoginLog loginLog1 = new MembershipLoginLog("2", enumLoginLogType.Login, "", Common.enum_appName.IOS_Customer);
            repositoryMembershipLoginLog.Add(loginLog1);
            DateTime dt3 = DateTime.Now;
            MembershipLoginLog loginLog2 = new MembershipLoginLog("1", enumLoginLogType.Login, "", Common.enum_appName.IOS_Customer);
            repositoryMembershipLoginLog.Add(loginLog2);
            IList<MembershipLoginLog> membershipLoginLogList = repositoryMembershipLoginLog.GetMembershipLastLoginLog();
            Assert.AreEqual(2, membershipLoginLogList.Count);
            Assert.AreEqual(Common.enum_appName.IOS_Customer, membershipLoginLogList[0].AppName);
            Assert.AreEqual(Common.enum_appName.IOS_Customer, membershipLoginLogList[1].AppName);
        }

        [Test()]
        public void RepositoryMembershipLoginLog_GetMembershipLoginLogListByTime_Test()
        {
            DateTime dt1 = DateTime.Now;
            MembershipLoginLog loginLog = new MembershipLoginLog("1",enumLoginLogType.Login,"", Common.enum_appName.Android_Customer);
            repositoryMembershipLoginLog.Add(loginLog);
            System.Threading.Thread.Sleep(1000);
            DateTime dt2 = DateTime.Now;
            MembershipLoginLog loginLog1 = new MembershipLoginLog("2", enumLoginLogType.Login, "", Common.enum_appName.IOS_Customer);
            repositoryMembershipLoginLog.Add(loginLog1);
            IList<MembershipLoginLog> loginLogList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(DateTime.MinValue, DateTime.MinValue);
            Assert.AreEqual(2, loginLogList.Count);
            loginLogList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime( dt1, dt2);
            Assert.AreEqual(1, loginLogList.Count);
            Assert.AreEqual("1", loginLogList[0].MemberId);
            loginLogList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(dt1, DateTime.Now.AddDays(1));
            Assert.AreEqual(2, loginLogList.Count);
            loginLogList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            Assert.AreEqual(0, loginLogList.Count);
        }
    }
}