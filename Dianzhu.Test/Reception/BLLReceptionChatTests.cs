using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Tests
{
    [TestFixture()]
    public class BLLReceptionChatTests
    {
        [SetUp]
        public void setup()
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            Bootstrap.Boot();
        }
        [Test()]
        public void GetChatsTest()
        {
            BLLReceptionChat bllRe = Bootstrap.Container.Resolve<BLLReceptionChat>();
            Model.Trait_Filtering filter = new Model.Trait_Filtering();
            filter.baseID = "";
           var list= bllRe.GetChats(filter, "pushOrder", "customerService", Guid.Empty, new Guid("45ae6bf3-701b-11e6-9ad9-02004c4f4f50"), "customer");

            Console.WriteLine(list.Count);
        }
    }
}