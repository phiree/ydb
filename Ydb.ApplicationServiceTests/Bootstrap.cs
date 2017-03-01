using AutoMapper;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using OpenfireExtension;
using Ydb.BusinessResource.Infrastructure;
using Ydb.Common;
using Ydb.Finance.Application;
using Ydb.Finance.Infrastructure;
using Ydb.Infrastructure;
using Ydb.InstantMessage.Infrastructure;
using Ydb.Membership.Infrastructure;
using Ydb.Notice;
using Ydb.Push.Infrastructure;

public class Bootstrap
{
    public static IWindsorContainer Container { get; private set; }

    public static void Boot()
    {
        Container = new WindsorContainer();
        //公用組件註冊
        Container.Install(new Installer());

        //限界上下文註冊

        Container.Install(new InstallerCommon(GetDbConfig("ydb_common")));

        Container.Install(new InstallerOpenfireExtension());

        Container.Install(new InstallerFinance(GetDbConfig("ydb_finance")));
        Container.Install(new InstallerNotice(GetDbConfig("ydb_notice")));
        Container.Install(new InstallerInstantMessage(GetDbConfig("ydb_instantmessage")));
        Container.Install(new InstallerPush(GetDbConfig("ydb_push")));
        Container.Install(new InstallerBusinessResource(GetDbConfig("ydb_businessresource")));
        Container.Install(new InstallerMembership(GetDbConfig("ydb_membership")));

        Container.Install(new Ydb.ApplicationService.Installer());
        LoggingConfiguration.Config("Ydb.ApplicationTest");
        Mapper.Initialize(x =>
        {
            AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
            Ydb.Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
        });

        LoggingConfiguration.Config("ApplicationTest.log");
    }

    private static FluentConfiguration GetDbConfig(string databaseName)
    {
        var dbConfigInstantMessage = Fluently.Configure()
            .Database(SQLiteConfiguration.
                Standard
                .ConnectionString("Data Source=" + databaseName + "; Version=3;BinaryGuid=False")
            )
            .ExposeConfiguration(config => { new SchemaExport(config).Create(true, true); });

        return dbConfigInstantMessage;
    }
}