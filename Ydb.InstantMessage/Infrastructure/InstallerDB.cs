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

namespace Ydb.InstantMessage.Infrastructure
{
    public class InstallerIntantMessageDB : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString("data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test")
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping.ReceptionStatusMap>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
        }
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }


    }
}
