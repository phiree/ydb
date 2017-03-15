using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;




using NHibernate;
using System.Configuration;
using nhf = FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor.Installer;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>

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
            new Ydb.Finance.Infrastructure.InstallerFinance(BuildDBConfig("ydb_finance"))
            );

        container.Install(
           new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))
            );
        container.Install(
         new Ydb.Order.Infrastructure.InstallerOrder(BuildDBConfig("ydb_order"))
          );

        container.Install(
       new Ydb.PayGateway.InstallerPayGateway (BuildDBConfig("ydb_paygateway"))
        );

        AutoMapper.Mapper.Initialize(x =>
        {
            Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
        });

        //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
        //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
        //   .ConnectionStrings["MongoDB"].ConnectionString, false));
        Ydb.Common.LoggingConfiguration.Config("Dianzhu.Web.Pay");

    }


    private static nhf.FluentConfiguration BuildDBConfig(string connectionStringName)
    {
        Ydb.Common.Infrastructure.IEncryptService encryptService = container.Resolve<Ydb.Common.Infrastructure.IEncryptService>();
        nhf.FluentConfiguration dbConfig = nhf.Fluently.Configure()
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