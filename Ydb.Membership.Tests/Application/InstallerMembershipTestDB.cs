using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Infrastructure;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping;
namespace Ydb.Membership.Tests.Application
{
    public class InstallerMembershipTestDB : IWindsorInstaller
    {
        ISessionFactory _sessionFactory;
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
              _sessionFactory = Fluently.Configure()
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DZMembershipMap>())
                       .Database(
                            SQLiteConfiguration
                           .Standard
                           //.InMemory
                     .UsingFile("test_ydb_membership.db3") 
                     )
                   .ExposeConfiguration(BuildSchema)
                   .BuildSessionFactory();
           
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));



        }
        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
   .Create(true, true);
            //SchemaUpdate update = new SchemaUpdate(config);
            //update.Execute(true, true);

        }

    }
}
