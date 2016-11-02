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
using Ydb.Membership.DomainModel;

namespace Ydb.Membership.Application
{
    public class InstallerMembership : IWindsorInstaller
    {
        FluentConfiguration config;
        public InstallerMembership(FluentConfiguration config)
        {
            this.config = config;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            Ydb.Membership.Infrastructure.Bootstrap.Boot(config);
            InstallApplicationService(container, store);
        }
        
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipService>().ImplementedBy<DZMembershipService>());
        }


    }
}
