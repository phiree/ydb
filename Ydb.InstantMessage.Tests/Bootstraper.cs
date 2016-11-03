﻿using Castle.Windsor;
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
namespace Ydb.InstantMessage.Tests
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
            container.Install(
new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(container.Resolve<IEncryptService>()),
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                );
        }
        

    }
}
