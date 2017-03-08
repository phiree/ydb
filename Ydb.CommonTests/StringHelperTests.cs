using NUnit.Framework;
using Ydb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.CommonTests
{
    [TestFixture()]
    public class StringHelperTests
    {
        [Test()]
        public void StringHelper_CheckDateTime_Test()
        {
            DateTime dt = StringHelper.CheckDateTime("", "", "", false);
            Assert.AreEqual(DateTime.MinValue, dt);
            DateTime dtbase = DateTime.Now;
            dt = StringHelper.CheckDateTime(dtbase.ToString("yyyyMMdd"), "yyyyMMdd", "时间", false);
            Assert.AreEqual(DateTime.Parse(dtbase.ToString("yyyy-MM-dd")), dt);
            try
            {
                dt = StringHelper.CheckDateTime(dtbase.ToString("yyyyMMdd"), "yyyy-MM-dd", "时间", false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("时间格式有误", ex.Message);
            }

            dt = StringHelper.CheckDateTime(dtbase.ToString("yyyyMM"), "yyyyMM", "时间", false);
            Assert.AreEqual(DateTime.Parse(dtbase.ToString("yyyy-MM")+"-01"), dt);
            try
            {
                dt = StringHelper.CheckDateTime(dtbase.ToString("yyyyMM"), "yyyy-MM-dd", "时间", false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("时间格式有误", ex.Message);
            }

            dt = StringHelper.CheckDateTime(dtbase.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", "时间", false);
            Assert.AreEqual(DateTime.Parse(dtbase.ToString("yyyy-MM-dd HH:mm:ss")), dt);
            try
            {
                dt = StringHelper.CheckDateTime(dtbase.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd", "时间", false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("时间格式有误", ex.Message);
            }

            dt = StringHelper.CheckDateTime(dtbase.ToString("yyyy-MM-dd HH:mm:ss fff"), "yyyy-MM-dd HH:mm:ss fff", "时间", false);
            Assert.AreEqual(DateTime.Parse(dtbase.ToString("yyyy-MM-dd HH:mm:ss.fff")), dt);
            try
            {
                dt = StringHelper.CheckDateTime(dtbase.ToString("yyyy-MM-dd HH:mm:ss fff"), "yyyy-MM-dd", "时间", false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("时间格式有误", ex.Message);
            }


            dt = StringHelper.CheckDateTime(dtbase.ToString("yyyy-MM-dd"), "yyyy-MM-dd", "时间", true);
            Assert.AreEqual(DateTime.Parse(dtbase.ToString("yyyy-MM-dd")).AddDays(1), dt);
           
        }
    }
}