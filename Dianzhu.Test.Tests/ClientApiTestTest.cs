using System.IO;
// <copyright file="ClientApiTestTest.cs" company="Microsoft">Copyright © Microsoft 2015</copyright>

using System;
using Dianzhu.Test.DianzhuRestfulApi;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dianzhu.Test.DianzhuRestfulApi.Tests
{
    [TestClass]
    [PexClass(typeof(ClientApiTest))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ClientApiTestTest
    {

        [PexMethod]
        [PexAllowedException(typeof(FileNotFoundException))]
        public void Should_get_authorization_successfully([PexAssumeUnderTest]ClientApiTest target)
        {
            target.Should_get_authorization_successfully();
            // TODO: 将断言添加到 方法 ClientApiTestTest.Should_get_authorization_successfully(ClientApiTest)
        }
    }
}
