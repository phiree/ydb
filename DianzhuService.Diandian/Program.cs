using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DianzhuService.Diandian
{
    static class Program
    {
        static log4net.ILog  log = log4net.LogManager.GetLogger("Dianzhu.Diandian");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            //PHSuit.Logging.Config("Dianzhu.DianDian");
            Ydb.Common.LoggingConfiguration.Config();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ServiceDiandian sdd = new ServiceDiandian();
            if (Environment.UserInteractive)
            {

                sdd.TestStartupAndStop(args);
            }
            else
            { 
               
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
               sdd
            };
            ServiceBase.Run(ServicesToRun);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error("发生异常" + ((Exception)e.ExceptionObject).Message);
        }
    }
}
