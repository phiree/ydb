using NUnit.Framework;
using JSYK.Infrastructure.ClassMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace JSYK.Infrastructure.ClassMapper.Tests
{
    [TestFixture()]
    public class DZMapperTests
    {
        [Test()]
        public void InitTest()
        {
            JSYK.Infrastructure.ClassMapper.DZMapper.Init();
            

            //config.AssertConfigurationIsValid();
        }
    }
}