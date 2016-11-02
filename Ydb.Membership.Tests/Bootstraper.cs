using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
               new Ydb.Membership.Application.InstallerMembership(BuildConfig())
               
                );
            
            
        }
        private static FluentConfiguration BuildConfig()
        {

            FluentConfiguration config = Fluently.Configure()
                             .Database(
                               SQLiteConfiguration
                              .Standard
                        .UsingFile("test_membership.db3")
                        )
                      .ExposeConfiguration(schemaConfig => { new SchemaExport(schemaConfig).Create(true, true); });
            return config;
        }


    }
}
