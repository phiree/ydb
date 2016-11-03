using Castle.Windsor;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>
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
         
        container.Install(new Ydb.Infrastructure.Installer());
        container.Install(
                        new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
                        new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(container.Resolve<IEncryptService>()),
                        new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                        );
        // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
      


    }

}