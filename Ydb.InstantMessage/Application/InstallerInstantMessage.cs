﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Windsor;
using Ydb.InstantMessage.DomainModel;
using Ydb.InstantMessage.Application;

namespace Ydb.InstantMessage.Application
{
    public class InstallerInstantMessage : IWindsorInstaller
    {
        string dbType;
        string connectionString;

        public InstallerInstantMessage(string dbType,
        string connectionString)
        {
            this.connectionString = connectionString;
            this.dbType = dbType;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Ydb.InstantMessage.Infrastructure.Bootstrap.Boot(dbType,connectionString);
            InstallApplicationService(container, store);
        }

        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IReceptionService>().ImplementedBy<Ydb.InstantMessage.Application.ReceptionService>());
            container.Register(Component.For<IChatService>().ImplementedBy<Ydb.InstantMessage.Application.ChatService>());
        }


    }
}
