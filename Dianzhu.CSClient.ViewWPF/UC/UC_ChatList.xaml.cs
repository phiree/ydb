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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// ChatList.xaml 的交互逻辑
    /// </summary>

    public partial class UC_ChatList : UserControl,IView.IViewChatList
    {
        public UC_ChatList()
        {
            InitializeComponent();

            ((StackPanel)svChatList.FindName("StackPanel")).SizeChanged += UC_ChatList_SizeChanged;
        }

        public string ChatListCustomerName
        {
            get { return lblUserName.Content.ToString(); }
            set
            {
                lblUserName.Content = value;
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
                Action lambda = () =>
                {
                    chatList = value;
                    ((StackPanel)svChatList.FindName("StackPanel")).Children.Clear();

                    if (chatList == null)
                    {
                        chatList = new List<ReceptionChat>();
                        return;
                    }
                    
                    //pnlChatList.Children.Clear();
                    
                    if (chatList.Count > 0)
                    {
                        Button btn = new Button();
                        btn.Content = "查看更多";
                        btn.Click += Btn_Click;

                        ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(btn);//加载更多按钮

                        foreach (ReceptionChat chat in chatList)
                        {
                            AddOneChat(chat);
                        }
                    }
                };
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(lambda);
                }
                else { lambda(); }
            }
        }

        public void ShowNoMoreLabel()
        {
            Label lbl = new Label();
            lbl.Content = "——没有更多消息了——";
            lbl.HorizontalAlignment = HorizontalAlignment.Center;

            ((StackPanel)svChatList.FindName("StackPanel")).Children.RemoveAt(0);
            ((StackPanel)svChatList.FindName("StackPanel")).Children.Insert(0, lbl);
        }


        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (BtnMoreChat==null)
            {
                return;
            }
            Action ac = () =>
            {
                BtnMoreChat();
            };
            NHibernateUnitOfWork.With.Transaction(ac);
        }

        public void AddOneChat(ReceptionChat chat)
        {
            Action lamda = () =>
            {
                UC_ChatCustomer chatCustomer = new UC_ChatCustomer(chat, currentCustomerService);
                
                // WindowNotification();
                ((StackPanel)svChatList.FindName("StackPanel")).Children.Add(chatCustomer);
                //pnlChatList.Children.Add(pnlOneChat);

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
            this.ChatList = null;
            this.ChatListCustomerName = string.Empty;
        }
    }
}
