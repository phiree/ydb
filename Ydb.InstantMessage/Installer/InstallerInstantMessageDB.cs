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
using Ydb.InstantMessage.DomainModel;
using Ydb.InstantMessage.Infrastructure.Repository;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Application;
using Ydb.Common.Infrastructure;
namespace Ydb.InstantMessage.Infrastructure
{
    public class InstallerIntantMessageDB : IWindsorInstaller
    {
        FluentConfiguration dbConfigInstantMessage;
        public InstallerIntantMessageDB(FluentConfiguration dbConfigInstantMessage)
        {
            this.dbConfigInstantMessage = dbConfigInstantMessage;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory =
                //Fluently.Configure()
                //        .Database(
                //             MySQLConfiguration
                //            .Standard
                //            .ConnectionString(
                //                 encryptService.Decrypt(
                //                 System.Configuration.ConfigurationManager
                //               .ConnectionStrings["ydb_instantmessage"].ConnectionString, false)
                //                 )
                //      )
                dbConfigInstantMessage
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping.ReceptionStatusMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("InstantMessageSessionFactory"));
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
