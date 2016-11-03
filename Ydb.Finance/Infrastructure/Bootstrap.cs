using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
//using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
//领域内部的container.
using Ydb.Common.Infrastructure;
namespace Ydb.Finance.Infrastructure
{
    internal class Bootstrap
    {

       
        static IWindsorContainer container;
        public static IWindsorContainer Container
        {
            get { return container; }
            private set { container = value; }
        }
        public static void Boot(string dbType,string connectionString)
        {
            container = new WindsorContainer();
            //container=container1;
            container.Install(
            new Ydb.Finance.Infrastructure.InstallerFinanceDB(dbType,connectionString),
            new Ydb.Finance.Infrastructure.InstallerFinance()
                );
            Ydb.Finance.Application.AutoMapperConfiguration.Configure();
        }

    }
}
