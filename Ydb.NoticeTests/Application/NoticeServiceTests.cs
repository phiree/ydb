using NUnit.Framework;
using Ydb.Notice.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Notice.Application;
using M = Ydb.Notice.DomainModel;
namespace Ydb.Notice.Application.Tests
{
    [TestFixture()]
    public class NoticeServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }
        [Test()]
        public void AddNoticeTest()
        {
            INoticeService noticeService = Bootstrap.Container.Resolve<INoticeService>();

          var notice=  noticeService.AddNotice("noticeTitle", "<html>notice</html>", Guid.NewGuid(), Common.enum_UserType.business | Common.enum_UserType.customer);

           M.Notice getNotice= noticeService.GetOne(notice.Id.ToString());

            Assert.AreEqual("noticeTitle", getNotice.Title);
            Assert.AreEqual("<html>notice</html>", getNotice.Body);
            Assert.AreEqual(3,(int) getNotice.TargetUserType);

        }
    }
}