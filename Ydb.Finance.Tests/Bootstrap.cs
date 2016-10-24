using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ydb.Finance.Tests
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
                new Ydb.Finance.Infrastructure.InstallerFinance()
                );
        }


    }
}
