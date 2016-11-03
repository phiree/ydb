using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>
using Ydb.Common.Infrastructure;
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
            new Dianzhu.DependencyInstaller.InstallerComponent(),
            new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
            new Dianzhu.DependencyInstaller.InstallerRepository(),
            new Dianzhu.DependencyInstaller.InstallerApplicationService(),
        

            
             
            new Ydb.Infrastructure.Installer()
             
            );
    }
    

}