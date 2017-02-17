﻿using System;
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
            container = new WindsorContainer();
            container.Install(
                new Ydb.Infrastructure.Installer()
                );

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
                );
            container.Install(
              new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource"))
              );

            container.Install(
               new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))
                );
           // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            Mapper.Initialize(x =>
            {
                x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
                x.AddProfile<Ydb.BusinessResource.Application.ModelToDtoMappingProfile>();
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