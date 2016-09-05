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

        private readonly string preCutomerButton = "c";

        public UC_IdentityList()
        {
            InitializeComponent();
        }
        

        #region 用户控件增删改

        public void AddIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                //string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                ////UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                //UC_Customer ucIdentity = (UC_Customer)wpTopIdentityList.FindName(ctrlName);

                //if (ucIdentity == null)
                //{
                //    ucIdentity = new UC_Customer(serviceOrder);
                //    ucIdentity.btnCustomer.Tag = serviceOrder;
                //    ucIdentity.Name = ctrlName;
                //    ucIdentity.btnCustomer.Click += BtnIdentity_Click;
                //    ucIdentity.IdleTimerOut += UcIdentity_IdleTimerOut;

                //    InsertTopPanel(ucIdentity, ctrlName);

                    
                //}

                string cbtnName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString(), preCutomerButton);
                var ucCustomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(cbtnName);
                if (ucCustomer == null)
                {
                    IViewCustomer c = new UC_CustomerNew()
                    {
                        AvatarImage = serviceOrder.Customer.AvatarUrl,
                        CustomerName = serviceOrder.Customer.DisplayName,
                        CustomerReceptionStatus = enum_CustomerReceptionStatus.Unread,
                        Order = serviceOrder
                    };
                    c.CustomerClick += C_CustomerClick;
                    c.IdleTimerOut += C_IdleTimerOut;

                    AddUIForTopPanel((UC_CustomerNew)c, cbtnName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        private void C_IdleTimerOut(Guid orderId)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();

            FinalChatTimerTick(orderId);

            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        private void C_CustomerClick(ServiceOrder order)
        {
            if (IdentityClick != null)
            {
                ServiceOrder IdentityOrder = order;
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

        public void RemoveIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                //string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());

                ////UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                //UC_Customer ucIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);

                //if (ucIdentity != null)
                //{
                //    RemoveUIForNotTopPanel(ucIdentity, ctrlName);
                //}


                string cbtnName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString(), preCutomerButton);
                var ucCustomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(cbtnName);
                if (ucCustomer != null)
                {
                    RemoveUIForNotTopPanel(ucCustomer, cbtnName);
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
                //string ctrOldlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrderId.ToString());
                //string ctrNewlName = PHSuit.StringHelper.SafeNameForWpfControl(newOrder.Id.ToString());

                ////UC_Customer btnOldIdentity = (UC_Customer)pnlIdentityList.FindName(ctrOldlName);
                //UC_Customer btnOldIdentity = (UC_Customer)wpNotTopIdentityList.FindName(ctrOldlName);

                //if (btnOldIdentity != null)
                //{
                //    //注销
                //    //pnlIdentityList.UnregisterName(btnOldIdentity.Name);
                //    wpNotTopIdentityList.UnregisterName(btnOldIdentity.Name);

                //    //重新注册
                //    btnOldIdentity.Name = ctrNewlName;
                //    //pnlIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);
                //    wpNotTopIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);

                //    //更新按钮的tag
                //    btnOldIdentity.btnCustomer.Tag = newOrder;
                //}
                //else
                //{
                //    log.Error("错误：按钮不应该为null");
                //}

                string ctrOldlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrderId.ToString(),preCutomerButton);
                string ctrNewlName = PHSuit.StringHelper.SafeNameForWpfControl(newOrder.Id.ToString(),preCutomerButton);
                
                var btnOldCustomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrOldlName);

                if (btnOldCustomer != null)
                {
                    //注销
                    wpNotTopIdentityList.UnregisterName(btnOldCustomer.Name);

                    //重新注册
                    btnOldCustomer.Name = ctrNewlName;
                    wpNotTopIdentityList.RegisterName(btnOldCustomer.Name, btnOldCustomer);

                    //更新order
                    btnOldCustomer.Order = newOrder;
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

        public void SetCustomerOrder(ServiceOrder oldOrder,ServiceOrder newOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrder.Id.ToString(),preCutomerButton);

                var ucCustomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCustomer != null)
                {
                    ucCustomer.Order = newOrder;
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
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(orderId.ToString(),preCutomerButton);

                var ucCutomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCutomer != null)
                {
                    ucCutomer.StartFinalChatTimer();
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
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(orderId.ToString(), preCutomerButton);

                var ucCutomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCutomer != null)
                {
                    ucCutomer.StopFinalChatTimer();
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
                
        #region 设置用户控件的状态

        public void SetIdentityReaded(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString(), preCutomerButton);

                var ucCustomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCustomer != null)
                {
                    if (wpTopIdentityList.Children.Contains(ucCustomer))
                    {
                        RemoveUIForTopPanel(ucCustomer, ctrlName);
                        InsertNotTopPanel(ucCustomer, ctrlName);
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
                //SetIdentityButtonStyle(serviceOrder, enum_CustomerReceptionStatus.Unread);

                //string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                //UC_Customer u = (UC_Customer)wpTopIdentityList.FindName(ctrlName);
                //if (u != null)
                //{
                //    if (wpTopIdentityList.Children.Contains(u))
                //    {
                //        RemoveUIForTopPanel(u, ctrlName);
                //        InsertTopPanel(u, ctrlName);
                //    }
                //    else
                //    {
                //        RemoveUIForNotTopPanel(u, ctrlName);
                //        InsertTopPanel(u, ctrlName);
                //    }                    
                //}


                string ctrlNameNew = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString(), preCutomerButton);
                var u = (UC_CustomerNew)wpTopIdentityList.FindName(ctrlNameNew);
                if (u != null)
                {
                    u.CustomerReceptionStatus = enum_CustomerReceptionStatus.Unread;

                    if (!wpTopIdentityList.Children.Contains(u))
                    {
                        RemoveUIForNotTopPanel(u, ctrlNameNew);
                        AddUIForTopPanel(u, ctrlNameNew);
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

        #endregion

        #region 用户头像列表相关UI方法
        /// <summary>
        /// 新消息需置顶
        /// </summary>
        /// <param name="u"></param>
        public void AddUIForTopPanel(UC_CustomerNew u,string registerName)
        {
            Action lambda = () =>
            {
                if (!wpTopIdentityList.Children.Contains(u))
                {
                    wpTopIdentityList.Width += u.Width;
                    wpTopIdentityList.Children.Add(u);
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
        public void RemoveUIForTopPanel(UC_CustomerNew u, string registerName)
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
        public void InsertNotTopPanel(UC_CustomerNew u, string registerName)
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
        public void RemoveUIForNotTopPanel(UC_CustomerNew u, string registerName)
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
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString(), preCutomerButton);

                var ucCutomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCutomer != null)
                {
                    RemoveUIForTopPanel(ucCutomer, ctrlName);
                    InsertNotTopPanel(ucCutomer, ctrlName);
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
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString(), preCutomerButton);

                var ucCutomer = (UC_CustomerNew)wpNotTopIdentityList.FindName(ctrlName);
                if (ucCutomer != null)
                {
                    RemoveUIForNotTopPanel(ucCutomer, ctrlName);
                    AddUIForTopPanel(ucCutomer, ctrlName);
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

        #endregion

        #region 播放提示音

        MediaPlayer player = new MediaPlayer();
        public void PlayVoice()
        {
            Action lambda = () =>
            {
                player.Open(new Uri(System.Environment.CurrentDirectory + @"\Resources\YDBan.wav"));
                //player.Open(new Uri("E:\\projects\\output\\csclient\\Resources\\YDBan.wav"));
                player.Play();
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

        #endregion
    }
}
