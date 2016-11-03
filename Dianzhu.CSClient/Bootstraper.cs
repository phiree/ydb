using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DependencyInstaller;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;
using Ydb.Common.Infrastructure;
namespace Dianzhu.CSClient
{
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

            //领域注册
            container.Install(
                new Ydb.Infrastructure.Installer()
                );
            container.Install(
            new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
            new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(container.Resolve<IEncryptService>()),
            new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                );

            container.Install(

               new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
               new Ydb.Membership.Infrastructure.InstallerMembership(),
               new Ydb.Membership.Application.InstallerMembershipDB(container.Resolve<IEncryptService>())
                // new Application.InstallerMembershipTestDB()



                );
            container.Install(
                new InstallerComponent(),
                new InstallerInfrstructure(),
                new InstallerRepository(),
                new InstallerApplicationService(),
                new InstallerUI()
                );

          


        }
        public static FluentNHibernate.Cfg.FluentConfiguration BuildConfig(string connectionstring)
        {
            var config = Fluently.Configure()
                         .Database(
                              MySQLConfiguration
                             .Standard
                             .ConnectionString(
                                  PHSuit.Security.Decrypt(connectionstring
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


    }
}
