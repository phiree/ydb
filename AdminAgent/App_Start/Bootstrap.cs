using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using NHibernate;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor.Installer;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Ydb.Common.Infrastructure;
using AutoMapper;

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
        Ydb.Common.LoggingConfiguration.Config("Ydb.AdminAgent");
        container = new WindsorContainer();
        container.Install(
            new Ydb.Infrastructure.Installer()
            );

        container.Install(
            new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
            );
        container.Install(
       new Ydb.Push.Infrastructure.InstallerPush(BuildDBConfig("ydb_push"))
       );
        container.Install(
    new OpenfireExtension.InstallerOpenfireExtension()
    );
        container.Install(
         new Ydb.Notice.InstallerNotice(BuildDBConfig("ydb_notice"))
         );
        container.Install(
            new Ydb.Finance.Infrastructure.InstallerFinance(BuildDBConfig("ydb_finance"))
           );
        container.Install(
          new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource"))
          );
        container.Install(
          new Ydb.Order.Infrastructure.InstallerOrder(BuildDBConfig("ydb_order"))
          );
        container.Install(
          new Ydb.PayGateway.InstallerPayGateway(BuildDBConfig("ydb_paygateway"))
          );
        container.Install(
        new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage"))
            );
        container.Install(
           new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))
            );

        container.Install(
                new Ydb.ApplicationService.Installer()
                );
        // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
        Mapper.Initialize(x =>
            {
                x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
                x.AddProfile<Ydb.BusinessResource.Application.ModelToDtoMappingProfile>();
                x.AddProfile<Ydb.Finance.Application.ModelToDtoMappingProfile>();
            });


        //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
        //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
        //   .ConnectionStrings["MongoDB"].ConnectionString, false));
        Ydb.Common.LoggingConfiguration.Config("Ydb.Web.AdminAgent");
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