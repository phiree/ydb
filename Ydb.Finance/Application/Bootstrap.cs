using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

namespace Ydb.Finance.Application
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
            //container=container1;
            container.Install(
                new Ydb.Finance.Infrastructure.InstallerFinance()
                );
            AutoMapperConfiguration.Configure();
        }

    }
}
