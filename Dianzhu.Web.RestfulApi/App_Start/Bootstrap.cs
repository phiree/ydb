using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.BLL;
//using NHibernate;
using System.Configuration;
//using nhf = FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;
//using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor.Installer;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi
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
                new Dianzhu.DependencyInstaller.InstallerComponent(),
                new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
                new Dianzhu.DependencyInstaller.InstallerRepository(),
                new Dianzhu.DependencyInstaller.InstallerApplicationService(),
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage(),
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(),
                new InstallerRestfulApi()
                );

            Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            
        }
    }
}