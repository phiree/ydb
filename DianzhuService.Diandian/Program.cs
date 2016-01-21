﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DianzhuService.Diandian
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
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

       
    }
}
