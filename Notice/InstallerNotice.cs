using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using Ydb.Common.Repository;
using Ydb.Notice.DomainModel.Repository;
using Ydb.Notice.Infrastructure.YdbNHibernate.Repository;
using Ydb.Notice.Application;
using NHibernate;
using Ydb.Notice.Infrastructure.YdbNHibernate.UnitOfWork;
using Castle.Core;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Ydb.Notice
{
    public class InstallerNotice : IWindsorInstaller
    {
        private FluentConfiguration dbConfigInstantMessage;

        public InstallerNotice(FluentConfiguration dbConfigInstantMessage)
        {
            this.dbConfigInstantMessage = dbConfigInstantMessage;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDb(container, store);
            //   new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallUnifOfWork(container, store);

            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<RepositoryNotice>()
                                .WithService.DefaultInterfaces()
                );
        }

        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<NoticeService>()
                                .WithService.DefaultInterfaces()
                );
        }

        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq(SesssionFactoryName))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq(SesssionFactoryName))
                    );
            }
        }

        private const string SesssionFactoryName = "ApiClientSessionFactory";

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
                //Fluently.Configure()
                //        .Database(
                //             MySQLConfiguration
                //            .Standard
                //            .ConnectionString(
                //                 encryptService.Decrypt(
                //                 System.Configuration.ConfigurationManager
                //               .ConnectionStrings["ydb_instantmessage"].ConnectionString, false)
                //                 )
                //      )
                dbConfigInstantMessage
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Notice.Infrastructure.YdbNHibernate.Mapping.NoticeMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named(SesssionFactoryName));
        }

        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);

            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                update.Execute(true, true);
            }
        }
    }
}