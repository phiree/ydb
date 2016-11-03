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
using Ydb.Membership.DomainModel.Service;
using Ydb.Membership.Application;

namespace Ydb.Membership.Infrastructure
{
    public class InstallerMembership : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);

            AutoMapperConfiguration.Configure();
        }

        
        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryDZMembership>().ImplementedBy<RepositoryDZMembership>());
            container.Register(Component.For<IRepositoryUserToken>().ImplementedBy<RepositoryUserToken>());

        }
       
      
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipDomainService>().ImplementedBy<DZMembershipDomainService>());
            container.Register(Component.For<ILoginNameDetermine>().ImplementedBy < LoginNameDetermine>());
            container.Register(Component.For<ILogin3rd>().ImplementedBy<Login3rd>());
            
        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDZMembershipService>().ImplementedBy<DZMembershipService>());
        }

    }
    
}
