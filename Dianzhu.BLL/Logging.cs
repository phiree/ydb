using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
namespace Dianzhu.BLL
{
    public static class Logging
    {
        static private log4net.ILog ilog;
        public static ILog Log
        {
            get
            {
                if (ilog == null)
                {
                    ilog = LogManager.GetLogger("Dianzhu.BLL");
                }
                if (ilog != null)
                {
                    return ilog;
                }
                else {
                    throw new Exception("Log 初始化失败。");
                }
            }
        }
    }
}
