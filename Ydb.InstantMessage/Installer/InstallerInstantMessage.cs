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
    public class InstallerInstantMessage : IWindsorInstaller
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
            container.Register(Component.For<IRepositoryReception>().ImplementedBy<RepositoryReception>()
             //    .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );
            container.Register(Component.For<IRepositoryChat>().ImplementedBy<RepositoryChat>()
               //     .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                );

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReceptionService>().ImplementedBy<Ydb.InstantMessage.Application.ReceptionService>());
            container.Register(Component.For<IChatService>().ImplementedBy<Ydb.InstantMessage.Application.ChatService>());
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
