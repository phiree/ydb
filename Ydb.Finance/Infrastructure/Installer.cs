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
using Ydb.Finance.Infrastructure.Repository;
using Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping;

namespace Ydb.Finance.Infrastructure
{
    public class InstallerFinance : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBalanceFlow>().ImplementedBy<RepositoryBalanceFlow>());
            container.Register(Component.For<IRepositoryBalanceTotal>().ImplementedBy<RepositoryBalanceTotal>());
            container.Register(Component.For<IRepositoryServiceTypePoint>().ImplementedBy<RepositoryServiceTypePoint>());
            container.Register(Component.For<IRepositoryUserTypeSharePoint>().ImplementedBy<RepositoryUserTypeSharePoint>());

        }

        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
            //Database
            var _sessionFactory = Fluently.Configure()
                       .Database(
                            MySQLConfiguration
                           .Standard
                           .ConnectionString("data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test")
                     )
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BalanceFlowMap>())
                   .ExposeConfiguration(BuildSchema)
                   .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
            container.Register(Component.For<ISession>().Instance(_sessionFactory.OpenSession()));
            
        }
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
             

        }

    }
}
