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
        public void RepositoryBusiness_GetBusinessesByArea_Test()
        {
            Business business = new Business() { AreaBelongTo = "1" };
            repositoryBusiness.Add(business);
            Business business1 = new Business() { AreaBelongTo = "1", CreatedTime = DateTime.Now.AddDays(-1) };
            repositoryBusiness.Add(business1);
            IList<string> areaList = new List<string> { "1", "2" };
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue);
            Assert.AreEqual(2, businessList.Count);
            Assert.AreEqual("1", businessList[0].AreaBelongTo);
            Assert.AreEqual("1", businessList[1].AreaBelongTo);
            businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.Now.AddHours(-23), DateTime.Now.AddDays(1));
            Assert.AreEqual(1, businessList.Count);
            Assert.AreEqual("1", businessList[0].AreaBelongTo);
            businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            Assert.AreEqual(2, businessList.Count);
            Assert.AreEqual("1", businessList[0].AreaBelongTo);
            Assert.AreEqual("1", businessList[1].AreaBelongTo);
            businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            Assert.AreEqual(0, businessList.Count);
        }

        [Test()]
        public void RepositoryBusiness_GetBusinessesCountByArea_Test()
        {
            Business business = new Business() { AreaBelongTo = "1" };
            repositoryBusiness.Add(business);
            Business business1 = new Business() { AreaBelongTo = "1", CreatedTime = DateTime.Now.AddDays(-1) };
            repositoryBusiness.Add(business1);
            IList<string> areaList = new List<string> { "1", "2" };
            long c = repositoryBusiness.GetBusinessesCountByArea(areaList, DateTime.MinValue, DateTime.MinValue);
            Assert.AreEqual(2,c);
            c = repositoryBusiness.GetBusinessesCountByArea(areaList, DateTime.Now.AddHours(-23), DateTime.Now.AddDays(1));
            Assert.AreEqual(1, c);
            c = repositoryBusiness.GetBusinessesCountByArea(areaList, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            Assert.AreEqual(2, c);
            c = repositoryBusiness.GetBusinessesCountByArea(areaList, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            Assert.AreEqual(0, c);
        }
    }
}