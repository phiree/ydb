using NUnit.Framework;
using Ydb.LogManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpTestsEx;

namespace Ydb.LogManage.Tests
{
    [TestFixture()]
    public class SaveLogInfoTests
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
        }

        [Test()]
        public void SaveLastTime_FirstTime_Test()
        {
            SaveLogInfo saveLogInfo = new SaveLogInfo();
            string strGuid = System.Guid.NewGuid().ToString();
            strGuid = "Ydb.LogManage\\" + strGuid + ".log";
            saveLogInfo.SaveLastTime(strGuid, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"));
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            saveLogInfo.FindLastTimes(lastTime);
            bool b = lastTime.Keys.Contains(strGuid);
            Assert.IsTrue(b);
            Assert.AreEqual(DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"), lastTime[strGuid]);
        }

        [Test()]
        public void SaveLastTime_NotFirstTime_Test()
        {
            SaveLogInfo saveLogInfo = new SaveLogInfo();
            string strGuid = System.Guid.NewGuid().ToString();
            strGuid = "Ydb.LogManage\\" + strGuid + ".log";
            saveLogInfo.SaveLastTime(strGuid, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"));
            saveLogInfo.SaveLastTime(strGuid, "2017-01-17 10:27:50,383");
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            saveLogInfo.FindLastTimes(lastTime);
            bool b = lastTime.Keys.Contains(strGuid);
            Assert.IsTrue(b);
            Assert.AreEqual("2017-01-17 10:27:50,383", lastTime[strGuid]);
        }

        [Test()]
        public void IncApiCount_Test()
        {
            SaveLogInfo saveLogInfo = new SaveLogInfo();
            Type tSaveLogInfo = saveLogInfo.GetType();
            string apiRoute = "post/authorization";
            var documents = saveLogInfo.GetDocuments("ApiInfos", "ApiRoute", "post/authorization");
            var c = documents[0].GetElement("ApiRequestNum").Value.AsInt64;
            object[] ParamArray = new object[1];
            ParamArray[0] = apiRoute;
            tSaveLogInfo.InvokeMember("IncApiCount", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, saveLogInfo, ParamArray);

            documents = saveLogInfo.GetDocuments("ApiInfos", "ApiRoute", "post/authorization");
            var c1 = documents[0].GetElement("ApiRequestNum").Value.AsInt64;
            Assert.AreEqual(c+1,c1);
        }

        [Test()]
        public void AddRequestInfos_Test()
        {
            string strGuid = System.Guid.NewGuid().ToString();
            SaveLogInfo saveLogInfo = new SaveLogInfo();
            Type tSaveLogInfo = saveLogInfo.GetType();
            string apiRoute = "post/authorization";
            string message = "Info(UserInfo)1484620052088:ApiRoute=post/authorization,UserName=18889387677,UserId=,UserType=,RequestMethodUriSign="+ strGuid;
            object[] ParamArray = new object[2];
            ParamArray[0] = apiRoute;
            ParamArray[1] = message;
            tSaveLogInfo.InvokeMember("AddRequestInfos", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, saveLogInfo, ParamArray);

            var documents = saveLogInfo.GetDocuments("RequestInfos", "RequestMethodUriSign", strGuid);
            var c1 = documents[0].GetElement("RequestMethodUriSign").Value.AsString;
            Assert.AreEqual(strGuid, c1);
        }
    }
}