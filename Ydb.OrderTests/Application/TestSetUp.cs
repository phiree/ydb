using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.OrderTests;
namespace Ydb.Order.Application.Tests
{
    [SetUpFixture]
    public class TestSetUp
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }
       
    }
}
