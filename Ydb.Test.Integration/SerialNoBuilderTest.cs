using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application.Dto;
using Dianzhu.ApplicationService;
using FizzWare.NBuilder;
using AutoMapper;
using Ydb.Common.Infrastructure;

namespace Ydb.Test.Integration
{
    [TestFixture]
    public class SerialNoBuilderTest
    {
        ISerialNoBuilder serialNoBuilder;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            serialNoBuilder = Bootstrap.Container.Resolve<ISerialNoBuilder>();
        }

        [Test]
        public void SerialNoBuilderTest_GetSerialNo()
        {
            Console.WriteLine("SerialNoBuilderTest.GetSerialNo:" + serialNoBuilder.GetSerialNo("AW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"), 2));
        }
    }
}
