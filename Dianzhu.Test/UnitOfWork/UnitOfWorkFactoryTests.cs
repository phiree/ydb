using NUnit.Framework;
using NHibernateUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateUnitOfWork.Tests
{
    [TestFixture()]
    public class UnitOfWorkFactoryTests
    {
        [SetUp]
        public void Init()
        {
            //PHSuit.Logging.Config("Dianzhu.Test.NHibernateUnitOfWork.Tests");
            Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
        }
        [Test()]
        public void CreateTest()
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
        }
    }
}