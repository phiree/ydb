using NUnit.Framework;
using Dianzhu.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationServiceTests
{
    [TestFixture()]
    public class utilsTests
    {
        [Test()]
        public void utils_GetCity_Test()
        {

            //112.369447,26.978709
            utils.GetCity("112.369447", "26.978709");
            //utils.GetCity("110.317672", "20.029132");
            //Assert.Fail();
        }
    }
}