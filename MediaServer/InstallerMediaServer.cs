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

using Ydb.Common.Repository;
using Castle.Core;
using Ydb.MediaServer.Application;

namespace Ydb.MediaServer.Infrastructure
{
    public class InstallerMediaServer : IWindsorInstaller
    {
        FluentConfiguration dbConfigFinance;
        string sessionFactoryName = "MediaServerSessionFactory";
        public InstallerMediaServer(FluentConfiguration dbConfigFinance)
        {
            this.dbConfigFinance = dbConfigFinance;//Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap//Dianzhu.DAL.Mapping.AreaMap
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //todo: 支撑域的注册应该有不同之处. 可能需要体现在被支撑域中, 如何体现?
 
            InstallDb(container, store);
            InstallUnifOfWork(container, store);
           
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs< RepositoryStorageFileInfo>()
                               .WithService.DefaultInterfaces()
               );
            //container.Register(Component.For<IRepositoryClaims>().ImplementedBy<RepositoryClaims>());
            //container.Register(Component.For<IRepositoryClaimsDetails>().ImplementedBy<RepositoryClaimsDetails>());
            //container.Register(Component.For<IRepositoryOrderAssignment>().ImplementedBy<RepositoryOrderAssignment>());
            //container.Register(Component.For<IRepositoryRefund>().ImplementedBy<RepositoryRefund>());
            //container.Register(Component.For<IRepositoryPayment>().ImplementedBy<RepositoryPayment>());
            //container.Register(Component.For<IRepositoryServiceOrder>().ImplementedBy<RepositoryServiceOrder>());
            //container.Register(Component.For<IRepositoryServiceOrderAppraise>().ImplementedBy<RepositoryServiceOrderAppraise>());
            //container.Register(Component.For<IRepositoryServiceOrderPushedService>().ImplementedBy<RepositoryServiceOrderPushedService>());
            //container.Register(Component.For<IRepositoryServiceOrderRemind>().ImplementedBy<RepositoryServiceOrderRemind>());
            //container.Register(Component.For<IRepositoryServiceOrderStateChangeHis>().ImplementedBy<RepositoryServiceOrderStateChangeHis>());

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<StorageFileInfoService>()
                               .WithService.DefaultInterfaces()
               );
            //container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());

        }
       
        
        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq(sessionFactoryName))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq(sessionFactoryName))
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
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.MediaServer.Infrastructure.StorageFileInfoMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
           // HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named(sessionFactoryName));
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
