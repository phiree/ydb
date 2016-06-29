using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.Model;
using Dianzhu.BLL;
namespace Dianzhu.Test.DALTest
{
    [TestFixture]
   public  class UnitOfWorkTest
    {
        [Test]
        public void UFtest1()
        {
            ServiceOrder order = null;
          NHibernateUnitOfWork.UnitOfWork.Start();
            Action ac = () => { 
            order = NHibernateUnitOfWork.UnitOfWork.CurrentSession.Get<ServiceOrder>(new Guid("db83d2d7-a6fe-4046-aaf2-a6090111aa0a"));
            };
            ac();
            //NHibernate.NHibernateUtil.Initialize(order.Details);
            //NHibernate.NHibernateUtil.Initialize(order.Service);
            //NHibernate.NHibernateUtil.Initialize(order.Service.ServiceType.Parent);
            //NHibernate.NHibernateUtil.Initialize(order.Service.ServiceType.Children);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush(System.Data.IsolationLevel.ReadCommitted);
           // NHibernateUnitOfWork.With.Transaction(ac);
             NHibernateUnitOfWork.UnitOfWork.Current.Dispose();
            /*
            Dianzhu.Test.DALTest.UnitOfWorkTest.UFtest1:
 NHibernate.LazyInitializationException :
     Initializing[Dianzhu.Model.ServiceOrder#db83d2d7-a6fe-4046-aaf2-a6090111aa0a]-
        failed to lazily initialize a collection of role:
         Dianzhu.Model.ServiceOrder.Details, no session or session was closed
             */
           
            
           // System.Threading.Thread.Sleep(10000);
            NHibernateUnitOfWork.UnitOfWork.Start();
            order = NHibernateUnitOfWork.UnitOfWork.CurrentSession.Get<ServiceOrder>(new Guid("db83d2d7-a6fe-4046-aaf2-a6090111aa0a"));
            //     NHibernateUnitOfWork.UnitOfWork.CurrentSession.Update(order);
            //     NHibernateUnitOfWork.UnitOfWork.CurrentSession.Refresh(order);
            ServiceType st = order.Service.ServiceType;
                string name = st.ToString();
                st.Name = "name1";
             //   NHibernateUnitOfWork.UnitOfWork.CurrentSession.Update(st);
                Console.WriteLine(st.ToString());
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush(System.Data.IsolationLevel.ReadCommitted);
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

            
        }
    }
}
