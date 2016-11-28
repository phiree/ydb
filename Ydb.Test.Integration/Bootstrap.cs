using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Ydb.Common.Infrastructure;
namespace Ydb.Test.Integration
{
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

            FluentConfiguration dbConfigCommon = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_Common.db3"));

            container.Install(
                    new Ydb.Infrastructure.Installer()
                    );

            container.Install(
                new Ydb.Infrastructure.InstallerCommon(dbConfigCommon)
                );

//            FluentConfiguration dbConfigInstantMessage = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_InstantMessage.db3"));

//            container.Install(
 
 
//new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage(dbConfigInstantMessage)
//                );


            FluentConfiguration dbConfigMembership = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_membership.db3"));
            container.Install(
 
               new Ydb.Membership.Infrastructure.InstallerMembership(dbConfigMembership)           
           
                
 
                );
            AutoMapper.Mapper.Initialize(x => {
                Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
                Ydb.Finance.Application.AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
                
                
            });

        }
      
    }
}
