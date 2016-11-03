using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;

namespace Ydb.Membership.Tests
{
    public class Bootstrap
    {

        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot()
        {
            
            container = new WindsorContainer();


            container.Install(
                new Ydb.Infrastructure.Installer(),
                new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
                new Ydb.Membership.Infrastructure.InstallerMembership());
            container.Install(
               new Ydb.Membership.Application.InstallerMembershipDB(container.Resolve<IEncryptService>())
               // new Application.InstallerMembershipTestDB()
                
                
               
                );
         
            
            
        }
         


    }
}
