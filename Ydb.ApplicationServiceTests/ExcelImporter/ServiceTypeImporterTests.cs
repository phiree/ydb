using NUnit.Framework;
using Ydb.ApplicationService.ExcelImporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ExcelImporter.Tests
{
    [TestFixture()]
    public class ServiceTypeImporterTests
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }

        [Test()]
        public void ImportTest()
        {
            FileStream fs = File.Open("files/动词服务分类-(2015-8-19)-表格.xls", FileMode.Open);
          ServiceTypeImporter importer=Bootstrap.Container.Resolve<ServiceTypeImporter>();
            importer.Import(fs);
        }
    }
}