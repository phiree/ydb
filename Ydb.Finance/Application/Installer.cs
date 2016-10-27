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
    public class InstallerFinance : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallInfrastructure(container, store);
            Bootstrap.Boot();
            InstallApplicationService(container, store);
        }
        
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());
            container.Register(Component.For<IOrderShareService>().ImplementedBy<OrderShareService>());
            container.Register(Component.For<IServiceTypePointService>().ImplementedBy<ServiceTypePointService>());
            container.Register(Component.For<IUserTypeSharePointService>().ImplementedBy<UserTypeSharePointService>());
            container.Register(Component.For<IWithdrawCashService>().ImplementedBy<WithdrawCashService>());
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
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap>())
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
    }
}
