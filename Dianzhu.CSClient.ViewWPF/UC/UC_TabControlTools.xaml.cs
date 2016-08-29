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
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TabControlTools.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TabControlTools : UserControl, IView.IViewTabControl
    {
        bool loadCompleted = false;
        public event SetSearchAddress SetSearchAddress;

        public UC_TabControlTools()
        {
            InitializeComponent();

            Uri uriA = new Uri(@"http://localhost/map.html");
            webBaiduMap.Navigate(uriA);
            webBaiduMap.LoadCompleted += WebBaiduMap_LoadCompleted;
            ComVisibleObjectForScripting com = new ComVisibleObjectForScripting();
            webBaiduMap.ObjectForScripting = com;

            com.SetSearchAddressStr += Com_SetSearchAddress;
        }

        private void Com_SetSearchAddress(string address)
        {
            if (SetSearchAddress != null)
            {
                SetSearchAddress(address);
            }            
        }

        private void WebBaiduMap_LoadCompleted(object sender, NavigationEventArgs e)
        {
            loadCompleted = true;
        }
    }

    public delegate void SetSearchAddressStr(string address);
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ComVisibleObjectForScripting
    {
        public event SetSearchAddressStr SetSearchAddressStr;
        public void SetValue(string str)
        {
            if (SetSearchAddressStr != null)
            {
                SetSearchAddressStr(str);
            }
        }
    }
}
