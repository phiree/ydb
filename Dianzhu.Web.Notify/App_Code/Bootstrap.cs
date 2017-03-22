﻿using Castle.Windsor;
 
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>
using Ydb.Common.Infrastructure;
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
          new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))
          );

        container.Install(

 
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage"))
            );

        //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
        //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
        //   .ConnectionStrings["MongoDB"].ConnectionString, false));
        Ydb.Common.LoggingConfiguration.Config("Ydb.Web.Notify");

     AutoMapper.Mapper.Initialize(x =>
        {
      
            x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
           
        });
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