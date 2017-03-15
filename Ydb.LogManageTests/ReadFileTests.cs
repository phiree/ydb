using NUnit.Framework;
using Ydb.LogManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Ydb.LogManageTests
{
    [TestFixture()]
    public class ReadFileTests
    {
        //[SetUp]
        //public void SetUp()
        //{

        //}

        [Test()]
        public void TryDeserialize_CanDeserialize_Test()
        {
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            string strJson = "{date:\"2017 - 01 - 17 14:53:38,401\",thread:\"10\",leve:\"DEBUG\",logger:\"Ydb.ResultWithChallenge.NoRule.v1.RestfulApi.Web.Dianzhu\",filename:\"E:\\projects\\dz\\Dianzhu.Web.RestfulApi\\Providers\\HMACAuthenticationAttribute.cs\",linenumber:\"494\",classname:\"Dianzhu.Web.RestfulApi.ResultWithChallenge +< ExecuteAsync > d__4\",domain:\" / LM / W3SVC / 11 / ROOT - 1 - 131291095357303392\"},message=Response(Content)1484636017589:{\"token\":\"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJsb2dpbk5hbWUiOiIxODg4OTM4NzY3NyIsInBhc3N3b3JkIjoic2NKNkdoZDBGY2VNVVVVZEhzTXFVb3JRMzlYc0FGRjZ1SWJsNHd4VVpHZyIsIlVzZXJUeXBlIjoiY3VzdG9tZXIiLCJVc2VySUQiOiIwNDA0YzRmOS0wNWJkLTRhMjctOTIwMy1hNjc0MDEwY2ZjMzYifQ.5cVgFPtTjyIFe8ib0_34VWTRsc4GlEh3e0G9K9oXoRg\",\"userEndpoint\":\"http://localhost:8041/api/v1/customers/0404c4f9-05bd-4a27-9203-a674010cfc36\"}CodeNo=200;CodeString=OK";
            log logTest = new log();
            object[] ParamArray = new object[2];
            ParamArray[0] = strJson;
            ParamArray[1] = logTest;
            ParameterModifier[] ParamMods = new ParameterModifier[2];
            ParamMods[0] = new ParameterModifier(2); // 初始化为接口参数的个数
            ParamMods[0][1] = true; // 设置第二个参数为返回参数
            bool b = (bool)tReadFile.InvokeMember("TryDeserialize", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray, ParamMods, null, null);
            Assert.IsFalse(b);
            Assert.AreEqual("2017 - 01 - 17 14:53:38,401", ((log)ParamArray[1]).date);
        }

        [Test()]
        public void TryDeserialize_CanNotDeserialize_Test()
        {
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            string strJson = "  \"password\": \"123456\"\n}";
            log logTest = new log();
            object[] ParamArray = new object[2];
            ParamArray[0] = strJson;
            ParamArray[1] = logTest;
            ParameterModifier[] ParamMods = new ParameterModifier[2];
            ParamMods[0] = new ParameterModifier(2); // 初始化为接口参数的个数
            ParamMods[0][1] = true; // 设置第二个参数为返回参数
            bool b = (bool)tReadFile.InvokeMember("TryDeserialize", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray, ParamMods, null, null);
            Assert.IsTrue(b);
        }

        [Test()]
        public void CheckFirstLine_FirstTime_Test()
        {
            //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"));
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            int FileCount = (int)tReadFile.InvokeMember("CheckFirstLine", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
       //     Assert.AreEqual(2, FileCount);
        }

        [Test()]
        public void CheckFirstLine_NotFirstTime_MoreFiles_SmallerAllFiles_Test()
        {  //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-15 16:57:40,487");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            int FileCount = (int)tReadFile.InvokeMember("CheckFirstLine", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
           // Assert.AreEqual(2, FileCount);
        }

        [Test()]
        public void CheckFirstLine_NotFirstTime_MoreFiles_SmallerOneFile_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-16 15:49:46,838");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            int FileCount = (int)tReadFile.InvokeMember("CheckFirstLine", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
         //   Assert.AreEqual(1, FileCount);
        }

        [Test()]
        public void CheckFirstLine_NotFirstTime_OneFiles_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-16 16:57:46,645");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            int FileCount = (int)tReadFile.InvokeMember("CheckFirstLine", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
            Assert.AreEqual(0, FileCount);
        }

        //总行数：278,47

        [Test()]
        public void GetNewLogs_FirstTime_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"));
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            IList<log> logList = (IList<log>)tReadFile.InvokeMember("GetNewLogs", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
            //Assert.AreEqual(325, logList.Count);
        }

        [Test()]
        public void GetNewLogs_NotFirstTime_MoreFiles_SmallerAllFiles_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-15 16:57:40,487");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            IList<log> logList = (IList<log>)tReadFile.InvokeMember("GetNewLogs", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
          //  Assert.AreEqual(325, logList.Count);
        }

        //278,15
        [Test()]
        public void GetNewLogs_NotFirstTime_MoreFiles_SmallerOneFile_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-16 15:49:46,838");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            IList<log> logList = (IList<log>)tReadFile.InvokeMember("GetNewLogs", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
           // Assert.AreEqual(293, logList.Count);
        }

        //259
        [Test()]
        public void GetNewLogs_NotFirstTime_OneFiles_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            lastTime.Add(strPath, "2017-01-16 16:57:46,645");
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            IList<log> logList = (IList<log>)tReadFile.InvokeMember("GetNewLogs", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
         //   Assert.AreEqual(259, logList.Count);
        }

        [Test()]
        public void ReadFileAllLines_Test()
        { //todo: 依赖于本地运行环境,需要修改.
            string strPath = "Ydb.LogManage\\LONGPHUI-PCYdb.log";
            Dictionary<string, string> lastTime = new Dictionary<string, string>();
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            Type tReadFile = readFile.GetType();
            object[] ParamArray = new object[1];
            ParamArray[0] = strPath;
            IList<string> lineList = (IList<string>)tReadFile.InvokeMember("ReadFileAllLines", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, readFile, ParamArray);
          //  Assert.AreEqual(485, lineList.Count);
        }

        
    }
}
