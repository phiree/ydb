﻿using Castle.Windsor;
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
            new Dianzhu.DependencyInstaller.InstallerComponent(),
            new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
            new Dianzhu.DependencyInstaller.InstallerRepository(),
            new Dianzhu.DependencyInstaller.InstallerApplicationService(),


          new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
                   new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),

                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage(),
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(),

                new Ydb.Membership.Application.InstallerMembership(),
                 new Ydb.Membership.Application.InstallerMembershipDB(),
            new Ydb.Infrastructure.Installer()
             
            );
        Ydb.Membership.Application.AutoMapperConfiguration.Configure();
        

    }

}