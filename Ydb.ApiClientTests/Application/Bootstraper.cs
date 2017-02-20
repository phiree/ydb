using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace Ydb.ApiClient.Application.Tests
{
    public class Bootstrap
    {

        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
          static FluentConfiguration GetDbConfig(string databaseName)
        {
             
                FluentConfiguration dbConfigInstantMessage = Fluently.Configure()
                 .Database(SQLiteConfiguration.
                 Standard
                 .ConnectionString("Data Source="+ databaseName + "; Version=3;BinaryGuid=False")
                 )
                 .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });
                return dbConfigInstantMessage;
           
        }
        public static void Boot()
        {

            container = new WindsorContainer();

            FluentConfiguration dbConfigCommon = GetDbConfig("test_ydb_common.db3");
            FluentConfiguration dbConfigApiClient = GetDbConfig("test_ydb_apiclient.db3");


            container.Install(
                    new Ydb.Infrastructure.Installer()
                    );
            

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(dbConfigCommon),
                new Ydb.ApiClient.InstallerApiClient(dbConfigApiClient)
               
                );



        }



    }
}
