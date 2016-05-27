using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Dianzhu.CSClient.IView;
namespace Dianzhu.IntegrationTest.Bootstraper
{
    public class InstallerUI : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Dianzhu.CSClient.Presenter.LoginPresenter>());

           container.Register(Component.For<ILoginForm>().ImplementedBy<LoginView>().DependsOn(
                Dependency.OnValue("username","aa@aa.aa"),
                Dependency.OnValue("plainPassword","123456")
                ));
           
        }
    }

}
