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

using DDDCommon.Domain;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

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
        container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>().LifestylePerWebRequest());
        container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(CreateNhSessionFactory).LifestylePerWebRequest());

        container.Register(Component.For<IRepository<Advertisement, Guid>,IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
        container.Register(Component.For<IRepository<Area, int>,IDALArea>().ImplementedBy<DALArea>());
        container.Register(Component.For<IBLLServiceOrder>().ImplementedBy<BLLServiceOrder>()
            .DependsOn(Dependency.OnValue("bllServiceOrderStateChangeHis", new BLLServiceOrderStateChangeHis()))
             .DependsOn(Dependency.OnValue("membershipProvider", new DZMembershipProvider()))
              .DependsOn(Dependency.OnValue("bllPayment", new BLLPayment()))
               .DependsOn(Dependency.OnValue("bllRefund", new BLLRefund()))

            );
        //  public BLLServiceOrder(  BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis, DZMembershipProvider membershipProvider,BLLPayment bllPayment,BLLRefund bllRefund,IDALServiceOrder repoServiceOrder)
    }
            private static ISessionFactory CreateNhSessionFactory()
    {
        var f =  Fluently.Configure()
                         .Database(
                              MySQLConfiguration
                             .Standard
                             .ConnectionString(
                                PHSuit.Security.Decrypt(
                                System.Configuration.ConfigurationManager
                                .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false)
                                      )
                                      .Dialect<NHCustomDialect>()
                           )
                         .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.CashTicketMap>())
                        .ExposeConfiguration(BuildSchema)
                         .BuildSessionFactory();
        HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        return f;
    }
    private static void BuildSchema(NHibernate.Cfg.Configuration config)
    {
        // this NHibernate tool takes a configuration (with mapping info in)
        // and exports a database schema from it
        SchemaUpdate update = new SchemaUpdate(config);
        //update.Execute(true, true);
    }




 
}