﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.Presenter;
using FizzWare.NBuilder;
using Dianzhu.Model;
namespace Dianzhu.Test.DZCSClientTest
{
    [TestFixture]
    public class PGlobalTest
    {
        IViewIdentityList viewCustomerList;
        IViewChatList viewChatList;
        //PIdentityList pi;
        //PChatList pc;
        [SetUp]
        public void setup()
        {
            viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
            viewChatList = MockRepository.GenerateStub<IViewChatList>();
          //   pi = MockRepository.GenerateStub<PIdentityList>();
          //pc=  MockRepository.GenerateStub<PChatList>();

        }
    }
}
