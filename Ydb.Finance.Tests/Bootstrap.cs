using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Ydb.Finance.Tests
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
               
                new Ydb.Finance.Application.InstallerFinance(BuildConfig())
                );
        }
        private  static FluentConfiguration BuildConfig()
        {

            FluentConfiguration config = Fluently.Configure()
                             .Database(
                               SQLiteConfiguration
                              .Standard
                        .UsingFile("test.db3")
                        )
                      .ExposeConfiguration(schemaConfig=> { new SchemaExport(schemaConfig).Create(true, true); });
            return config;
        }
       
    }
}
