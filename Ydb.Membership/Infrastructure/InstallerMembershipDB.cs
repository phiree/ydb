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
using Ydb.Membership.DomainModel;
using System.Configuration;

namespace Ydb.Membership.Infrastructure
{
    internal class InstallerMembershipDB : IWindsorInstaller
    {
        string dbType;
        string connectionString;

        public InstallerMembershipDB(string dbType,
        string connectionString)
        {
            this.connectionString = connectionString;
            this.dbType = dbType;
        }
 
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            //Database

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(BuildConfig()
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping.DZMembershipMap>())
               .BuildSessionFactory()));
        }

           FluentConfiguration BuildConfig( )
        {
            switch (dbType)
            {
                case "mysql":return BuildMysqlConfig( );
                case "sqlite":return BuildSqliteConfig( );
            }

            throw new Exception("不支持的数据库类型:" + dbType);
        }
            FluentNHibernate.Cfg.FluentConfiguration BuildMysqlConfig( )
        {
            var config = Fluently.Configure()
                         .Database(
                              MySQLConfiguration
                             .Standard
                             .ConnectionString(
                                  PHSuit.Security.Decrypt(connectionString
                               , false)
                                )
                           )
                          .ExposeConfiguration(c =>
                          {
                              if (ConfigurationManager.AppSettings["UpdateSchema"] == "1")
                              {
                                  new SchemaUpdate(c).Execute(true, true);
                              }
                          });
            return config;
        }
            FluentConfiguration BuildSqliteConfig()
        {

            FluentConfiguration config = Fluently.Configure()
                             .Database(
                               SQLiteConfiguration
                              .Standard
                        .UsingFile(connectionString)
                        )
                      .ExposeConfiguration(schemaConfig => { new SchemaExport(schemaConfig).Create(true, true); });
            return config;
        }



    }
}
