
using Castle.Windsor;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>
namespace DianzhuService.Diandian
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
                new Ydb.Infrastructure.Installer()
                );

           

            IEncryptService iEncryptService = container.Resolve<IEncryptService>();
            Ydb.Common.LoggingConfiguration.Config(iEncryptService.Decrypt(System.Configuration.ConfigurationManager
               .ConnectionStrings["MongoDB"].ConnectionString, false));
        }

    }
}