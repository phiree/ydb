
using Castle.Windsor;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>
namespace Dianzhu.DemoClient
{
    public class Bootstrap
    {
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot()
        {
            container = new WindsorContainer();


            container.Install(
                new Ydb.Infrastructure.InstallerCommonWithoutDb()
                );

            //IEncryptService iEncryptService = container.Resolve<IEncryptService>();
            //Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
            //   .ConnectionStrings["MongoDB"].ConnectionString, false));
            Ydb.Common.LoggingConfiguration.Config("Dianzhu.DemoClient");
        }

    }
}