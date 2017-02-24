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
        //公用組件註冊
        container.Install(new Ydb.Infrastructure.Installer());

        //限界上下文註冊

        container.Install(new Ydb.Infrastructure.InstallerCommon(GetDbConfig("ydb_common")));

        container.Install(new OpenfireExtension.InstallerOpenfireExtension());

        container.Install(new Ydb.Finance.Infrastructure.InstallerFinance(GetDbConfig("ydb_finance")));
        container.Install(new Ydb.Notice.InstallerNotice(GetDbConfig("ydb_notice")));
        container.Install(new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(GetDbConfig("ydb_instantmessage")));
        container.Install(new Ydb.Push.Infrastructure.InstallerPush(GetDbConfig("ydb_push")));
        container.Install(new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(GetDbConfig("ydb_businessresource")));
        container.Install(new Ydb.Membership.Infrastructure.InstallerMembership(GetDbConfig("ydb_membership")));

        container.Install(new Ydb.ApplicationService.Installer());

        AutoMapper.Mapper.Initialize(x =>
        {
            Ydb.Finance.Application.AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
        });
    }

    private static FluentConfiguration GetDbConfig(string databaseName)
    {
        FluentConfiguration dbConfigInstantMessage = Fluently.Configure()
         .Database(SQLiteConfiguration.
         Standard
         .ConnectionString("Data Source=" + databaseName + "; Version=3;BinaryGuid=False")
         )
         .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });
        return dbConfigInstantMessage;
    }
}