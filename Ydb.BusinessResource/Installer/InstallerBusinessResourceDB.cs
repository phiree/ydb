using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using System.Configuration;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Windsor;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Infrastructure.Repository;
 
using Ydb.BusinessResource.Infrastructure.Repository.NHibernate;
 
using Ydb.BusinessResource.Application;
using Ydb.Common.Infrastructure;
namespace Ydb.BusinessResource.Infrastructure
{
    public class InstallerIntantMessageDB : IWindsorInstaller
    {
        IEncryptService encryptService;
        public InstallerIntantMessageDB(IEncryptService encryptService)
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
                                ConfigurationManager
                               .ConnectionStrings["ydb_businessresource"].ConnectionString, false)
                                 )
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.BusinessResource.Infrastructure.Repository.NHibernate.Mapping.AreaMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("BusinessResourceSessionFactory"));
        }
        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                update.Execute(true, true);
            }
        }


    }
}
