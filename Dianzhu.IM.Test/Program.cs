using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dianzhu.IM.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Ydb.Common.LoggingConfiguration.Config("Dianzhu.IMTest");
            //Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
            Application.Run(new Main());
        }
    }
}
