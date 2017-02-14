using NUnit.Framework;
using Ydb.Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Common.ApplicationTests
{
    [TestFixture()]
    public class AreaServiceTests
    {
        IAreaService areaService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            areaService = Bootstrap.Container.Resolve<IAreaService>();
        }


        [Test()]
        public void AreaService_GetSubArea_Test()
        {
            IList<Area> areaList = areaService.GetSubArea("110000");
        }
    }
}