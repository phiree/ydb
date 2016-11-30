using NUnit.Framework;
using Ydb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Infrastructure.Tests
{
    [TestFixture()]
    public class EncryptServiceTests
    {
        [Test()]
        public void EncryptTestWithPrefixComment()
        {
            var encryptService = new EncryptService();
            string rawString = "150___server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsTrue(encrypted.StartsWith("150___"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
        [Test()]
        public void EncryptTestWithoutPrefixComment()
        {
            var encryptService = new EncryptService();
            string rawString = "server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsFalse(encrypted.StartsWith("150____"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
        [Test()]
        public void EncryptTestWithEmptyPrefixComment()
        {
            var encryptService = new EncryptService();
            string rawString = "___server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsFalse(encrypted.StartsWith("___"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
    }
}