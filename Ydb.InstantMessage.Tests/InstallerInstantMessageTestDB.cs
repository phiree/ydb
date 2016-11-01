using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Ydb.InstantMessage.Tests
{
    public class InstallerInstantMessageTestDB : IWindsorInstaller
    {
        ISessionFactory _sessionFactory;
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
              _sessionFactory = Fluently.Configure()
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping.ReceptionChatMap>())
                       .Database(
                            SQLiteConfiguration
                           .Standard
                           //.InMemory
                     .UsingFile("test_instantmessage.db3") 
                     )
                  
                        .ExposeConfiguration(BuildSchema)
                   .BuildSessionFactory();
           
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));



        }
        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
   .Create(true, true);
            //SchemaUpdate update = new SchemaUpdate(config);
            //update.Execute(true, true);

        }

    }
}
