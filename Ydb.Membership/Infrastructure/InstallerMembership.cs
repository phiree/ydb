using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel;
using Ydb.Membership.Infrastructure;
using Ydb.Membership.Infrastructure.Repository.NHibernate;
using Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping;
namespace Ydb.Membership.Application
{
    public class InstallerMembership : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
          //  new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        
        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryDZMembership>().ImplementedBy<RepositoryDZMembership>()
                .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("MembershipSessionFactory"))
                );
            container.Register(Component.For<IRepositoryUserToken>().ImplementedBy<RepositoryUserToken>()
                 .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("MembershipSessionFactory"))
                );

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipService>().ImplementedBy<DZMembershipService>());

        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
        }
      
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipDomainService>().ImplementedBy<DZMembershipDomainService>());

            container.Register(Component.For<ILoginNameDetermine>().ImplementedBy < LoginNameDetermine>());

        }

    }
    
}
