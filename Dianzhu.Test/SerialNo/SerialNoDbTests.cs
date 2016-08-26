using NUnit.Framework;
using JSYK.Infrastructure.SerialNo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL.Common;
namespace JSYK.Infrastructure.SerialNo.Tests
{
    [TestFixture()]
    public class SerialNoDbTests
    {
        [SetUp]
        public void SetUp()
        {
            PHSuit.Logging.Config("JSYK.Test");
            Bootstrap.Boot();
            NHibernateUnitOfWork.UnitOfWork.Start();
        }
        [Test()]
        public void GetSerialNoTest()
        {

            Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder builder = Bootstrap.Container.Resolve<Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder>();
           string value= builder.GetSerialNo("test");

            Console.WriteLine(value);
            
        }
        [TearDown]
        public void TearDown()
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush(System.Data.IsolationLevel.Chaos);
        }
    }
}