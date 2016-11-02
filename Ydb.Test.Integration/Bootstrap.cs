using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

namespace Ydb.Test.Integration
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
                new Ydb.Infrastructure.Installer(),
                //new Ydb.Finance.Application.InstallerFinanceDB(),
                new Ydb.Finance.Application.InstallerFinance(),
                new Ydb.Membership.Application.InstallerMembershipDB(),
                new Ydb.Membership.Application.InstallerMembership()
                );
        }
    }
}
