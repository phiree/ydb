using NUnit.Framework;
using Dianzhu.CSClient.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.Application;

using Ydb.BusinessResource.DomainModel;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.Presenter.Tests
{
    [TestFixture()]
    public class PSearchTests
    {
        IDZServiceService dzService;
        [SetUp]
        public void Setup()
        {
             dzService = Rhino.Mocks.MockRepository.Mock<IDZServiceService>();
            

        }
        IView.IViewSearchResult viewSearchResult = Rhino.Mocks.MockRepository.Mock<IView.IViewSearchResult>();
        IView.IViewSearch viewSearch= Rhino.Mocks.MockRepository.Mock<IViewSearch>();

        IView.IViewTypeSelect viewTypeSelect = Rhino.Mocks.MockRepository.Mock<IViewTypeSelect>();
        [Test()]
        public void ViewSearch_SearchTest()
        {
            var business = Builder<Business>.CreateNew().Build();
            IList<DZService> serviceList = Builder<DZService>.CreateListOfSize(10).All().With(x=>x.Business=business) .Build();

            string name = "name";
            double latitude =1, longtitude =2;
            Guid serviceTypeId = Guid.NewGuid();
            decimal minPrice = 1, maxPrice = 2;
            DateTime targetTime = DateTime.Now;
            int total;

            //dzService.SearchService(name, minPrice, maxPrice, servieTypeId, targetTime, double.Parse(lng), double.Parse(lat), 0, 999, out total);
            dzService.Stub(x => x.SearchService(name, minPrice, maxPrice, serviceTypeId, targetTime, longtitude, latitude, 0, 999, out total)).Return(serviceList);
          
            PSearch pSearch = new PSearch(null,viewSearch,viewSearchResult,viewTypeSelect, null, null, dzService, null, null, null, null, null, null, null, null, null);
            pSearch.ViewSearch_Search(targetTime, minPrice, maxPrice, serviceTypeId, name, longtitude.ToString(), latitude.ToString());
            Assert.AreEqual(10, pSearch.SelectedServiceList.Count);

        }
    }
}