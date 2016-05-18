using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using System.Configuration;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
using System;
namespace Dianzhu.DependencyInstaller
{
    public class DianzhuDependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            
            //Register all components
            container.Register(

                //Nhibernate session factory
                Component.For<ISessionFactory>().UsingFactoryMethod(CreateNhSessionFactory).LifeStyle.Singleton,

                Component.For(typeof(Dianzhu.IDAL.IRepository<,>)).ImplementedBy(typeof(Dianzhu.DAL.NHRepositoryBase<,>)),
                //Unitofwork interceptor
              //  Component.For<NhUnitOfWorkInterceptor>().LifeStyle.Transient,
                Component.For<Dianzhu.IDAL.IUnitOfWork>().ImplementedBy<Dianzhu.DAL.NHUnitOfWork>(),
                  Component.For<IDALArea,NHRepositoryBase<Area,int>>().ImplementedBy<DALArea>(),
                 Component.For<IDALAdvertisement, NHRepositoryBase<Advertisement, Guid>>().ImplementedBy<DALAdvertisement>(),
                  Component.For<IDALServiceOrder, NHRepositoryBase<ServiceOrder, Guid>>().ImplementedBy<DALServiceOrder>(),

                //All repoistories
                // Classes.FromAssembly(Assembly.GetAssembly(typeof(NhPersonRepository))).InSameNamespaceAs<NhPersonRepository>().WithService.DefaultInterfaces().LifestyleTransient(),
                //Classes.FromAssembly(Assembly.GetAssembly(typeof( IDALAdvertisement))).InSameNamespaceAs<Dianzhu.DAL.IDALAdvertisement>()
                // .WithService.DefaultInterfaces().LifestyleTransient(),

                //All services
                Classes.FromAssembly(Assembly.GetAssembly(typeof(Dianzhu.BLL.IBLLServiceOrder)))
                .InSameNamespaceAs<Dianzhu.BLL.IBLLServiceOrder>().WithService.DefaultInterfaces().LifestyleTransient()

                );
        }

        /// <summary>
        /// Creates NHibernate Session Factory.
        /// </summary>
        /// <returns>NHibernate Session Factory</returns>
        private static ISessionFactory CreateNhSessionFactory()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PhoneBook"].ConnectionString;
            //var f12= Fluently.Configure()
            //    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr))
            //    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(PersonMap))))

            //    .BuildSessionFactory();
            var f = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString(
                               connStr
                                     )
                                     
                          )
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.AdvertisementMap>())
                         .ExposeConfiguration(BuildSchema)
                        .BuildSessionFactory();
          NHibernateProfiler.Initialize();
            return f;
        }
                           
 
    private static void BuildSchema(NHibernate.Cfg.Configuration config)
    {
        // this NHibernate tool takes a configuration (with mapping info in)
        // and exports a database schema from it
        SchemaUpdate update = new SchemaUpdate(config);
        update.Execute(true, true);
    }
    void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            //Intercept all methods of all repositories.
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
            }

            //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(NhUnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }
}