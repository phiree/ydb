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

namespace Ydb.Membership.Infrastructure
{
    internal class InstallerMembershipDB : IWindsorInstaller
    {
        FluentConfiguration config;
        public InstallerMembershipDB(FluentConfiguration config)
        {
            this.config = config;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new Ydb.Common.Depentcy.InstallerCommon().Install(container, store);
            //Database

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(config
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping.DZMembershipMap>())
               .BuildSessionFactory()));
        }



    }
}
