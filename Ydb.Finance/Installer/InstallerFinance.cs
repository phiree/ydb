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
using Ydb.Finance.Infrastructure.Repository.NHibernate;
using Ydb.Finance.Application;

namespace Ydb.Finance.Infrastructure
{
    public class InstallerFinance : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //   new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBalanceFlow>().ImplementedBy<RepositoryBalanceFlow>()
                 .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
                );
            container.Register(Component.For<IRepositoryBalanceTotal>().ImplementedBy<RepositoryBalanceTotal>()
                .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryServiceTypePoint>().ImplementedBy<RepositoryServiceTypePoint>()
                .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryUserTypeSharePoint>().ImplementedBy<RepositoryUserTypeSharePoint>()
                .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryBalanceAccount>().ImplementedBy<RepositoryBalanceAccount>()
                .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());
            container.Register(Component.For<IOrderShareService>().ImplementedBy<OrderShareService>());
            container.Register(Component.For<IServiceTypePointService>().ImplementedBy<ServiceTypePointService>());
            container.Register(Component.For<IUserTypeSharePointService>().ImplementedBy<UserTypeSharePointService>());
            container.Register(Component.For<IWithdrawCashService>().ImplementedBy<WithdrawCashService>());
            container.Register(Component.For<IBalanceAccountService>().ImplementedBy<BalanceAccountService>());
        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
          
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
