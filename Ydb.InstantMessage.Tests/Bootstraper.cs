using Castle.Windsor;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
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
 
                    new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
                      new Ydb.InstantMessage.Tests.InstallerInstantMessageTestDB(),
                new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage()
               
                
 
                );
        }
        private static void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                update.Execute(true, true);
            }
        }



    }
}
