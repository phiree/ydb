using NUnit.Framework;
using JSYK.Infrastructure.SerialNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL.Common;
using Rhino.Mocks;
namespace JSYK.Infrastructure.SerialNo.Tests
{
    [TestFixture()]
    public class SerialNoDbTests
    {
        [SetUp]
        public void SetUp()
        {
            PHSuit.Logging.Config("JSYK.Test");
            
        }
        [Test()]
        public void GetSerialNoTestWithWhenKeyExisted()
        {
            string testKey = "testkey";
            Dianzhu.Model.SerialNo serialNo = new Dianzhu.Model.SerialNo { SerialKey=testKey, SerialValue=1 };
            IList<Dianzhu.Model.SerialNo> listNo = new List<Dianzhu.Model.SerialNo>() { serialNo };
            var  dalSerialNo = Rhino.Mocks.MockRepository.GenerateMock<Dianzhu.IDAL.IDALSerialNo>();
            dalSerialNo.Stub(x => x.FindBySerialKey(testKey)).Return(listNo);

            SerialNo.SerialNoDb bllSerialNo = new SerialNoDb(dalSerialNo);

            string value = bllSerialNo.GetSerialNo(testKey,5);

            Assert.AreEqual("testkey00002", value);

        }
        [Test()]
        public void GetSerialNoTestWhenKeyNotExisted()
        {
            string testKey = "testkey";
             
            
            var dalSerialNo = Rhino.Mocks.MockRepository.GenerateMock<Dianzhu.IDAL.IDALSerialNo>();
            dalSerialNo.Stub(x => x.FindBySerialKey(testKey)).Return(new List<Dianzhu.Model.SerialNo>());

            SerialNo.SerialNoDb bllSerialNo = new SerialNoDb(dalSerialNo);

            string value = bllSerialNo.GetSerialNo(testKey, 5);

            Assert.AreEqual("testkey00001", value);

        }
        [TearDown]
        public void TearDown()
        {
           
        }

        [Test()]
        public void GetSerialNoTest1()
        {
            Assert.Fail();
        }
    }
}