using Castle.MicroKernel.Registration;
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
using Ydb.Finance.Infrastructure.Repository;
using Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping;
using Ydb.Common.Repository;
using Castle.Core;

namespace Ydb.Finance.Infrastructure
{
    internal class InstallerFinance : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDomainService(container, store);
            InstallRepository(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBalanceFlow>().ImplementedBy<RepositoryBalanceFlow>());
            container.Register(Component.For<IRepositoryBalanceTotal>().ImplementedBy<RepositoryBalanceTotal>());
            container.Register(Component.For<IRepositoryServiceTypePoint>().ImplementedBy<RepositoryServiceTypePoint>());
            container.Register(Component.For<IRepositoryUserTypeSharePoint>().ImplementedBy<RepositoryUserTypeSharePoint>());

        }


        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
        }

    }
}
