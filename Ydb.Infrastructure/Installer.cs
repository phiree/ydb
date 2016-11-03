using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ydb.Common.Infrastructure;
 
namespace Ydb.Infrastructure
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>());
            container.Register(Component.For<IEncryptService>().ImplementedBy<EncryptService>());
            container.Register(Component.For<IHttpRequest>().ImplementedBy<HttpRequestImpl>());
            container.Register(Component.For<IDownloadAvatarToMediaServer>().ImplementedBy<DownloadAvatarToMediaServer>());
        }

      

    }
}
