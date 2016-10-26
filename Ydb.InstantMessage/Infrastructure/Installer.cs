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

namespace Ydb.InstantMessage.Infrastructure
{
    public class InstallerIntantMessage : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryReception>().ImplementedBy<RepositoryReception>());
            container.Register(Component.For<IRepositoryChat>().ImplementedBy<RepositoryChat>());
        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReceptionService>().ImplementedBy<Ydb.InstantMessage.Application.ReceptionService>());
            container.Register(Component.For<IChatService>().ImplementedBy<Ydb.InstantMessage.Application.ChatService>());
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
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping.ReceptionStatusMap>())
                   .ExposeConfiguration(BuildSchema)
                   .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
            container.Register(Component.For<ISession>().Instance(_sessionFactory.OpenSession()).LifeStyle.PerWebRequest);
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
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AssignStratage>().ImplementedBy<Ydb.InstantMessage.DomainModel.Reception.AssignStratageRandom>());
            container.Register(Component.For<IReceptionAssigner>().ImplementedBy<Ydb.InstantMessage.DomainModel.Reception.ReceptionAssigner>());

            container.Register(Component.For<IMessageAdapter>().ImplementedBy<MessageAdapter>());
            container.Register(Component.For<IReceptionSession>().ImplementedBy<ReceptionSessionOpenfireRestapi>()
                .DependsOn(Dependency.OnValue("restApiUrl", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiBaseUrl")))
                .DependsOn(Dependency.OnValue("restApiSecretKey", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiAuthKey")))
                .Named("OpenfireSession"));

        }

    }
}
