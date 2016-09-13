using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
namespace Dianzhu.BLL.Tests
{
    [TestFixture()]
    public class BLLPushTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            NHibernateUnitOfWork.UnitOfWork.Start();
        }
        [Test()]
        public void PushTest()
        {
           
        }
        [TearDown]
        public void TearDown()
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
    }
}