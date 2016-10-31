using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Windsor;
using Ydb.InstantMessage.Infrastructure.UnitOfWork;
using Castle.Core;

namespace Ydb.InstantMessage.Infrastructure
{
    public class InstallerUnitOfWorkInstantMessage : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
            InstallRepository(container, store);

        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            if (!container.Kernel.HasComponent(typeof(IUnitOfWork)))//.HasComponent("IUnitOfWorkMembership" ))
            {
                container.Register(Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>()
                       .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                    );
            }

            if (!container.Kernel.HasComponent(typeof(NhUnitOfWorkInterceptor)))
            {
                container.Register(Component.For<NhUnitOfWorkInterceptor>()
                      .DependsOn(ServiceOverride.ForKey<ISessionFactory>().Eq("InstantMessageSessionFactory"))
                    .LifeStyle.Transient);
            }
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
