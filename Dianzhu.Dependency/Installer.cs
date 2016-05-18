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
using NHibernate;
using System.Configuration;
using nhf = FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
namespace Dianzhu.DependencyInstaller
{
    /// <summary>
    /// Summary description for Installer
    /// </summary>
    public class Installer
    {
        static WindsorContainer container;
        public static WindsorContainer Container
        {
            get { return container; }
        }
        static Installer()
        {
            container = new WindsorContainer();

            container.Register(Component.For<BLLAdvertisement>());
             container.Register(Component.For<BLLArea>());
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NHRepositoryBase<,>)));

            container.Register(Component.For<IRepository<Advertisement, Guid>, IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
            container.Register(Component.For<IRepository<Area, int>, IDALArea>().ImplementedBy<DALArea>());
            container.Register(Component.For<IRepository<ServiceOrder, Guid>, IDALServiceOrder>().ImplementedBy<DALServiceOrder>());
            
            container.Register(Component.For<IBLLServiceOrder>().ImplementedBy<BLLServiceOrder>()
       .DependsOn(Dependency.OnValue("bllServiceOrderStateChangeHis", new BLLServiceOrderStateChangeHis()))
       .DependsOn(Dependency.OnValue("membershipProvider", new DZMembershipProvider()))
       .DependsOn(Dependency.OnValue("bllPayment", new BLLPayment()))
       .DependsOn(Dependency.OnValue("bllRefund", new BLLRefund()))
       );



        }
    }
}