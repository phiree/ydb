﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Dianzhu.ApplicationService.Tests
{
 [SetUpFixture]
 public   class TestSetup
    {
        [SetUp]
        public void Setup()
        {
            Bootstrap.Boot();
        }
    }
}
