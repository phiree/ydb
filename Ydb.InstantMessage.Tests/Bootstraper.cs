using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Infrastructure;

namespace Ydb.InstantMessage.Tests
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
                 //new Ydb.InstantMessage.Tests.InstallerInstantMessageTestDB() //测试数据库
                 new InstallerIntantMessageDB() //本地数据库
                );
        }


    }
}
