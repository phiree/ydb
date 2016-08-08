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
using Dianzhu.Model;
using System.Windows.Interop;
using System.Windows.Controls.Primitives;
using System.IO;
using System.ComponentModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// ChatList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ChatList : UserControl,IView.IViewChatList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_ChatList");
        UC_Hint hint;
        public UC_ChatList()
        {
            InitializeComponent();

            ((StackPanel)svChatList.FindName("StackPanel")).SizeChanged += UC_ChatList_SizeChanged;
            hint = new UC_Hint(Btn_Click);
            ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(hint);
        }

        public string ChatListCustomerName
        {
            get { return lblUserName.Content.ToString(); }
            set
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    lblUserName.Content = value;
                }));
            }
        }

        private void UC_ChatList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer sc = ((ScrollViewer)svChatList.FindName("ScrollViewer"));
            if (sc.ScrollableHeight == sc.VerticalOffset)
            {
                sc.ScrollToEnd();
            }
        }

        IList<ReceptionChat> chatList = new List<ReceptionChat>();
        public IList<ReceptionChat> ChatList
        {
            get
            {
                return chatList;
            }

            set
            {
                chatList = value;
                //Action lambda = () =>
                //{
                //    chatList = value;
                //    ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();

                //    if (chatList == null)
                //    {
                //        chatList = new List<ReceptionChat>();
                //        return;
                //    }
                    
                //    //pnlChatList.Children.Clear();
                    
                //    if (chatList.Count > 0)
                //    {
                //        Button btn = new Button();
                //        btn.Content = "查看更多";
                //        btn.Click += Btn_Click;

                //        ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(btn);//加载更多按钮

                //        foreach (ReceptionChat chat in chatList)
                //        {
                //            AddOneChat(chat);
                //        }
                        
                //        tbkHint.Visibility = Visibility.Collapsed;
                //    }
                //};
                //if (!Dispatcher.CheckAccess())
                //{
                //    Dispatcher.Invoke(lambda);
                //}
                //else { lambda(); }
            }
        }

        public void ShowMoreLabel()
        {
            Action lamda = () =>
            {
                HideLoadingMsg();
                hint.btnMore.Visibility = Visibility.Visible;
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        public void ShowNoMoreLabel()
        {
            Action lamda = () =>
            {
                hint.lblHint.Content = "——没有更多消息了——";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        public void HideLoadingMsg()
        {
            Action lamda = () =>
            {
                hint.lblHint.Content = string.Empty;
                hint.lblHint.Visibility = Visibility.Collapsed;
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        public void ShowLoadingMsg()
        {
            Action lamda = () =>
            {
                hint.lblHint.Content = "加载聊天记录中...";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }
        public void ShowNoChatMsg()
        {
            Action lamda = () =>
            {
                hint.lblHint.Content = "没有聊天记录";
                hint.lblHint.Visibility = Visibility.Visible;
                hint.btnMore.Visibility = Visibility.Collapsed;
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        BackgroundWorker worker;
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (BtnMoreChat==null)
            {
                return;
            }
            
            ShowLoadingMsg();

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            BtnMoreChat();
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        public void AddOneChat(ReceptionChat chat)
        {
            log.Debug("chat begin, body:" + chat.MessageBody);
            Action lamda = () =>
            {                
                UC_ChatCustomer chatCustomer = new UC_ChatCustomer(chat, currentCustomerService);
                
                // WindowNotification();
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(chatCustomer);
                //pnlChatList.Children.Add(pnlOneChat);

                log.Debug("chat end, body:" + chat.MessageBody);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        public void InsertOneChat(ReceptionChat chat)
        {
            Action lamda = () =>
            {
                UC_ChatCustomer chatCustomer = new UC_ChatCustomer(chat, currentCustomerService);
                
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Insert(1,chatCustomer);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }

        DZMembership currentCustomerService;        
        public event BtnMoreChat BtnMoreChat;

        public DZMembership CurrentCustomerService
        {
            get
            {
                return currentCustomerService;
            }

            set
            {
                currentCustomerService = value;
            }
        }

        public void ClearUCData()
        {
            Action lamda = () =>
            {
                this.ChatListCustomerName = string.Empty;
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(hint);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lamda);
            }
            else
            {
                lamda();
            }
        }
    }
}
