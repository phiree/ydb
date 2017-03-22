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
using Ydb.InstantMessage.DomainModel;
using Ydb.InstantMessage.Infrastructure.Repository;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Application;
using Ydb.Common.Repository;
using Castle.Core;

namespace Ydb.InstantMessage.Infrastructure
{
    public class InstallerInstantMessage : IWindsorInstaller
    {
        FluentConfiguration dbConfigInstantMessage;
        public InstallerInstantMessage(FluentConfiguration dbConfigInstantMessage)
        {
            this.dbConfigInstantMessage = dbConfigInstantMessage;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDb(container, store);
            //   new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallUnifOfWork(container, store);
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryReception>().ImplementedBy<RepositoryReception>()
             //    .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );
            container.Register(Component.For<IRepositoryChat>().ImplementedBy<RepositoryChat>()
               //     .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );
            container.Register(Component.For<IRepositoryIMUserStatus>().ImplementedBy<RepositoryIMUserStatus>()
                //     .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );
            container.Register(Component.For<IRepositoryIMUserStatusArchieve>().ImplementedBy<RepositoryIMUserStatusArchieve>()
                //     .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReceptionService>().ImplementedBy<Ydb.InstantMessage.Application.ReceptionService>());
            container.Register(Component.For<IChatService>().ImplementedBy<Ydb.InstantMessage.Application.ChatService>());
            container.Register(Component.For<IIMUserStatusService>().ImplementedBy<Ydb.InstantMessage.Application.IMUserStatusService>());
            container.Register(Component.For<IIMUserStatusArchieveService>().ImplementedBy<Ydb.InstantMessage.Application.IMUserStatusArchieveService>());
        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
            //OpenFire
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string domain = Dianzhu.Config.Config.GetAppSetting("ImDomain");
            container.Register(Component.For<IInstantMessage>().ImplementedBy<OpenfireXMPP>()
                                .DependsOn(
                                  Dependency.OnValue("server", server)
                                , Dependency.OnValue("domain", domain)
                                )
                            );
        }
       
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AssignStratage>().ImplementedBy<AssignStratageRandom>());
            container.Register(Component.For<IReceptionAssigner>().ImplementedBy<ReceptionAssigner>());
            container.Register(Component.For<DomainModel.DataStatistics.IStatisticsInstantMessage>().ImplementedBy<DomainModel.DataStatistics.StatisticsInstantMessage>());

            container.Register(Component.For<IMessageAdapter>().ImplementedBy<MessageAdapter>());
            container.Register(Component.For<IReceptionSession>().ImplementedBy<ReceptionSessionOpenfireRestapi>()
                .DependsOn(Dependency.OnValue("restApiUrl", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiBaseUrl")))
                .DependsOn(Dependency.OnValue("restApiSecretKey", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiAuthKey")))
                .Named("OpenfireSession").IsFallback());

        }


        private void InstallUnifOfWork(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
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
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping.ReceptionStatusMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("InstantMessageSessionFactory"));
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
