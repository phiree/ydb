using Castle.Windsor;

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
        container.Install(
            new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage(),
            new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(),
            new Ydb.Infrastructure.Installer()
            );
        

    }

}