using NUnit.Framework;
using DianzhuService.Diandian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;

namespace DianzhuService.Diandian.Tests
{
    [TestFixture()]
    public class ServiceDiandianTests
    {
        //Ydb.Common.Infrastructure.IHttpRequest httpRequest1;
        //[SetUp]
        //public void SetUp()
        //{
        //    httpRequest1 = MockRepository.GenerateMock<Ydb.Common.Infrastructure.IHttpRequest>();
        //    httpRequest1.Stub(x => x.CreateHttpRequest(System.Configuration.ConfigurationManager.AppSettings["CheckUri"].ToString(), "get", null)).Throw(new Exception("异常了！"));
        //}

        /// <summary>
        /// 测试消息不匹配
        /// </summary>
        [Test()]
        public void ServiceDiandian_CheckMessage_TestNotMatch_HttpTrue()
        {
            ServiceDiandian serviceDiandian = new ServiceDiandian();

            Ydb.Common.Infrastructure.IHttpRequest httpRequest = MockRepository.GenerateMock<Ydb.Common.Infrastructure.IHttpRequest>();
            httpRequest.Stub(x => x.CreateHttpRequest(System.Configuration.ConfigurationManager.AppSettings["CheckUri"].ToString(), "get", null)).Return("你好！");

            string result = serviceDiandian.CheckMessage("a",httpRequest);
            Console.WriteLine(result);
            Assert.AreEqual("当前没有客服在线，请留言..",result);
        }

        /// <summary>
        /// 测试消息匹配，且http请求成功
        /// </summary>
        [Test()]
        public void ServiceDiandian_CheckMessage_TestIsMatch_HttpTrue()
        {
            ServiceDiandian serviceDiandian = new ServiceDiandian();

            Ydb.Common.Infrastructure.IHttpRequest httpRequest = MockRepository.GenerateMock<Ydb.Common.Infrastructure.IHttpRequest>();
            httpRequest.Stub(x => x.CreateHttpRequest(System.Configuration.ConfigurationManager.AppSettings["CheckUri"].ToString(), "get", null)).Return("你好！");

            string result = serviceDiandian.CheckMessage("1", httpRequest);
            Console.WriteLine(result);
            Assert.AreEqual("你好！", result);
        }

        /// <summary>
        /// 测试消息匹配，且http请求不成功
        /// </summary>
        [Test()]
        public void ServiceDiandian_CheckMessage_TestIsMatch_HttpFalse()
        {
            ServiceDiandian serviceDiandian = new ServiceDiandian();

            Ydb.Common.Infrastructure.IHttpRequest httpRequest = MockRepository.GenerateMock<Ydb.Common.Infrastructure.IHttpRequest>();
            httpRequest.Stub(x => x.CreateHttpRequest(System.Configuration.ConfigurationManager.AppSettings["CheckUri"].ToString(), "get", null)).Throw(new Exception("异常了！"));

            string result = serviceDiandian.CheckMessage("1", httpRequest);
            Console.WriteLine(result);
            Assert.AreEqual("发生错误，请联系客服处理..", result);
        }
    }
}