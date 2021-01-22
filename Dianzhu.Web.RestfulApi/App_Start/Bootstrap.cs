using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;




//using NHibernate;
using System.Configuration;
//using nhf = FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;
//using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor.Installer;
using Dianzhu.ApplicationService;
using Ydb.Common.Infrastructure;
using AutoMapper;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Dianzhu.Web.RestfulApi
{
    public class Bootstrap
    {
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot()
        {
            container = new WindsorContainer();
            container.Install(
                //new Dianzhu.DependencyInstaller.InstallerComponent(),
                //new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
                //new Dianzhu.DependencyInstaller.InstallerRepository(),
                //new Dianzhu.DependencyInstaller.InstallerApplicationService()
                );



            container.Install(
                new Ydb.Infrastructure.Installer()
                );

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
                );
            container.Install(
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage"))
                );
            container.Install(
              new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource"))
              );
            container.Install(
           new Ydb.Order.Infrastructure.InstallerOrder(BuildDBConfig("ydb_order"))
           );

            container.Install(
         new Ydb.Push.Infrastructure.InstallerPush(BuildDBConfig("ydb_push"))
         );
            container.Install(
        new Ydb.ApiClient.InstallerApiClient(BuildDBConfig("ydb_apiclient"))
        );
            container.Install(
     new Ydb.PayGateway.InstallerPayGateway(BuildDBConfig("ydb_paygateway"))
     );
            container.Install(
    new Ydb.MediaServer.Infrastructure.InstallerMediaServer(BuildDBConfig("ydb_mediaserver"))
    );

            container.Install(


               new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership")),

            // new Application.InstallerMembershipTestDB()
            new InstallerRestfulApi()
                );
            // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            Mapper.Initialize(x =>
            {
                x.AddProfile<ApplicationService.Mapping.ModelToDtoMappingProfile>();
                x.AddProfile<ApplicationService.Mapping.DtoToModelMappingProfile>();
                x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
                x.AddProfile<Ydb.BusinessResource.Application.ModelToDtoMappingProfile>();
                x.AddProfile<Ydb.ApplicationService.ModelToDtoMappingProfileCrossDomain>();
            });


            //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
            //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
            //   .ConnectionStrings["MongoDB"].ConnectionString, false));
            Ydb.Common.LoggingConfiguration.Config("Dianzhu.Web.RestfulApi");
        }

        private static FluentConfiguration BuildDBConfig(string connectionStringName)
        {
            IEncryptService encryptService = container.Resolve<IEncryptService>();
            FluentConfiguration dbConfig = Fluently.Configure()
                                                           .Database(
                                                                MySQLConfiguration
                                                               .Standard
                                                               .ConnectionString(
                                                                    encryptService.Decrypt(
                                                                    System.Configuration.ConfigurationManager
                                                                  .ConnectionStrings[connectionStringName].ConnectionString, false)
                                                                    )
                                                         );
            return dbConfig;
        }
    }
}