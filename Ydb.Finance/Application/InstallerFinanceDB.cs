using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Windsor;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.Application
{
    public class InstallerFinanceDB : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Database
            var _sessionFactory = GetSessionFactory(true);
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
            container.Register(Component.For<ISession>().Instance(_sessionFactory.OpenSession()));
        }

        private static void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }

        public static ISessionFactory GetSessionFactory(bool b)
        {
            ISessionFactory _sessionFactory;
            if (b)
            {
                _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString("data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test")
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            else
            {
                //Database
                _sessionFactory = Fluently.Configure()
                         .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap>())
                         .Database(
                              SQLiteConfiguration
                             .Standard
                       //.InMemory
                       .UsingFile("test.db3")
                       )
                     .ExposeConfiguration(BuildSchema)
                     .BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }
}
