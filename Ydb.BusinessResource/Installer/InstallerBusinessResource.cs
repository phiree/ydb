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
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Infrastructure.Repository;
 
using Ydb.BusinessResource.Infrastructure.Repository.NHibernate;
 
using Ydb.BusinessResource.Application;

namespace Ydb.BusinessResource.Infrastructure
{
    public class InstallerBusinessResourceMessage : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //   new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            InstallInfrastructure(container, store);
            InstallDomainService(container, store);
            InstallRepository(container, store);
            InstallApplicationService(container, store);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositoryBusiness>().ImplementedBy<RepositoryBusiness>()
                 .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("BusinessResourceSessionFactory"))
                );
           

        }
        private void InstallApplicationService(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBusinessService>().ImplementedBy< BusinessService>());
          
        }
        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
           
        }
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
        private void InstallDomainService(IWindsorContainer container, IConfigurationStore store)
        {
             

        }

    }
}
