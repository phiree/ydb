using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Ydb.Membership.Tests
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
                new Ydb.Membership.Application.InstallerMembership(),
                new Ydb.Infrastructure.Installer(),
                new Application.InstallerMembershipTestDB()
                );
            
            
        }


    }
}
