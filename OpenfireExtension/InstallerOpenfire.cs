using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ydb.Common.Repository;
using Castle.Core;
using System;

namespace OpenfireExtension
{
    public class InstallerOpenfireExtension : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<OpenfireDbService>().WithService.DefaultInterfaces());
        }
    }
}