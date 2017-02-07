using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using FluentNHibernate.Cfg;
using Castle.Windsor;

using Ydb.Common.Repository;
using Castle.Core;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Ydb.PayGateway.DomainModel.Repository;
using Ydb.PayGateway.Infrastructure;
using Ydb.PayGateway.Infrastructure.Nhibernate.Repository;

namespace Ydb.PayGateway
{
    public class InstallerPayGateway : IWindsorInstaller
    {
        FluentConfiguration dbConfigPayGateway;
        public InstallerPayGateway(FluentConfiguration dbConfigPayGateway)
        {
            this.dbConfigPayGateway =dbConfigPayGateway;//Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap//Dianzhu.DAL.Mapping.AreaMap
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
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<RepositoryRefundLog>().WithService.DefaultInterfaces());
          

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<Application.PaymentLogService>()
                               .WithService.DefaultInterfaces()
               );
           
        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
             
          
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
                    dbConfigPayGateway
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.PayGateway.Infrastructure.Nhibernate.Mapping.RefundLogMap>())
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
