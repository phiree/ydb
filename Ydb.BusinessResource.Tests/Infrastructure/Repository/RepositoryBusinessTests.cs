using NUnit.Framework;
using Ydb.BusinessResource.Infrastructure.Repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Infrastructure.Repository.NHibernateTests
{
    [TestFixture()]
    public class RepositoryBusinessTests
    {

        IRepositoryBusiness repositoryBusiness;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            repositoryBusiness = Bootstrap.Container.Resolve<IRepositoryBusiness>();
        }
        [Test()]
        public void GetBusinessesByAreaTest()
        {
            Assert.Fail();
        }
    }
}