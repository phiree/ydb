﻿using NUnit.Framework;
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
             dzService = Rhino.Mocks.MockRepository.GenerateStub<IDZServiceService>();
            

        }
        IView.IViewSearchResult viewSearchResult = Rhino.Mocks.MockRepository.GenerateMock<IView.IViewSearchResult>();
        IView.IViewSearch viewSearch= Rhino.Mocks.MockRepository.GenerateMock<IViewSearch>();

        IView.IViewTypeSelect viewTypeSelect = Rhino.Mocks.MockRepository.GenerateMock<IViewTypeSelect>();
        [Test()]
        public void ViewSearch_SearchTest()
        {
            IList<DZService> serviceList = Builder<DZService>.CreateListOfSize(10).Build();

            string name = "name";
            double latitude =1, longtitude =2;
            Guid serviceTypeId = Guid.NewGuid();
            decimal minPrice = 1, maxPrice = 2;
            DateTime targetTime = DateTime.Now;
            int total;

            //dzService.SearchService(name, minPrice, maxPrice, servieTypeId, targetTime, double.Parse(lng), double.Parse(lat), 0, 999, out total);
            dzService.Stub(x => x.SearchService(name, minPrice, maxPrice, serviceTypeId, targetTime, longtitude, latitude, 0, 000, out total)).Return(serviceList);
          
            PSearch pSearch = new PSearch(null,viewSearch,viewSearchResult,viewTypeSelect, null, null, dzService, null, null, null, null, null, null, null, null, null);
            pSearch.ViewSearch_Search(targetTime, minPrice, maxPrice, serviceTypeId, name, longtitude.ToString(), latitude.ToString());
            Assert.AreEqual(10, pSearch.ViewSearchResult.SearchedService.Count);
            

            Assert.Fail();
        }
    }
}