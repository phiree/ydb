using System;
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
            IList<ServiceType> level3Type = Builder<ServiceType>.CreateListOfSize(3)
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
            dal.Stub(x => x.GetOneByServiceType(level1Type[0])).Return(points[0]);
            dal.Stub(x => x.GetOneByServiceType(level2Type[0])).Return(points[1]);
            dal.Stub(x => x.GetOneByServiceType(level3Type[0])).Return(points[2]);
            var pointLv3=  bllPoint.GetPoint(level3Type[0]);
            var pointLv2 = bllPoint.GetPoint(level2Type[0]);
            Assert.AreEqual(0.3m, pointLv3);
            Assert.AreEqual(0.2m, pointLv2);

        }
        [Test]
        
        public void GetPointRealData()
        {
            
            Assert.AreEqual(0.1m, GetPoint("a34e88e3-f858-48f7-b79e-2a49bd022c06"));
            Assert.AreEqual(0.2m, GetPoint("62fc0c8f-6143-48c7-92c0-d3c26e1bd32b"));
           Assert.AreEqual(0.3m, GetPoint("65ebe9b3-a328-4009-a5a9-b63f1c57fe1c"));

            Assert.AreEqual(0.1m, GetPoint("9cbb5c45-78e1-4964-a9fa-88934256c11c"));
           
            Assert.AreEqual(0.1m, GetPoint("583bcc15-3882-489c-8605-e2ac9372f935"));
        }

        private decimal GetPoint(string typeid)
        {
            BLL.Finance.IBLLServiceTypePoint bllPoint = Bootstrap.Container.Resolve<BLL.Finance.IBLLServiceTypePoint>();
            BLL.BLLServiceType bllType = Bootstrap.Container.Resolve < BLL.BLLServiceType>();
            ServiceType type = bllType.GetOne(new Guid(typeid));

            decimal point = bllPoint.GetPoint(type);
            return point;
        }
    }
}
