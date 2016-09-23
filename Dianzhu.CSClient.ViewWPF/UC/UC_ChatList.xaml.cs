﻿using System;
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

        private const string PRECHATCUSTOMER = "cc";

        UC_Hint hint;
        StackPanel stackPanel;
        public UC_ChatList()
        {
            InitializeComponent();

            stackPanel = ((StackPanel)svChatList.FindName("StackPanel"));

            stackPanel.SizeChanged += UC_ChatList_SizeChanged;
            hint = new UC_Hint(Btn_Click);
            stackPanel.Children.Add(hint);
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
                //    stackPanel.Children.Clear();

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

                //        stackPanel.Children.Add(btn);//加载更多按钮

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
            log.Debug("更多聊天信息加载完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            BtnMoreChat();
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        public void AddOneChat(ReceptionChat chat,string customerAvatar)
        {
            if (chat == null)
            {
                log.Warn("chat is null");
                return;
            }

            Action lamda = () =>
            {
                log.Debug("chat begin, chatId:" + chat.Id + ",body:" + chat.MessageBody);
                IViewChatCustomer chatCustomer = new UC_ChatCustomer()
                {
                    CurrentCS = currentCustomerService,
                    Chat = chat,
                    CustomerAvatar = customerAvatar
                };

                string chatId = PHSuit.StringHelper.SafeNameForWpfControl(chat.Id.ToString(), PRECHATCUSTOMER);
                if ((UC_ChatCustomer)stackPanel.FindName(chatId) == null)
                {
                    stackPanel.RegisterName(chatId, chatCustomer);
                }                
                stackPanel.Children.Add((UC_ChatCustomer)chatCustomer);

                log.Debug("chat begin, chatId:" + chat.Id + ",body:" + chat.MessageBody);
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

        public void InsertOneChat(ReceptionChat chat,string customerAvatar)
        {
            Action lamda = () =>
            {
                IViewChatCustomer chatCustomer = new UC_ChatCustomer()
                {
                    CurrentCS = currentCustomerService,
                    Chat = chat,
                    CustomerAvatar= customerAvatar
                };

                stackPanel.Children.Insert(1,(UC_ChatCustomer)chatCustomer);
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
                stackPanel.Children.Clear();
                stackPanel.Children.Add(hint);
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

        public void ShowChatImageNormalMask(Guid chatId)
        {
            Action lamda = () =>
            {
                try
                {
                    string id = PHSuit.StringHelper.SafeNameForWpfControl(chatId.ToString(), PRECHATCUSTOMER);
                    UC_ChatCustomer ucC = stackPanel.FindName(id) as UC_ChatCustomer;
                    UC_ChatImageNoraml ucCI = ucC.FindName(id) as UC_ChatImageNoraml;
                    ucCI.ShowMask();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
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

        public void RemoveChatImageNormalMask(Guid chatId)
        {
            Action lamda = () =>
            {
                try
                {
                    string id = PHSuit.StringHelper.SafeNameForWpfControl(chatId.ToString(), PRECHATCUSTOMER);
                    UC_ChatCustomer ucC = stackPanel.FindName(id) as UC_ChatCustomer;
                    UC_ChatImageNoraml ucCI = ucC.FindName(id) as UC_ChatImageNoraml;
                    ucCI.RemoveMask();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
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
