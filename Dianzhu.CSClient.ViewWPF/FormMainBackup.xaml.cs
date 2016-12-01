using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// FormMain.xaml 的交互逻辑
    /// </summary>
    public partial class FormMainBackup : Window
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF");
        Window main;

        private HwndSource _HwndSource;
        private const int WM_SYSCOMMAND = 0x112;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private Dictionary<ResizeDirection, Cursor> cursors = new Dictionary<ResizeDirection, Cursor>
        {
            {ResizeDirection.Top, Cursors.SizeNS},
            {ResizeDirection.Bottom, Cursors.SizeNS},
            {ResizeDirection.Left, Cursors.SizeWE},
            {ResizeDirection.Right, Cursors.SizeWE},
            {ResizeDirection.TopLeft, Cursors.SizeNWSE},
            {ResizeDirection.BottomRight, Cursors.SizeNWSE},
            {ResizeDirection.TopRight, Cursors.SizeNESW},
            {ResizeDirection.BottomLeft, Cursors.SizeNESW}
        };

        public FormMainBackup(IViewIdentityList viewIdentityList, IView.IViewChatList viewChatList, IViewChatSend viewChatSend,
            IViewSearch viewSearch, IViewSearchResult viewSearchResult, IViewOrderHistory viewOrderHistory,
            IViewNotice viewNotice,IViewToolsControl viewTabControl)
        {
            InitializeComponent();
            //pnlNotice.Children.Add((UC_Notice) viewNotice);
            pnlCustomerList.Children.Add((UC_IdentityList) viewIdentityList);
            pnlSearch.Children.Add((UC_Search) viewSearch);
            pnlChatList.Children.Add((UC_ChatList)viewChatList);
            pnlSearchResult.Children.Add((UC_SearchResult)viewSearchResult);
            pnlChatSend.Children.Add((UC_ChatSend)viewChatSend);
            pnlOrderHistory.Children.Add( (UC_OrderHistory)viewOrderHistory);
            pnlTools.Children.Add((UC_TabControlTools)viewTabControl);

            main = System.Windows.Window.GetWindow(this) as FormMain;

            this.SourceInitialized += delegate (object sender, EventArgs e)
            {
                this._HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            };

            this.MouseMove += new MouseEventHandler(Window_MouseMove);
        }

        public void CloseApplication()
        {
            Action lambda = () =>
            {
                this.Close();
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void btnWindowsClosed_Click(object sender, RoutedEventArgs e)
        {
            main.Close();
        }

        private void btnWindowsMin_Click(object sender, RoutedEventArgs e)
        {
            main.WindowState = WindowState.Minimized;
        }

        public string CSName
        {
            set
            {
                lblCSName.Content = value;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            main.DragMove();
        }

        public void FlashTaskBar()
        {
            Action lambda = () =>
            {
                FlashInTaskBar.FlashWindowEx(this);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }            
        }


        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null && !element.Name.Contains("Resize")) this.Cursor = Cursors.Arrow;
            }
        }

        private void ResizePressed(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));

            this.Cursor = cursors[direction];
            if (e.LeftButton == MouseButtonState.Pressed) ResizeWindow(direction);
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        public void AddIdentityTab(string identity, IViewSearch viewSearch, IViewSearchResult viewSearchResult, IViewChatList viewChatList, IViewChatSend viewChatSend, IViewOrderHistory viewOrderHistory, IViewToolsControl viewTabControl)
        {
            throw new NotImplementedException();
        }

        public void RemoveIdentityTab(string identity)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 鼠标方向
    /// </summary>
    //public enum ResizeDirection
    //{
    //    /// <summary>
    //    /// 左
    //    /// </summary>
    //    Left = 1,
    //    /// <summary>
    //    /// 右
    //    /// </summary>
    //    Right = 2,
    //    /// <summary>
    //    /// 上
    //    /// </summary>
    //    Top = 3,
    //    /// <summary>
    //    /// 左上
    //    /// </summary>
    //    TopLeft = 4,
    //    /// <summary>
    //    /// 右上
    //    /// </summary>
    //    TopRight = 5,
    //    /// <summary>
    //    /// 下
    //    /// </summary>
    //    Bottom = 6,
    //    /// <summary>
    //    /// 左下
    //    /// </summary>
    //    BottomLeft = 7,
    //    /// <summary>
    //    /// 右下
    //    /// </summary>
    //    BottomRight = 8,
    //}
}
