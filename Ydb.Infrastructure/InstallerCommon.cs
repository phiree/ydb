using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ydb.Common.Infrastructure;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Core;
using Ydb.Common.Repository;


namespace Ydb.Infrastructure
{
    public class InstallerCommon : IWindsorInstaller
    {
        FluentConfiguration dbConfigCommon;
        public InstallerCommon(FluentConfiguration dbConfigCommon)
        {
            this.dbConfigCommon = dbConfigCommon;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDb(container, store);
            InstallUnifOfWork(container, store);
            container.Register(Component.For<ISerialNoBuilder>().ImplementedBy<SerialNoBuilder>());
        }

        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("CommonSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("CommonSessionFactory"))
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
                    dbConfigCommon
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Infrastructure.Repository.NHibernate.Mapping.SerialNoMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("CommonSessionFactory"));
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
