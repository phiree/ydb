using Castle.Windsor;
using Castle.MicroKernel.Registration;
/// <summary>
/// Summary description for Installer
/// </summary>
public class Installer
{
    static WindsorContainer container;
    public static WindsorContainer Container
    {
        get { return container; }
    }
    static Installer()
    {

        container = Dianzhu.DependencyInstaller.Installer.Container;

        container.Register(Component.For<Dianzhu.NotifyCenter.IMNotify>());
        

    }
   
    
  
}