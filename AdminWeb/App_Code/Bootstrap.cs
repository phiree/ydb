using Castle.Windsor;
using Dianzhu.DependencyInstaller;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
 
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
           new InstallerApplicationService(),
           new InstallerAdminWeb()
            );

        //公用組件註冊
        container.Install(new Ydb.Infrastructure.Installer());

        //限界上下文註冊

        container.Install(new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common")));

        container.Install(new Ydb.Finance.Infrastructure.InstallerFinance(BuildDBConfig("ydb_finance")));

        container.Install(new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(BuildDBConfig("ydb_instantmessage")));

        container.Install(new Ydb.Membership.Infrastructure.InstallerMembership(BuildDBConfig("ydb_membership")));
        container.Install(new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource")));
        AutoMapper.Mapper.Initialize(x =>
        {
            Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
            Ydb.Finance.Application.AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
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