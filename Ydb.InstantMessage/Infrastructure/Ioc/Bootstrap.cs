using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
//using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Ydb.InstantMessage.Infrastructure
{
    internal class Bootstrap
    {
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot(string dbType, string connectionString)
        {
            container = new WindsorContainer();
            //container=container1;
            container.Install(
            new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(dbType,connectionString),
            new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage()
                );
           
        }

    }
}
