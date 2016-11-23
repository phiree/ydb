using NUnit.Framework;
using Ydb.BusinessResource.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
namespace Ydb.BusinessResource.DomainModel.Tests
{
    [TestFixture()]
    public class TimePeriodListTests
    {
        PeriodValidator periodLink;
        [Test()]
        [ExpectedException()]
        public void TimePeriodList_Constructor_ConflictTest()
        {
            TimePeriod p1 = new TimePeriod(new Time("1:00"), new Time("4:00"));
            TimePeriod p2 = new TimePeriod(new Time("1:00"), new Time("4:00"));
            periodLink = new PeriodValidator(new List<TimePeriod> { p1, p2 });



        }
        [Test()]

        public void TimePeriodList_Constructor_OKTest()
        {
            TimePeriod p1 = new TimePeriod(new Time("1:00"), new Time("4:00"));
            TimePeriod p2 = new TimePeriod(new Time("4:01"), new Time("7:00"));
            periodLink = new PeriodValidator(new List<TimePeriod> { p1, p2 });
            Assert.AreEqual(2, periodLink.Count);




        }
        [Test()]
        public void TimePeriodList_Constructor_IncludeBeginTest()
        {
            TimePeriod p1 = new TimePeriod(new Time("0:00"), new Time("4:00"));
            TimePeriod p2 = new TimePeriod(new Time("4:01"), new Time("7:00"));
            periodLink = new PeriodValidator(new List<TimePeriod> { p1, p2 });
            Assert.AreEqual(2, periodLink.Count);
        }
        [Test()]
        public void TimePeriodList_Constructor_IncludeBegin_AddBegin_Test()
        {
            TimePeriod p1 = new TimePeriod(new Time("0:00"), new Time("4:00"));
            periodLink = new PeriodValidator(new List<TimePeriod>());
            periodLink.Add(p1);
            Assert.AreEqual(1, periodLink.Count);

        }
        [Test]

        public void IsConflictTest_AddConfilict()
        {
            periodLink = new PeriodValidator(new List<TimePeriod> { new TimePeriod(new Time("08:00"), new Time("12:00")) });
            var worktime = new TimePeriod(new Time("09:00"), new Time("10:00"));
            var isConfilict = periodLink.IsConflict(worktime);
            Assert.AreEqual(true, isConfilict);
        }

        [Test()]
        public void CanModifyTest_Conflict()
        {
            var oldP = new TimePeriod(new Time("08:00"), new Time("12:00")); 
            periodLink = new PeriodValidator(
                new List<TimePeriod> { oldP,
                                       new TimePeriod(new Time("14:00"), new Time("16:00"))}
                );
            var newP = new TimePeriod(new Time("09:00"), new Time("15:00"));
            string errMsg;

            Assert.IsFalse(periodLink.CanModify(oldP, newP, out errMsg));
        }
        [Test()]
        public void CanModifyTest_NotConflict()
        {
            var p1 = new TimePeriod(new Time("08:00"), new Time("12:00"));
            var p2 = new TimePeriod(new Time("14:00"), new Time("18:00"));
            periodLink = new PeriodValidator(
                new List<TimePeriod> { p1,
                                       p2}
                );
            var newP = new TimePeriod(new Time("13:00"), new Time("15:00"));
            string errMsg;

            Assert.IsTrue(periodLink.CanModify(p2, newP, out errMsg));
        }
    }
}