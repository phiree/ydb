using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ydb.Common.Infrastructure;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Castle.Core;
using Ydb.Common.Repository;

namespace Ydb.Infrastructure
{
    public class InstallerCommonWithoutDb: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>());
            container.Register(Component.For<IEncryptService>().ImplementedBy<EncryptService>());
            container.Register(Component.For<IHttpRequest>().ImplementedBy<HttpRequestImpl>());
            container.Register(Component.For<IDownloadAvatarToMediaServer>().ImplementedBy<DownloadAvatarToMediaServer>());
            container.Register(Component.For<IExcelReader>().ImplementedBy<ExcelReader>());
        }
        
    }
}
