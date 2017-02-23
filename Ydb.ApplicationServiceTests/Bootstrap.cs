using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
using Ydb.ApplicationService;
using Ydb.ApplicationService.ExcelImporter;
using Ydb.Common.Infrastructure;
using Ydb.Infrastructure;

public class Bootstrap
{
    private static IWindsorContainer container;

    public static IWindsorContainer Container
    {
        get { return container; }
        private set { container = value; }
    }

    public static void Boot()
    {
        container = new WindsorContainer();

        container.Register(Component.For<ServiceTypeImporter>());
        //公用組件註冊
        container.Install(new Ydb.Infrastructure.Installer());

        container.Install(new OpenfireExtension.InstallerOpenfireExtension());

        //限界上下文註冊

        container.Install(new Ydb.Infrastructure.InstallerCommon(BuildDBConfig("ydb_common")));

        container.Install(new Ydb.Finance.Infrastructure.InstallerFinance(BuildDBConfig("ydb_finance")));

        container.Install(new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(BuildDBConfig("ydb_businessresource")));
        AutoMapper.Mapper.Initialize(x =>
        {
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