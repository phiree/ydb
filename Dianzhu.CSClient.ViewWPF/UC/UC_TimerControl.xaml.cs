using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TimerControl.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TimerControl : UserControl
    {
        DispatcherTimer timer;
        public UC_TimerControl()
        {
            InitializeComponent();
            
            LoadData();
        }

        public void LoadData()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void StartTimer()
        {
            if (timer != null)
            {
                timer.Start();
            }
        }

        public void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int minute = 0;
            int second = 0;

            int.TryParse(tbkMinute.Text, out minute);
            int.TryParse(tbkSecond.Text, out second);

            if (second < 9)
            {
                second++;
                tbkSecond.Text = "0" + second.ToString();                
            }
            else if (second < 59)
            {
                second++;
                tbkSecond.Text = second.ToString();
            }
            else
            {
                second = 0;
                tbkSecond.Text = "0" + second;

                minute++;
                tbkMinute.Text = minute.ToString();
            }
        }
    }
}
