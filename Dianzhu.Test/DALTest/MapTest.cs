using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentNHibernate.Testing;

namespace Dianzhu.Test.DALTest
{
    [TestFixture]
    public class MapTest
    {
        [SetUp]
        public void setup()
        {
            Bootstrap.Boot();
        }
        [Test]
        public void CanCorrectlyMapEmployee()
        {
             
        }
    }
}
