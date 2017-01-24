using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Ydb.LogManage
{
    public class ReadFile
    {
        Dictionary<string, string> lastTime ;
        Dictionary<string, int> changeTimes ;
        public static bool IsRead;

        public ReadFile(Dictionary<string, string> lastTime, Dictionary<string, int> changeTimes)
        {
            this.lastTime = lastTime;
            this.changeTimes = changeTimes;
            ReadFile.IsRead = true;
            if (lastTime.Count == 0)
            {
                new SaveLogInfo().FindLastTimes(lastTime);
            }
        }

        public void StartReadFile()
        {
            while (IsRead)
            {
                SaveLogInfo saveLogInfo = new SaveLogInfo();
                int lastReadTimes = 0;
                //foreach (KeyValuePair<string, int> item in changeTimes)
                for(int i=0;i< changeTimes.Count;i++)
                {
                    KeyValuePair<string, int> item = changeTimes.ElementAt(i);
                    if (item.Value > 0)
                    {
                        changeTimes[item.Key]--;
                        if (!lastTime.Keys.Contains(item.Key))
                        {
                            lastTime.Add(item.Key, DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss,000"));
                        }
                        saveLogInfo.SaveLog(GetNewLogs(item.Key));
                        saveLogInfo.SaveLastTime(item.Key, lastTime[item.Key]);
                    }
                    lastReadTimes = lastReadTimes + item.Value;
                }
                if (lastReadTimes == 0)
                {
                    IsRead = false;
                }
            }
        }

        private IList<log> GetNewLogs(string strPath)
        {
            int RollNum = CheckFirstLine(strPath);
            List<log> newLogs = new List<log>();
            string newTime = lastTime[strPath];
            for (int i = 0; i <= RollNum; i++)
            {
                string filePath = i == 0 ? strPath : strPath + "." + i.ToString();
                IList<string> allLines = ReadFileAllLines(filePath);
                if (allLines.Count > 0)
                {
                    int allCount = allLines.Count;
                    for (int j = allCount-1; j >= 0; j--)
                    {
                        int k = j;
                        string strLastLine = allLines[j];
                        log loggerInfo = new log();
                        while (TryDeserialize(strLastLine, out loggerInfo))
                        {
                            if (k < 0)
                            {
                                break;
                            }
                            k--;
                            strLastLine = allLines[k] + strLastLine;
                        }
                        if (loggerInfo.date.CompareTo(lastTime[strPath]) > 0)
                        {
                            if (newTime == lastTime[strPath])
                            {
                                newTime = loggerInfo.date;
                            }
                            loggerInfo.SetLogger1();
                            loggerInfo.SetLogger2();
                            loggerInfo.SetTimestamp();
                            newLogs.Add(loggerInfo);
                        }
                        j = k;
                    }
                }
            }
            lastTime[strPath] = newTime;
            return newLogs;
        }

        private int CheckFirstLine(string strPath)
        {
            int RollNum = 0;
            bool b = true;
            string filePath = strPath;
            while (b)
            {
                if (RollNum > 0)
                {
                    filePath = strPath + "." + RollNum.ToString();
                }
                IList<string> allLines = ReadFileAllLines(filePath);
                if (allLines.Count > 0)
                {
                    int i = 0;
                    string strFirstLine = allLines[i];
                    log loggerInfo = new log();
                    while (TryDeserialize(strFirstLine, out loggerInfo))
                    {
                        if (i > allLines.Count)
                        {
                            b = false;
                            break;
                        }
                        i++;
                        strFirstLine = strFirstLine + allLines[i];
                    }
                    if (b && loggerInfo.date.CompareTo(lastTime[strPath]) > 0)
                    {
                        RollNum++; 
                    }
                    else
                    {
                        b = false;
                    }
                }
                else
                {
                    b = false;
                }
            }
            return RollNum;
        }

        private bool TryDeserialize(string strJson,out log loggerInfo)
        {
            bool b = true;
            try
            {
                string[] strArr = strJson.Split(new[] { ",message=" }, StringSplitOptions.None);
                loggerInfo = JsonConvert.DeserializeObject<log>(strArr[0].Replace('\\','.'));
                loggerInfo.message = strArr[1];
                b = false;
                if (loggerInfo == null)
                {
                    b = true;
                }
            }
            catch(Exception ex)
            {
                loggerInfo = null;
                b = true;
            }
            return b;
        }

        private IList<string> ReadFileAllLines(string strPath)
        {
            strPath = Utils.GetRootPath() + "/logs/" + strPath;
            List<string> listFile = new List<string>();
            try
            {
                if (File.Exists(strPath))
                {
                    StreamReader objSr = new StreamReader(strPath, Encoding.Default);
                    while (!objSr.EndOfStream)
                    {
                        listFile.Add(objSr.ReadLine());
                    }
                    objSr.Dispose();
                }
            }
            catch
            { }
            return listFile;
        }

    }
}
