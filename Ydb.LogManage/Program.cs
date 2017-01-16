using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ydb.LogManage
{
    class Program
    {
        static void Main(string[] args)
        {
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo info = new DirectoryInfo(strPath);
            strPath = info.Parent.Parent.Parent.FullName + "/logs";
            WatcherStrat(strPath, "*.*");
            Console.ReadKey();
        }

        private static void WatcherStrat(string path, string filter)
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

        private static void OnProcess(object source, FileSystemEventArgs e)
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
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件新建事件处理逻辑 {0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件改变事件处理逻辑{0}  {1}  {2}  {3}",DateTime.Now.ToString(), e.ChangeType, e.FullPath, e.Name);
            try
            {
                string[] allLines = System.IO.File.ReadAllLines(e.FullPath);
                string lastestLine = allLines[allLines.Length - 1];
                Console.WriteLine(lastestLine);
                //ReadLastRowFromLog(e.FullPath);
            }
            catch
            { }
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("文件删除事件处理逻辑{0}  {1}   {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("文件重命名事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
        }
    }
}
