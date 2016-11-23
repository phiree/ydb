using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Ydb.Common.Infrastructure;
namespace Ydb.BusinessResource.Application.Tests
{
    public class Bootstrap
    {
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }

        public static FluentConfiguration dbConfigInstantMessage
        {
            get
            {
                FluentConfiguration dbConfigInstantMessage = Fluently.Configure()
                 .Database(SQLiteConfiguration.
                 Standard
                 .ConnectionString("Data Source=test_ydb_businessresource.db3; Version=3;BinaryGuid=False")
                 )
                 .ExposeConfiguration((config) => { new SchemaExport(config).Create(true, true); });
                return dbConfigInstantMessage;
            }
        }
        public static void Boot()
        {
            container = new WindsorContainer();
            container.Install(new Ydb.Infrastructure.Installer());
            container.Install(new Ydb.BusinessResource.Infrastructure.InstallerBusinessResource(dbConfigInstantMessage));
            AutoMapper.Mapper.Initialize(cfg=>
            {
                AutoMapperConfiguration.AutoMapperBusinessResource.Invoke(cfg);
            }
            );
        }


    }
}
