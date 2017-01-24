using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ydb.LogManage
{
    public class Utils
    {
        public static string GetRootPath()
        {
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo info = new DirectoryInfo(strPath);
            strPath = info.Parent.Parent.Parent.FullName;
            return strPath;
        }
    }
}
