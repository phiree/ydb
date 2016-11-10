﻿using System;
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
           
            container.Install(
                new Ydb.Infrastructure.Installer()
                );


            FluentConfiguration dbConfigInstantMessage = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_InstantMessage.db3"));

            container.Install(
 
new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(dbConfigInstantMessage),
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                );


            FluentConfiguration dbConfigMembership = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("test_ydb_membership.db3"));
            container.Install(
 
               new Ydb.Membership.Infrastructure.InstallerMembership(),            
               new Ydb.Membership.Application.InstallerMembershipDB(dbConfigMembership)
                
 
                );
            AutoMapper.Mapper.Initialize(x => {
                Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
                Ydb.Finance.Application.AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
                
                
            });

        }
      
    }
}
