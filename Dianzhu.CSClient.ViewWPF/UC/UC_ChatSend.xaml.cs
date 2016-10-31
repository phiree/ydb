using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.IO;
using RisCaptureLib;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_ChatSend.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatSend : UserControl,IView.IViewChatSend
    {
        private readonly ScreenCaputre screenCaputre = new ScreenCaputre();
        private Size? lastSize;

        public event SaveMessageText SaveMessageText;

        public UC_ChatSend()
        {
            InitializeComponent();

            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;

            //启动键盘钩子   
            if (hKeyboardHook == 0)
            {
                //实例化委托  
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public string MessageText
        {
            get
            {
                string msg = string.Empty;
                this.Dispatcher.Invoke((Action)(() =>
                {
                    msg= tbxTextMessage.Text;

                }));
                return msg;
               
            }

            set
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tbxTextMessage.Text = value;

                }));
               
            }
        }

        public string MessageTimer
        {
            get { return timerMsg.Text; }
            set { timerMsg.Text = value; }
        }

        public event SendTextClick SendTextClick;
        public event SendMediaClick SendMediaClick;
        public event FinalChatTimerSend FinalChatTimerSend;

        private void btnSendTextMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SendTextClick != null && MessageText.Trim() != string.Empty)
            {
                SendText();
            }
        }

        Button btnMediaSender;
        private void btnSendMedia_Click(object sender, RoutedEventArgs e)
        {
                btnMediaSender = (Button)sender;
               SendMedia();
            
        }

        private void WorkerSendMedia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblSendingMsg.Content = string.Empty;
        }
        Microsoft.Win32.OpenFileDialog dlg;
        string domain = string.Empty;
        string mediaType = string.Empty;
        private void SendMedia()
        {
            if (SendMediaClick != null)
            {

                // Create OpenFileDialog
                 dlg = new Microsoft.Win32.OpenFileDialog();

                Button btn = btnMediaSender;
             

                if (btn.Name == "btnSendImage")
                {
                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = ".png;.jpg";
                    dlg.Filter = "Images |*.jpg";
                    domain = "ChatImage";
                    mediaType = "image";
                }
                else if (btn.Name == "btnSendAudio")
                {
                    dlg.DefaultExt = ".wma;.mp3";
                    //dlg.Filter = "Audios |*.mp3";
                    dlg.Filter = "Audios |*.*";
                    domain = "ChatAudio";
                    mediaType = "voice";
                }

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    BackgroundWorker workerSendMedia = new BackgroundWorker();
                    workerSendMedia.DoWork += WorkerSendMedia_DoWork;
                    workerSendMedia.RunWorkerCompleted += WorkerSendMedia_RunWorkerCompleted;
                    workerSendMedia.RunWorkerAsync();
                    

                }
               
            }
        }
        private void WorkerSendMedia_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                lblSendingMsg.Content = "正在发送,请稍后";
               
            }));
            byte[] fileData = File.ReadAllBytes(dlg.FileName);

            //NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () => {
                SendMediaClick(fileData, domain, mediaType);
                FinalChatTimerSend();
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        #region 截图

        BitmapSource bmp;
        Window win;
        Window mainForm;
        private void btnCaptureImage_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
            //((Window)((Grid)((Grid)((WrapPanel)this.Parent).Parent).Parent).Parent).Hide();
            //Window mainForm = (Window)((Grid)((Grid)((Grid)((Grid)((Grid)this.Parent).Parent).Parent).Parent).Parent).Parent;
            if (mainForm == null)
            {
                mainForm = (Window)((Grid)((Grid)((Grid)((Grid)((Grid)((Grid)this.Parent).Parent).Parent).Parent).Parent).Parent).Parent;
            }
            //mainForm.Visibility = Visibility.Collapsed;
            mainForm.WindowState = WindowState.Minimized;
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            //mainForm.Visibility = Visibility.Visible;
            mainForm.WindowState = WindowState.Normal;
            //Show();
            Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);

            //mainForm.Visibility = Visibility.Visible;
            mainForm.WindowState = WindowState.Normal;
            //Show();

            //test
            bmp = e.Bmp;
            win = new Window { SizeToContent = SizeToContent.WidthAndHeight, ResizeMode = ResizeMode.NoResize,WindowStartupLocation=WindowStartupLocation.CenterScreen };

            var canvas = new Canvas { Width = bmp.Width, Height = bmp.Height + 50, MaxHeight = 650, MaxWidth = 800 };
            var canvasSure = new StackPanel { Width = bmp.Width, Height = bmp.Height + 50, MaxHeight = 650, MaxWidth = 800 };
            var imageSure = new Image { Width = bmp.Width, Height = bmp.Height, Source = bmp, MaxHeight = 600, MaxWidth = 800 };
            var btnSure = new Button { Width = 100, Height = 40, Name = "btnSendCapture", Content = "确认" };
            btnSure.Click += BtnSure_Click;
            canvasSure.Children.Add(imageSure);
            canvasSure.Children.Add(btnSure);
            canvas.Children.Add(canvasSure);

            win.Content = canvas;
            win.Show();
        }

        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            
            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            win.Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] bytes=null;
            this.Dispatcher.Invoke((Action)(() =>
            {
                win.Title = "正在发送,请稍后........";
                bytes= BitmapSource2ByteArray(bmp);
            }));

            //NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () =>
            //{
                SendMediaClick(bytes, "ChatImage", "image");
            FinalChatTimerSend();
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        private Byte[] BitmapSource2ByteArray(BitmapSource source)
        {
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();

            encoder.Save(stream);
            return stream.ToArray();
        }
        #endregion

        private void tbxTextMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (SendTextClick != null && MessageText.Trim() != string.Empty && e.Key == Key.Enter && (Keyboard.Modifiers & (ModifierKeys.Control)) == (ModifierKeys.Control))
            {
                SendText();
            }
        }

        private void SendText()
        {
            //NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () =>
            //{
            SendTextClick();
                FinalChatTimerSend();
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }


        #region 键盘钩子

        /// 声明回调函数委托  
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>  
        /// 委托实例  
        /// </summary>  
        HookProc KeyboardHookProcedure;

        /// <summary>  
        /// 键盘钩子句柄  
        /// </summary>  
        static int hKeyboardHook = 0;

        //装置钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //获取某个进程的句柄函数  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>  
        /// 普通按键消息  
        /// </summary>  
        private const int WM_KEYDOWN = 0x100;
        /// <summary>  
        /// 系统按键消息  
        /// </summary>  
        private const int WM_SYSKEYDOWN = 0x104;

        //鼠标常量   
        public const int WH_KEYBOARD_LL = 13;

        //声明键盘钩子的封送结构类型   
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //表示一个在1到254间的虚似键盘码   
            public int scanCode; //表示硬件扫描码   
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        /// <summary>  
        /// 截取全局按键，发送新按键，返回  
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                System.Windows.Forms.Keys keyData = (System.Windows.Forms.Keys)MyKeyboardHookStruct.vkCode;

                if (keyData == System.Windows.Forms.Keys.Q && (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Alt)
                {
                    //Thread.Sleep(300);
                    screenCaputre.StartCaputre(30, lastSize);

                    //return为了屏蔽原来的按键，如果去掉，则原来的按键和新的按键都会模拟按。  
                    return 1;
                }
            }
            return 0;
        }

        #endregion

        private void tbxTextMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveMessageText != null)
            {
                SaveMessageText("MessageText", MessageText);
            }
        }
    }
}
