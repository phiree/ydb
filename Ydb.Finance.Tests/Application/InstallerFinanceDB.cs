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

namespace Ydb.Finance.Tests.Application
{
    public class InstallerFinanceDB : IWindsorInstaller
    {
        ISessionFactory _sessionFactory;
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            _sessionFactory = Ydb.Finance.Application.InstallerFinanceDB.GetSessionFactory(false);
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory));
            container.Register(Component.For<ISession>().Instance(_sessionFactory.OpenSession()));
        }
    }
}
