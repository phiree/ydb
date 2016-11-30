using NUnit.Framework;
using Ydb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Tests
{
    [TestFixture()]
    public class LoggingConfigurationTests
    {
        [Test()]
        public void ConfigTest()
        {
            LoggingConfiguration.Config(string.Empty);

            log4net.ILog log = log4net.LogManager.GetLogger("Ydb.CommTest");

            log.Debug("aaaaaa");


        }
    }
}