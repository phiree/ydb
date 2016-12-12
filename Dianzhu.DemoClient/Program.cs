using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dianzhu.DemoClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //获取分配的客服.
            //PHSuit.Logging.Config("Dianzhu.DemoClient");
            Ydb.Common.LoggingConfiguration.Config();
            bool isValid= GlobalViables.CheckConfig();
            if (!isValid)
            {
                MessageBox.Show("配置冲突,即将退出");
                return;
            }
            Application.Run(new FmMain(args));
        }
    }
}
