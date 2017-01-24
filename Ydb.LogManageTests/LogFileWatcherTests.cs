using NUnit.Framework;
using Ydb.LogManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ydb.LogManage.Tests
{
    [TestFixture()]
    public class LogFileWatcherTests
    {
        string strTest = "";
        //[SetUp]
        //public void SetUp()
        //{

        //}

        [Test()]
        public void AddChangeTimes_IsReadIsFalse_NameNotExists_Test()
        {
            strTest = "ReadFileTest";
            System.Threading.Timer timerRead = new System.Threading.Timer(new TimerCallback(ReadFileTest), null, 0, 3600000);
            System.Threading.Thread.Sleep(1000);
            ReadFile.IsRead = false;
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            LogFileWatcher logFileWatcher = new LogFileWatcher("/logs", "*.*", changeTimes, timerRead);
            logFileWatcher.AddChangeTimes("test.log");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, changeTimes.Count);
            Assert.AreEqual(1, changeTimes["test.log"]);
            Assert.AreEqual("ReadFileTest11", strTest);
        }

        [Test()]
        public void AddChangeTimes_IsReadIsFalse_NameIsExists_Test()
        {
            strTest = "ReadFileTest";
            System.Threading.Timer timerRead = new System.Threading.Timer(new TimerCallback(ReadFileTest), null, 0, 3600000);
            System.Threading.Thread.Sleep(1000);
            ReadFile.IsRead = false;
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            changeTimes.Add("test.log", 1);
            LogFileWatcher logFileWatcher = new LogFileWatcher("/logs", "*.*", changeTimes, timerRead);
            logFileWatcher.AddChangeTimes("test.log");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, changeTimes.Count);
            Assert.AreEqual(2, changeTimes["test.log"]);
            Assert.AreEqual("ReadFileTest11", strTest);
        }

        [Test()]
        public void AddChangeTimes_IsReadIsTrue_NameNotExists_Test()
        {
            strTest = "ReadFileTest";
            System.Threading.Timer timerRead = new System.Threading.Timer(new TimerCallback(ReadFileTest), null, 0, 3600000);
            System.Threading.Thread.Sleep(1000);
            ReadFile.IsRead = true;
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            LogFileWatcher logFileWatcher = new LogFileWatcher("/logs", "*.*", changeTimes, timerRead);
            logFileWatcher.AddChangeTimes("test.log");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, changeTimes.Count);
            Assert.AreEqual(1, changeTimes["test.log"]);
            Assert.AreEqual("ReadFileTest1", strTest);
        }

        [Test()]
        public void AddChangeTimes_IsReadIsTrue_NameIsExists_Test()
        {
            strTest = "ReadFileTest";
            System.Threading.Timer timerRead = new System.Threading.Timer(new TimerCallback(ReadFileTest), null, 0, 3600000);
            System.Threading.Thread.Sleep(1000);
            ReadFile.IsRead = true;
            Dictionary<string, int> changeTimes = new Dictionary<string, int>();
            changeTimes.Add("test.log", 1);
            LogFileWatcher logFileWatcher = new LogFileWatcher("/logs", "*.*", changeTimes, timerRead);
            logFileWatcher.AddChangeTimes("test.log");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, changeTimes.Count);
            Assert.AreEqual(2, changeTimes["test.log"]);
            Assert.AreEqual("ReadFileTest1", strTest);
        }

        private void ReadFileTest(object sender)
        {
            strTest = strTest+ "1";
        }
    }
}