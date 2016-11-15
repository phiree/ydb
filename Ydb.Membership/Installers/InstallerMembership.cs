using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Infrastructure;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping;
using Ydb.Membership.DomainModel.Service;
using Ydb.Membership.Application;
using Ydb.Common.Repository;
using Castle.Core;

namespace Ydb.Membership.Infrastructure
{
    public class InstallerMembership : IWindsorInstaller
    {
        FluentConfiguration dbConfig;
        public InstallerMembership(FluentConfiguration dbConfig)
        {
            this.dbConfig = dbConfig;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDB(container, store);
            InstallUnifOfWork(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
         
            
        }

        
        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryDZMembership>().ImplementedBy<RepositoryDZMembership>());
            container.Register(Component.For<IRepositoryUserToken>().ImplementedBy<RepositoryUserToken>());

        }
       
      
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipDomainService>().ImplementedBy<DZMembershipDomainService>());
            container.Register(Component.For<ILoginNameDetermine>().ImplementedBy < LoginNameDetermine>());
            container.Register(Component.For<ILogin3rd>().ImplementedBy<Login3rd>());
            
        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipService>().ImplementedBy<DZMembershipService>());
        }

        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("MembershipSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("MembershipSessionFactory"))
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

        private void InstallDB(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory = dbConfig
                                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DZMembershipMap>())
                                  .ExposeConfiguration(BuildSchema)
                                  .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("MembershipSessionFactory"));


        }
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }

    }

}
