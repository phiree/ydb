using Castle.Windsor;
using Dianzhu.DependencyInstaller;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Ydb.Common.Infrastructure;
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
              new InstallerComponent(),
           new InstallerInfrstructure(),
           new InstallerRepository(),
           new InstallerApplicationService()
            
            );



        container.Install(
            new Ydb.Infrastructure.Installer()
            );


        container.Install(

new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(BuildDBConfig("ydb_instantmessage")),
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
            );

        container.Install(


           new Ydb.Membership.Infrastructure.InstallerMembership(),
           new Ydb.Membership.Application.InstallerMembershipDB(BuildDBConfig("ydb_membership"))
            // new Application.InstallerMembershipTestDB()

            );
        // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
        AutoMapper.Mapper.Initialize(x =>
        {
            Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
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