﻿using AutoMapper;
using Castle.Windsor;
 using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>
namespace Dianzhu.CSClient
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
                new Ydb.Infrastructure.Installer()
                );

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
                );

            container.Install(
              new Ydb.Order.Infrastructure.InstallerOrder (BuildDBConfig("ydb_order"))
              );

            container.Install(
            new Ydb.PayGateway.InstallerPayGateway(BuildDBConfig("ydb_paygateway"))
            );
            container.Install(
           new Ydb.Push.Infrastructure.InstallerPush(BuildDBConfig("ydb_push"))
           );

            container.Install(
                
                new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage"))
                );

            container.Install(
                new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))        
                );
            container.Install(
              new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource"))
              );

            container.Install(
             new Ydb.MediaServer.Infrastructure.InstallerMediaServer(BuildDBConfig("ydb_mediaserver"))
             );


            container.Install(
            
               new InstallerUI()
                );

            AutoMapper.Mapper.Initialize(x =>
            {
                Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
                Ydb.ApplicationService.AutoMapperConfiguration.AutoMapperCrossDomain.Invoke(x);
            });

            //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
            //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
            //   .ConnectionStrings["MongoDB"].ConnectionString, false));
            Ydb.Common.LoggingConfiguration.Config("Ydb.CSClient");
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