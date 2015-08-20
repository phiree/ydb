using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dianzhu.CSClient
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
            log4net.Config.XmlConfigurator.Configure();
            DialogResult result;
            using (var loginForm = new fmLogin())
            {
                  result = loginForm.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
            //Application.Run(new Views.Raw.ChatView());

            
        }
    }
}
