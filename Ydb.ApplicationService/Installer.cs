using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

namespace Ydb.ApplicationService
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<Application.AgentService.FinanceFlowService>()
                              .WithService.DefaultInterfaces());
            container.Register(Classes.FromThisAssembly().InSameNamespaceAs<Application.AgentService.DataStatistics.StatisticsCount>()
                              .WithService.DefaultInterfaces());
            container.Register(Component.For<ExcelImporter.ServiceTypeImporter>());
        }
    }
}