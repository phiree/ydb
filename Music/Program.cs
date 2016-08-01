using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    static class Program
    { 
        /// <summary>
      /// The main entry point for the application.
      /// </summary>
        [STAThread]

        static void Main()
        {
            Mutex mutex = new Mutex(false, "Music");
            bool Running = !mutex.WaitOne(0, false);
            if (!Running)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Music());
            }
            else
                MessageBox.Show("Music已经启动！", "Music");
        }
    }
}
