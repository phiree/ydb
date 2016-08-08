using NUnit.Framework;
using Dianzhu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.DAL.Tests
{
    [TestFixture()]
    public class DALServiceOrderTests
    {
        [SetUp]
        public void Init()
        {
            PHSuit.Logging.Config("Dianzhu.DAL.Tests.DALServiceOrderTests");
        }
        [Test()]
        public void GetOrderListOfServiceByDateRangeTest()
        {
            DALServiceOrder dal = new DALServiceOrder();
            Action ac = () => {
                 var list=  dal.GetOrderListOfServiceByDateRange(new Guid("9ab2f087-2f15-4c45-86a4-a6510094dfda"), new DateTime(2016, 7, 30), new DateTime(2016, 7, 30));
                //dal.GetOne(Guid.NewGuid());
            };
            NHibernateUnitOfWork.With.Transaction(ac);
        }
    }
}