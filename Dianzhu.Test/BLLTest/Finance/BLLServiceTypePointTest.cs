﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.Model;
using FizzWare.NBuilder;
namespace Dianzhu.Test.BLLTest.Finance
{
    public class BLLServiceTypePointTest
    {
        [Test]
        public void GetPoint()
        {
            IList<ServiceType> level1Type = Builder<ServiceType>.CreateListOfSize(2).All().With(x=>x.Parent= null).Build();
            IList<ServiceType> level2Type = Builder<ServiceType>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.Parent = level1Type[0])
                .TheNext(1).With(x=>x.Parent=level1Type[1])
                .Build();
            IList<ServiceType> level3Type = Builder<ServiceType>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.Parent = level2Type[0])
                .TheNext(1).With(x => x.Parent = level2Type[1])
                .Build();

            IList<Model.Finance.ServiceTypePoint> points = Builder<Model.Finance.ServiceTypePoint>.CreateListOfSize(4)
                .TheFirst(1).With(x => x.ServiceType = level1Type[0]).With(x=>x.Point=0.1m)//lv1 setted
                .TheNext(1).With(x => x.ServiceType = level2Type[0]).With(x => x.Point = 0.2m) //lv2 setted
                 .TheNext(1).With(x => x.ServiceType = level3Type[0]).With(x => x.Point = 0.3m) //lv3 setted
                  
                .Build();



            var dal = MockRepository.GenerateStub<DAL.Finance.DALServiceTypePoint>(string.Empty);
         
         
            BLL.Finance.BLLServiceTypePoint bllPoint = new BLL.Finance.BLLServiceTypePoint(dal);
            dal.Stub(x => x.GetOne(level1Type[0])).Return(points[0]);
           var pointLv3=  bllPoint.GetPoint(level3Type[0]);

            Assert.AreEqual(0.3m, pointLv3);

        }
    }
}
