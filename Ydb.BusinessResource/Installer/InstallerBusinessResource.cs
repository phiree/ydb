﻿using Castle.MicroKernel.Registration;
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
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Infrastructure.Repository;

using Ydb.BusinessResource.Infrastructure.Repository.NHibernate;

using Ydb.BusinessResource.Application;
using Castle.Core;
using Ydb.Common.Repository;

namespace Ydb.BusinessResource.Infrastructure
{
    public class InstallerBusinessResource : IWindsorInstaller
    {
        FluentConfiguration dbConfigBusinessResource;
        public InstallerBusinessResource(FluentConfiguration dbConfigBusinessResource)
        {
            this.dbConfigBusinessResource = dbConfigBusinessResource;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDb(container, store);
            InstallUnifOfWork(container, store);
           
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
            
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBusiness>().ImplementedBy<RepositoryBusiness>()
                 .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("BusinessResourceSessionFactory"))
                );
           

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBusinessService>().ImplementedBy< BusinessService>());
          
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
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("BusinessResourceSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("BusinessResourceSessionFactory"))
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
            var _sessionFactory = dbConfigBusinessResource
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.AreaMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("BusinessResourceSessionFactory"));
        }
        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                update.Execute(true, true);
            }
        }


    }
}
