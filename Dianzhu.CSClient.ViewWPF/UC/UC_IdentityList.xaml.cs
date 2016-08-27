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
using System.ComponentModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// IdentityList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_IdentityList : UserControl, IViewIdentityList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.ViewWPF.UC_IdentityList");

        public UC_IdentityList()
        {
            InitializeComponent();
        }
        

        #region 用户控件增删改

        public void AddIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)wpTopIdentityList.FindName(ctrlName);

                if (ucIdentity == null)
                {
                    ucIdentity = new UC_Customer(serviceOrder);
                    ucIdentity.btnCustomer.Tag = serviceOrder;
                    ucIdentity.Name = ctrlName;
                    ucIdentity.btnCustomer.Click += BtnIdentity_Click;
                    ucIdentity.IdleTimerOut += UcIdentity_IdleTimerOut;

                    InsertTopPanel(ucIdentity, ctrlName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void RemoveIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);

                if (ucIdentity != null)
                {
                    RemoveUIForNotTopPanel(ucIdentity, ctrlName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void UpdateIdentityBtnName(Guid oldOrderId, ServiceOrder newOrder)
        {
            Action lambda = () =>
            {
                string ctrOldlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrderId.ToString());
                string ctrNewlName = PHSuit.StringHelper.SafeNameForWpfControl(newOrder.Id.ToString());

                //UC_Customer btnOldIdentity = (UC_Customer)pnlIdentityList.FindName(ctrOldlName);
                UC_Customer btnOldIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrOldlName);

                if (btnOldIdentity != null)
                {
                    //注销
                    //pnlIdentityList.UnregisterName(btnOldIdentity.Name);
                    wpNotTopIdentityList.UnregisterName(btnOldIdentity.Name);

                    //重新注册
                    btnOldIdentity.Name = ctrNewlName;
                    //pnlIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);
                    wpNotTopIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);

                    //更新按钮的tag
                    btnOldIdentity.btnCustomer.Tag = newOrder;
                }
                else
                {
                    log.Error("错误：按钮不应该为null");
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        #endregion

        #region 更新用户控件对应的订单Id

        public void SetCustomerOrder(Guid oldOrderId,Guid newOrderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrderId.ToString());
                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);
                if (ucIdentity != null)
                {
                    ucIdentity.SetOrderTempData(newOrderId);
                    log.Debug("计时开始");
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        #endregion

        #region 时间控制器

        public event FinalChatTimerTick FinalChatTimerTick;

        public void IdleTimerStart(Guid orderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(orderId.ToString());
                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);
                if (ucIdentity != null)
                {
                    ucIdentity.TimerStart();
                    log.Debug("计时开始");
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void IdleTimerStop(Guid orderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(orderId.ToString());
                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)wpTopIdentityList.FindName(ctrlName);
                if (ucIdentity != null)
                {
                    ucIdentity.TimerStop();
                    log.Debug("计时停止");
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        private void UcIdentity_IdleTimerOut(Guid orderId)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac = () =>
            //{
                FinalChatTimerTick(orderId);
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        #endregion

        #region 点击用户控件相关事件

        public event IdentityClick IdentityClick;

        BackgroundWorker worker;
        ServiceOrder identityOrderTemp;
        public ServiceOrder IdentityOrderTemp
        {
            get { return identityOrderTemp; }
            set { identityOrderTemp = value; }
        }
        private void BtnIdentity_Click(object sender, RoutedEventArgs e)
        {
            if (IdentityClick != null)
            {
                ServiceOrder IdentityOrder = (ServiceOrder)((Button)sender).Tag;
                if (identityOrderTemp == null)
                {
                    identityOrderTemp = IdentityOrder;
                }
                else
                {
                    if (identityOrderTemp.Id == IdentityOrder.Id)
                    {
                        return;
                    }
                    else
                    {
                        identityOrderTemp = IdentityOrder;
                    }
                }
                SetIdentityReaded(IdentityOrder);



                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync(IdentityOrder);
                log.Debug("开始异步加载");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("异步加载完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ServiceOrder order = e.Argument as ServiceOrder;
            NHibernateUnitOfWork.UnitOfWork.Start();

            IdentityClick(order);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        #endregion

        #region 用户状态显示
        public void IdentityLogOnShowMsg(ServiceOrder serviceOrder,string msg)
        {
            Action lambda = () =>
            {
                if (serviceOrder == null)
                {
                    log.Debug("没有订单");
                    return;
                }

                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)FindName(ctrlName);

                if (ucIdentity != null)
                {
                    ucIdentity.tbkCustomerStatus.Text = msg;
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void IdentityLogOnShowMsgAndTimer(ServiceOrder serviceOrder, string msg)
        {
            Action lambda = () =>
            {
                if (serviceOrder == null)
                {
                    log.Debug("没有订单");
                    return;
                }
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)FindName(ctrlName);

                if (ucIdentity != null)
                {
                    ucIdentity.tbkCustomerStatus.Text = msg;
                    ucIdentity.TimeControlVisibility();
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void IdentityLogOffShowMsg(Guid serviceOrderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrderId.ToString());

                //UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)FindName(ctrlName);

                if (ucIdentity != null)
                {
                    ucIdentity.tbkCustomerStatus.Text = "该用户已下线";
                    ucIdentity.TimeControlCollapsed();
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }
        #endregion

        #region 设置用户控件的状态

        public void SetIdentityReaded(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Readed);

                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                UC_Customer u = (UC_Customer)wpTopIdentityList.FindName(ctrlName);
                if (u != null)
                {
                    if (wpTopIdentityList.Children.Contains(u))
                    {
                        RemoveUIForTopPanel(u, ctrlName);
                        InsertNotTopPanel(u, ctrlName);
                    }
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }

        public void SetIdentityUnread(ServiceOrder serviceOrder, int messageAmount)
        {
            Action lambda = () =>
            {
                SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Unread);

                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                UC_Customer u = (UC_Customer)wpTopIdentityList.FindName(ctrlName);
                if (u != null)
                {
                    if (wpTopIdentityList.Children.Contains(u))
                    {
                        RemoveUIForTopPanel(u, ctrlName);
                        InsertTopPanel(u, ctrlName);
                    }
                    else
                    {
                        RemoveUIForNotTopPanel(u, ctrlName);
                        InsertTopPanel(u, ctrlName);
                    }                    
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }
        public void SetIdentityLogOff(ServiceOrder serviceOrder)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.LogOff);
        }
        private void SetIdentityButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        {
            Action lambda = () =>
            {
                var objBtn = (UC_Customer)FindName(PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString()));
                if (objBtn != null)
                {
                    //Button btn = (Button)objBtn;
                    UC_Customer ucCustomer = (UC_Customer)objBtn;
                    Color foreColor = Colors.White;
                    string loadingText = "(加载中)";
                    switch (buttonStyle)
                    {
                        case em_ButtonStyle.Login:
                            foreColor = Colors.Green;
                            break;
                        case em_ButtonStyle.LogOff:
                            foreColor = Colors.Gray;
                            //btn.Visibility = Visibility.Collapsed;
                            //pnlIdentityList.Children.Remove(btn);
                            //pnlIdentityList.Children.Remove(ucCustomer);
                            break;
                        case em_ButtonStyle.Readed:
                            foreColor = Colors.Black;
                            //btn.Content = btn.Content.ToString().Replace(loadingText, string.Empty);
                            break;
                        case em_ButtonStyle.Unread:
                            foreColor = Colors.Red;
                            ucCustomer.CustomerUnread();
                            break;
                        case em_ButtonStyle.Actived:
                            foreColor = Colors.Yellow;
                            break;
                        case em_ButtonStyle.Loading:
                            //btn.Content = loadingText+btn.Content;
                            ucCustomer.CustomerCurrent();
                            break;
                        default: break;
                    }
                    //btn.Foreground = new SolidColorBrush(foreColor);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }

        }

        public void SetIdentityLoading(ServiceOrder serviceOrder)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Loading);
        }

        #endregion

        #region 用户头像列表相关UI方法
        /// <summary>
        /// 新消息需置顶
        /// </summary>
        /// <param name="u"></param>
        public void InsertTopPanel(UC_Customer u,string registerName)
        {
            Action lambda = () =>
            {
                if (!wpTopIdentityList.Children.Contains(u))
                {
                    wpTopIdentityList.Width += u.Width;
                    wpTopIdentityList.Children.Insert(0, u);
                    wpTopIdentityList.RegisterName(registerName, u);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }
        /// <summary>
        /// 已读的消息从置顶列表中清除
        /// </summary>
        /// <param name="u"></param>
        public void RemoveUIForTopPanel(UC_Customer u, string registerName)
        {
            Action lambda = () =>
            {
                if (wpTopIdentityList.Children.Contains(u))
                {
                    wpTopIdentityList.Width -= u.Width;
                    wpTopIdentityList.Children.Remove(u);
                    wpTopIdentityList.UnregisterName(registerName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }
        /// <summary>
        /// 已读消息放到非置顶列表
        /// </summary>
        /// <param name="u"></param>
        public void InsertNotTopPanel(UC_Customer u, string registerName)
        {
            Action lambda = () =>
            {
                if (!wpNotTopIdentityList.Children.Contains(u))
                {
                    wpNotTopIdentityList.Children.Insert(0, u);
                    wpNotTopIdentityList.RegisterName(registerName, u);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }
        /// <summary>
        /// 从非置顶列表删除用户
        /// </summary>
        /// <param name="u"></param>
        public void RemoveUIForNotTopPanel(UC_Customer u, string registerName)
        {
            Action lambda = () =>
            {
                if (wpNotTopIdentityList.Children.Contains(u))
                {
                    wpNotTopIdentityList.Children.Remove(u);
                    wpNotTopIdentityList.UnregisterName(registerName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }
        /// <summary>
        /// 用户控件从置顶区移到非置顶区
        /// </summary>
        /// <param name="order"></param>
        public void UIFromTopToNotTop(ServiceOrder order)
        {
            Action lambda = () =>
            {
                string ctrName = PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString());

                UC_Customer btnIdentity = (UC_Customer)wpTopIdentityList.FindName(ctrName);
                if (btnIdentity != null)
                {
                    RemoveUIForTopPanel(btnIdentity, ctrName);
                    InsertNotTopPanel(btnIdentity, ctrName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }

        /// <summary>
        /// 用户控件从非置顶区移到置顶区
        /// </summary>
        /// <param name="order"></param>
        public void UIFromNotTopToTop(ServiceOrder order)
        {
            Action lambda = () =>
            {
                string ctrName = PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString());

                UC_Customer btnIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrName);
                if (btnIdentity != null)
                {
                    RemoveUIForNotTopPanel(btnIdentity, ctrName);
                    InsertTopPanel(btnIdentity, ctrName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }
        }

        public UC_Customer FindUIByName(string name)
        {
            UC_Customer u = null;

            Action lambda = () =>
            {
                u = (UC_Customer)wpTopIdentityList.FindName(name);
                if (u == null)
                {
                    u = (UC_Customer)wpNotTopIdentityList.FindName(name);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }

            return u;
        }

        #endregion
    }
}
