using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.DAL;
namespace Dianzhu.Test.DALTest
{
    [TestFixture]
    public class DALServiceTest
    {
        [Test]
        public void Search()
        {
            DALDZService dal = new DALDZService();
            int totalRecord;
        IList<Model.DZService> serviceList=    dal.SearchService(0, 9999, new Guid("4997bd4b-2b1a-4b61-9e02-014883a0d901"), Convert.ToDateTime("2016-3-22 4:00:00"), 0, 13, out totalRecord);
            foreach (Model.DZService s in serviceList)
            {
                Console.WriteLine(s.Name);
            }
        }
    }
}
