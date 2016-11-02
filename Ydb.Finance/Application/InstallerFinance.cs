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
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.Application
{
    public class InstallerFinance : IWindsorInstaller
    {
        FluentConfiguration config;
        public InstallerFinance(FluentConfiguration config)
        {
            this.config = config;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            Ydb.Finance.Infrastructure.Bootstrap.Boot(config);
            InstallApplicationService(container, store);
        }
        
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());
            container.Register(Component.For<IOrderShareService>().ImplementedBy<OrderShareService>());
            container.Register(Component.For<IServiceTypePointService>().ImplementedBy<ServiceTypePointService>());
            container.Register(Component.For<IUserTypeSharePointService>().ImplementedBy<UserTypeSharePointService>());
            container.Register(Component.For<IWithdrawCashService>().ImplementedBy<WithdrawCashService>());
        }

        
    }
}
