using Castle.Windsor;
using Dianzhu.DependencyInstaller;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>
using Ydb.Common.Infrastructure;
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
              new InstallerComponent(),
           new InstallerInfrstructure(),
           new InstallerRepository(),
           new InstallerApplicationService()
            );

        Ydb.Common.LoggingConfiguration.Config("Ydb.HttpApi");


        container.Install(
            new Ydb.Infrastructure.Installer()
            );
        container.Install(
           new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource"))
           );
        container.Install(
            new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common"))
            );

      

        container.Install(

 
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage"))
            );

        container.Install(


           new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership"))
        
            // new Application.InstallerMembershipTestDB()

            );
        container.Install(


         new Ydb.Push.Infrastructure.InstallerPush(BuildDBConfig("ydb_push"))

          // new Application.InstallerMembershipTestDB()

          );
        container.Install(
      new Ydb.PayGateway.InstallerPayGateway(BuildDBConfig("ydb_paygateway"))
      );
        container.Install(
        new Ydb.Order.Infrastructure.InstallerOrder(BuildDBConfig("ydb_order"))
        );
      
        // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
        AutoMapper.Mapper.Initialize(x =>
        {
            Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
            Ydb.ApplicationService.AutoMapperConfiguration.AutoMapperCrossDomain.Invoke(x);
        });
    }

    private static FluentConfiguration BuildDBConfig(string connectionStringName)
    {
        IEncryptService encryptService = container.Resolve<IEncryptService>();
        FluentConfiguration dbConfig = Fluently.Configure()
                                                       .Database(
                                                            MySQLConfiguration
                                                           .Standard
                                                           .ConnectionString(
                                                                encryptService.Decrypt(
                                                                System.Configuration.ConfigurationManager
                                                              .ConnectionStrings[connectionStringName].ConnectionString, false)
                                                                )
                                                     );
        return dbConfig;
    }
}