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
        container.Register(Component.For<BLLServiceOrder>());
        container.Register(Component.For<BLLArea>());
        container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(CreateNhSessionFactory).LifestylePerWebRequest());
        container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>());
        container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NHRepositoryBase<,>)));
        //    container.Register( Component.For<NhUnitOfWorkInterceptor>().LifeStyle.Transient);

        container.Register(Component.For<IRepository<Advertisement, Guid>, IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
        container.Register(Component.For<IRepository<Area, int>, IDALArea>().ImplementedBy<DALArea>());
        container.Register(Component.For<IRepository<ServiceOrder, Guid>, IDALServiceOrder>().ImplementedBy<DALServiceOrder>());



    }
    private static ISessionFactory CreateNhSessionFactory()
    {
        var f = nhf.Fluently.Configure()
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