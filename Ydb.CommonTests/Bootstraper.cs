﻿using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

       // FluentConfiguration dbConfigCommon = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_common.db3"))
       //.ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });


        container.Install(
                new Ydb.Infrastructure.Installer()
                );


        container.Install(
            //new Ydb.Infrastructure.InstallerCommon(dbConfigCommon)
            new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
            );

        Ydb.Common.LoggingConfiguration.Config("Ydb.Common.Tests");

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