using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;

namespace Ydb.OrderTests
{
    public class Bootstrap
    {

        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static FluentConfiguration dbConfigOrder
        {
            get
            {
                FluentConfiguration dbConfigInstantMessage = Fluently.Configure()
                 .Database(SQLiteConfiguration.
                 Standard
                 .ConnectionString("Data Source=test_ydb_order.db3; Version=3;BinaryGuid=False")
                 )
                 .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });
                return dbConfigInstantMessage;
            }
        }
        public static void Boot()
        {

            container = new WindsorContainer();

            FluentConfiguration dbConfigCommon = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_common.db3"))
           .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });
            FluentConfiguration dbConfigPay = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_paygateway.db3"))
         .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });

            container.Install(
                    new Ydb.Infrastructure.Installer()
                    );
            

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(dbConfigCommon),
                new Ydb.Order.Infrastructure.InstallerOrder(dbConfigOrder),
                new Ydb.PayGateway.InstallerPayGateway(dbConfigPay)
                );



        }



    }
}
