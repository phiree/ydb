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
using Ydb.Common.Repository;
using Castle.Core;

namespace Ydb.Finance.Infrastructure
{
    public class InstallerFinance : IWindsorInstaller
    {
        FluentConfiguration dbConfigFinance;
        public InstallerFinance(FluentConfiguration dbConfigFinance)
        {
            this.dbConfigFinance = dbConfigFinance;//Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap//Dianzhu.DAL.Mapping.AreaMap
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDb(container, store);
            InstallUnifOfWork(container, store);
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBalanceFlow>().ImplementedBy<RepositoryBalanceFlow>()
        //         .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
                );
            container.Register(Component.For<IRepositoryBalanceTotal>().ImplementedBy<RepositoryBalanceTotal>()
             //   .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryServiceTypePoint>().ImplementedBy<RepositoryServiceTypePoint>()
             //   .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryUserTypeSharePoint>().ImplementedBy<RepositoryUserTypeSharePoint>()
             //   .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryBalanceAccount>().ImplementedBy<RepositoryBalanceAccount>()
              //  .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
               );
            container.Register(Component.For<IRepositoryWithdrawApply>().ImplementedBy<RepositoryWithdrawApply>());

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());
            container.Register(Component.For<IOrderShareService>().ImplementedBy<OrderShareService>());
            container.Register(Component.For<IServiceTypePointService>().ImplementedBy<ServiceTypePointService>());
            container.Register(Component.For<IUserTypeSharePointService>().ImplementedBy<UserTypeSharePointService>());
            container.Register(Component.For<IBalanceAccountService>().ImplementedBy<BalanceAccountService>());
            container.Register(Component.For<IWithdrawApplyService>().ImplementedBy<WithdrawApplyService>());
            container.Register(Component.For<IBalanceTotalService>().ImplementedBy<BalanceTotalService>());
        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICountServiceFee>().ImplementedBy<CountServiceFee_Alipay>());
        }
       
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
           

        }
        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("FinanceSessionFactory"))
                    );
            }
        }

        private void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of all repositories.
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
                    return;
                }
            }
        }
        private void InstallDb(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory =
                    dbConfigFinance
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("FinanceSessionFactory"));
        }
        private void BuildSchema(Configuration config)
        {
            //SchemaUpdate update = new SchemaUpdate(config);
            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                new SchemaUpdate(config).Execute(true, true);
            }
            //else if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "2")
            //{
            //    new SchemaExport(config).Create(true, true);
            //}

        }


    }
}
