using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>

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
            new Dianzhu.DependencyInstaller.InstallerComponent(),
            new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
            new Dianzhu.DependencyInstaller.InstallerRepository(),
            new Dianzhu.DependencyInstaller.InstallerApplicationService(),


          new Ydb.InstantMessage.Application.InstallerInstantMessage(
                        BuildConfig(System.Configuration.ConfigurationManager
                            .ConnectionStrings["ydb_instantmessage"].ConnectionString)
                            ),
                   

               

                new Ydb.Membership.Application.InstallerMembership(
                     BuildConfig(System.Configuration.ConfigurationManager
                            .ConnectionStrings["ydb_membership"].ConnectionString)
                    ),
             
            new Ydb.Infrastructure.Installer()
             
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