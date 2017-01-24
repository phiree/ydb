using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Ydb.LogManage
{
    public class LogFileWatcher
    {
        string strFolder = "";
        string strFilter = "";
        Dictionary<string, int> changeTimes;
        System.Threading.Timer timerRead;
        public LogFileWatcher(string strFolder, string strFilter, Dictionary<string, int> changeTimes, System.Threading.Timer timerRead)
        {
            this.strFolder = strFolder;
            this.strFilter = strFilter;
            this.changeTimes = changeTimes;
            this.timerRead = timerRead;
        }

        public void StartFileWatcher()
        {
            string strPath = Utils.GetRootPath() + strFolder;
            WatcherStrat(strPath, strFilter);
        }

        public void AddChangeTimes(string strName)
        {
            if (changeTimes.Keys.Contains(strName))
            {
                changeTimes[strName]++;
            }
            else
            {
                changeTimes.Add(strName, 1);
            }
            if (!ReadFile.IsRead)
            {
                timerRead.Change(0, 3600000);
            }
        }

        private void WatcherStrat(string path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.Deleted += new FileSystemEventHandler(OnProcess);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                OnChanged(source, e);
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                OnDeleted(source, e);
            }

        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            AddChangeTimes(e.Name);
            Console.WriteLine("文件新建事件处理逻辑 {0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            AddChangeTimes(e.Name);
            Console.WriteLine("文件改变事件处理逻辑{0}  {1}  {2}  {3}", DateTime.Now.ToString(), e.ChangeType, e.FullPath, e.Name);
            //try
            //{
            //    //string[] allLines = System.IO.File.ReadAllLines(e.FullPath);
            //    //string lastestLine = allLines[allLines.Length - 1];
            //    //Console.WriteLine(lastestLine);
            //    Console.WriteLine(getLastOne(e.FullPath));
            //    //ReadLastRowFromLog(e.FullPath);
            //}
            //catch
            //{ }
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            //AddChangeTimes(e.Name);
            //Console.WriteLine("文件删除事件处理逻辑{0}  {1}   {2}  {3}", e.ChangeType, e.FullPath, e.Name, c.ToString());
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            //AddChangeTimes(e.Name);
            //Console.WriteLine("文件重命名事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name, c.ToString());
        }
        
    }
}
