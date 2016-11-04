﻿using Castle.MicroKernel.Registration;
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
using Ydb.Finance.DomainModel;
using Ydb.Finance.Infrastructure.Repository;
using Ydb.Finance.Infrastructure.Repository.NHibernate;
using Ydb.Finance.Application;
using Ydb.Common.Infrastructure;
namespace Ydb.Finance.Infrastructure
{
    public class InstallerFinanceDB : IWindsorInstaller
    {
        IEncryptService encryptService;
        public InstallerFinanceDB(IEncryptService encryptService)
        {
            this.encryptService = encryptService;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString(
                                 encryptService.Decrypt(
                                 System.Configuration.ConfigurationManager
                               .ConnectionStrings["ydb_finance"].ConnectionString, false)
                                 )
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping.BalanceFlowMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("FinanceSessionFactory"));
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
