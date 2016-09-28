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
            NHibernateUnitOfWork.UnitOfWork.Start();

            DALDZService dal = new DALDZService();
            int totalRecord;
        IList<Model.DZService> serviceList=    dal.SearchService(string.Empty, 0, 200, new Guid("2ccd3b34-8597-4076-81bc-520a91b42e29"), Convert.ToDateTime("2016/9/26 17:30:00"), 110.32973, 20.035808, 0, 10, out totalRecord);
            foreach (Model.DZService s in serviceList)
            {
                Console.WriteLine(s.Name);
            }

            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
    }
}
