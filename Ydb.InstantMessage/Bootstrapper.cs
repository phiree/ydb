using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NHibernate;
using Castle.MicroKernel.SubSystems.Configuration;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.Infrastructure;
namespace Ydb.InstantMessage
{
   public static   class Bootstrapper
    {
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot()
        {
            var _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString("data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test")
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Infrastructure.Repository.NHibernate.Mapping.ReceptionStatusMapping>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            container = new WindsorContainer();
            container.Register(Component.For<IRepositoryReception>().ImplementedBy<ReceptionRepository>());
           
            container.Register(Component.For<IReceptionAssigner>().ImplementedBy<ReceptionAssigner>());

            container.Register(Component.For<IReceptionService>().ImplementedBy<ReceptionService>());
            container.Register(Component.For<IReceptionSession>().ImplementedBy<ReceptionSessionOpenfireRestapi>()
                .DependsOn(Dependency.OnValue("restApiUrl", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiBaseUrl")))
                .DependsOn(Dependency.OnValue("restApiSecretKey", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiAuthKey")))
                .Named("OpenfireSession"));
            container.Register(Component.For<AssignStratage>().ImplementedBy<AssignStratageRandom>());

            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
            container.Register(Component.For<ISession>().Instance(_sessionFactory.OpenSession()));
        }

        private static void BuildSchema(Configuration config)
        {


            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);

        }
        public class InstallerComponent : IWindsorInstaller
        {
            public void Install(IWindsorContainer container, IConfigurationStore store)
            {
              
                

            }
        }
    }
}
