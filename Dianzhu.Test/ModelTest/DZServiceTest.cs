using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.Model;
using FizzWare.NBuilder;
namespace Dianzhu.Test.ModelTest
{
    [TestFixture]
   public class DZServiceTest
    {
        [Test]
        public void GetServiceSnapShot()
        {
            DZService s = Builder<DZService>.CreateNew().
                With(x=>x.Name="s1").
                Build();
            ServiceSnapShotForOrder snapShot = s.GetServiceSnapShot();
            
            Assert.AreEqual(s.CancelCompensation, snapShot.CancelCompensation);
            Assert.AreEqual(s.MinPrice, snapShot.MinPrice);
            Assert.AreEqual(s.OverTimeForCancel, snapShot.OverTimeForCancel);
            Assert.AreEqual(s.ServiceMode, snapShot.ServiceMode);
            Assert.AreEqual(s.Name, snapShot.ServiceName);
            Assert.AreEqual(s.UnitPrice, snapShot.UnitPrice);
            Assert.AreEqual(s.ChargeUnit, snapShot.ChargeUnit);
            Assert.AreEqual(s.DepositAmount, snapShot.DepositAmount);
            Assert.AreEqual(s.Description, snapShot.Description);
            Assert.AreEqual(s.IsCompensationAdvance, snapShot.IsCompensationAdvance);

        }

        [Test]
        public void GetServiceTimeSnapShop()
        {
           

            IList<ServiceOpenTimeForDay> forday = Builder<ServiceOpenTimeForDay>.CreateListOfSize(2)
                .TheFirst(1).With(x=>x.TimeStart="8:00").With(x=>x.TimeEnd="10:00").With(x => x.MaxOrderForOpenTime = 2)
                  .TheNext(1).With(x => x.TimeStart = "12:00").With(x => x.TimeEnd = "14:00").With(x => x.MaxOrderForOpenTime = 3)
                 .Build();
            IList<ServiceOpenTime> day = Builder<ServiceOpenTime>.CreateListOfSize(1)
               .TheFirst(1).With(x => x.DayOfWeek = DayOfWeek.Friday).With(x => x.OpenTimeForDay = forday)

               .Build();

            DZService s = Builder<DZService>.CreateNew()
              .With(x=>x.OpenTimes=day)
              .Build();
            var a = s.GetOpenTimeSnapShot(new DateTime(2016,3,25,9,10,10));
            Assert.AreEqual(new DateTime(2016, 3, 25), a.Date);
            Assert.AreEqual(2, a.MaxOrder);
            Assert.AreEqual(480,a.PeriodBegin);
            Assert.AreEqual(600, a.PeriodEnd);
              a = s.GetOpenTimeSnapShot(new DateTime(2016, 3, 26, 9, 10, 10));
            Assert.AreEqual(new DateTime(2016, 3, 25), a.Date);
            Assert.AreEqual(2, a.MaxOrder);
            Assert.AreEqual(480, a.PeriodBegin);
            Assert.AreEqual(600, a.PeriodEnd);
        }
    }
}
