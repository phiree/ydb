using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DAL;
using NUnit.Framework;
using Dianzhu.DependencyInstaller;
using Castle.Windsor;
using Dianzhu.IDAL;
using System.Threading;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Ydb.Membership.Application;
namespace Ydb.Test.System.BugRepeat
{
    ///<summary>
    /// 
    /// </summary>
    /*
     * 同时可能触发的异常:
     System.InvalidOperationException: 集合已修改；可能无法执行枚举操作。
 在 System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
 在 System.Collections.Generic.List`1.Enumerator.MoveNextRare()
 在 System.Collections.Generic.List`1.Enumerator.MoveNext()
 在 NHibernate.Cfg.Configuration.SecondPassCompile()
 在 NHibernate.Cfg.Configuration.BuildSessionFactory()
 在 NHibernateUnitOfWork.UnitOfWorkFactory.get_SessionFactory() 位置 E:\Projects\dianzhu\src_vs2015\Dianzhu.DAL\UnitOfWork\UnitOfWorkFactory.cs:行号 89
         */
    [TestFixture]
   public class An_item_with_the_same_key_has_already_been_added
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.SystemTest");
        [SetUp]
        public void Setup()
        {
            PHSuit.Logging.Config("TestSystem");

        }
        [Test]
        public void An_item_with_the_same_key_has_already_been_added_in_dal()
        {
          
          
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread( AccessData);
                t.Start();
               // AccessData();
            }

            Thread.Sleep(5000);
            
        }
        /*
            object lockObject = new object();
        public ISessionFactory SessionFactory
        {
            get
            {
                lock (lockObject)------------> 如果不加上这个锁 就会抛"集合修改"的异常.
                { 
                    if (_sessionFactory == null)
                    _sessionFactory = Configuration.BuildSessionFactory();
                return _sessionFactory;
                }
            }
        }
             */
        private void AccessData()
        {
            try
            {
                IWindsorContainer container = new WindsorContainer();
                container.Install(
                   new InstallerRepository()
                    );
                NHibernateUnitOfWork.UnitOfWork.Start();
                IDALPaymentLog dalPaymentLog = container.Resolve<IDALPaymentLog>();
                Guid newId = Guid.NewGuid();
                dalPaymentLog.FindById(newId);
                log.Debug("find palymentlog:"+ newId);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
               
            }
            catch (Exception ex)
            {
                log.Debug(ex.ToString());
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        [Test]
        public void An_item_with_the_same_key_has_already_been_added_in_Domain()
        {


            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(AccessDataDomain);
                t.Start();
                // AccessData();
            }

            Thread.Sleep(5000);

        }
       
        private void AccessDataDomain()
        {
            try
            {
                FluentConfiguration dbConfigInstantMessage = Fluently.Configure().
                    Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_InstantMessage.db3"));

                IWindsorContainer container = new WindsorContainer();
                container.Install(
                    new Ydb.Infrastructure.Installer(),
                 new  Ydb.Membership.Infrastructure.InstallerMembership(),
                   new Ydb.Membership.Application.InstallerMembershipDB(dbConfigInstantMessage)
                    );
                
                 IDZMembershipService memberService = container.Resolve<IDZMembershipService>();
                Guid newId = Guid.NewGuid();
                memberService.GetUserById(newId.ToString());
     

            }
            catch (Exception ex)
            {
                log.Debug(ex.ToString());
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
