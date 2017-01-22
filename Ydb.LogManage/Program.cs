using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Ydb.LogManage
{
    class Program
    {
        static Dictionary<string, string> lastTime = new Dictionary<string, string>();
        static Dictionary<string, int> changeTimes = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            Bootstrap.Boot();
            System.Threading.Timer timerRead = new System.Threading.Timer(new TimerCallback(ReadFile), null, 0, 3600000);
            //开始监视文件夹
            LogFileWatcher logFileWatcher = new LogFileWatcher("/logs", "*.*", changeTimes, timerRead);
            logFileWatcher.StartFileWatcher();

            Console.ReadKey();
        }

        static void ReadFile(object sender)
        {
            ReadFile readFile = new ReadFile(lastTime, changeTimes);
            readFile.StartReadFile();
        }

    }
}
