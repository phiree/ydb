﻿using NUnit.Framework;
using Ydb.BusinessResource.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.Application.Tests;
using Ydb.Common.Application;
using Ydb.Common.Domain;

namespace Ydb.BusinessResource.DomainModel.Tests
{
    [TestFixture()]
    public class DZServiceTests
    {
        [Test()]
        public void AddWorkDayTest()
        {
            DZService servie = FizzWare.NBuilder.Builder<DZService>.CreateNew().Build();
            string errmsg;

            Assert.AreEqual(7, servie.OpenTimes.Count);
            bool addSuccess = servie.AddOpenTime(DayOfWeek.Friday, 10, out errmsg);
            Assert.IsFalse(addSuccess);

        }

        [Test()]
        public void AddWorkTimeTestSuccess()
        {
            DZService servie = FizzWare.NBuilder.Builder<DZService>.CreateNew().Build();
            string errmsg;
 
             var worktime=   servie.AddWorkTime(DayOfWeek.Friday,"12:01","12:10", 10, "test_tag");

            Assert.AreEqual(12, worktime.TimePeriod.StartTime.Hour);
            Assert.AreEqual(10, worktime.TimePeriod.EndTime.Minute);
            Assert.AreEqual(3, servie.OpenTimes[5].OpenTimeForDay.Count);

            var worktime2 = servie.AddWorkTime(DayOfWeek.Friday, "01:12", "03:03", 10, "test_tag");
            
            Assert.AreEqual(4, servie.OpenTimes[5].OpenTimeForDay.Count);
            Assert.AreEqual(12, servie.OpenTimes[5].OpenTimeForDay[3].TimePeriod.StartTime.Minute);
        }

        [Test]
        public void ModifyWorkTimeTestSuccess()
       
        {
            DZService service = Builder<DZService>.CreateNew().Build();
        
            service.AddWorkTime(DayOfWeek.Sunday, "01:01", "02:02", 13, "test_tag");

            string errMsg;
            service.ModifyWorkTime(DayOfWeek.Sunday,13,new TimePeriod(new Time("01:01"),new Time("02:02")),
               new TimePeriod(new Time("01:01"), new Time("03:03")),out errMsg
                );

         }
        [Test]
        public void ModifyWorkTimeTestConflict()

        {
            DZService service = Builder<DZService>.CreateNew().Build();

            service.AddWorkTime(DayOfWeek.Sunday, "01:01", "02:02", 13, "test_tag");

            string errMsg;
            service.ModifyWorkTime(DayOfWeek.Sunday, 13, new TimePeriod(new Time("01:01"), new Time("02:02")),
               new TimePeriod(new Time("09:01"), new Time("11:03")), out errMsg
                );
            Console.WriteLine("error"+errMsg);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));
            

        }
    }
}