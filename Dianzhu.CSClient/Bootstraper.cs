﻿using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DependencyInstaller;

namespace Dianzhu.CSClient
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
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage(),
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(),
                new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
                new Ydb.Membership.Application.InstallerMembership(),
                new Ydb.Membership.Application.InstallerMembershipDB(),
                new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
                new InstallerComponent(),
                new InstallerInfrstructure(),
                new InstallerRepository(),
                new InstallerApplicationService(),
                new InstallerUI()
                );

          

        }


    }
}
