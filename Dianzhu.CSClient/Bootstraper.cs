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
            container.Install(

               new Ydb.InstantMessage.Application.InstallerInstantMessage(
                        BuildConfig(System.Configuration.ConfigurationManager
                            .ConnectionStrings["ydb_instantmessage"].ConnectionString)
                            ),




                new Ydb.Membership.Application.InstallerMembership(
                     BuildConfig(System.Configuration.ConfigurationManager
                            .ConnectionStrings["ydb_membership"].ConnectionString)
                    ),


                new Ydb.Infrastructure.Installer(),
                new InstallerComponent(),
                new InstallerInfrstructure(),
                new InstallerRepository(),
                new InstallerApplicationService(),
                new InstallerUI()
                );

            Ydb.Membership.Application.AutoMapperConfiguration.Configure();


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
