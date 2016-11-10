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
using System.ComponentModel;
using System.Media;
using Dianzhu.CSClient.ViewModel;

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

        public void AddIdentity(VMIdentity vmIdentity)
        {
            Action lambda = () =>
            {
                string cbtnName = PHSuit.StringHelper.SafeNameForWpfControl(vmIdentity.CustomerId,GlobalVariable.PRECBUTTON);
                var ucCustomer = (UC_Customer)wpNotTopIdentityList.FindName(cbtnName);
                if (ucCustomer == null)
                {
                    IViewCustomer c = new UC_Customer()
                    {
                        AvatarImage = vmIdentity.CustomerAvatarUrl,
                        CustomerName = vmIdentity.CustomerName,
                        CustomerReceptionStatus = enum_CustomerReceptionStatus.Unread,
                        Identity = vmIdentity
                    };
                    c.CustomerClick += C_CustomerClick;
                    c.IdleTimerOut += C_IdleTimerOut;

                    AddUIForTopPanel((UC_Customer)c, cbtnName);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        private void C_IdleTimerOut(string customerId)
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                FinalChatTimerTick(customerId);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }        

        public void RemoveIdentity(string customerId)
        {
            Action lambda = () =>
            {
                string cbtnName = PHSuit.StringHelper.SafeNameForWpfControl(customerId, GlobalVariable.PRECBUTTON);
                var ucCustomer = (UC_Customer)wpNotTopIdentityList.FindName(cbtnName);
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

        public void UpdateIdentityBtnName(string oldCustomerId, VMIdentity vmIdentity)
        {
            Action lambda = () =>
            {
                string ctrOldlName = PHSuit.StringHelper.SafeNameForWpfControl(oldCustomerId, GlobalVariable.PRECBUTTON);
                string ctrNewlName = PHSuit.StringHelper.SafeNameForWpfControl(vmIdentity.CustomerId,GlobalVariable.PRECBUTTON);
                
                var btnOldCustomer = (UC_Customer)wpNotTopIdentityList.FindName(ctrOldlName);

                if (btnOldCustomer != null)
                {
                    //注销
                    wpNotTopIdentityList.UnregisterName(ctrOldlName);

                    //重新注册
                    wpNotTopIdentityList.RegisterName(ctrNewlName, btnOldCustomer);

                    //更新
                    btnOldCustomer.Identity = vmIdentity;
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

        #region 时间控制器

        public event FinalChatTimerTick FinalChatTimerTick;

        public void IdleTimerStart(string customerId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(customerId,GlobalVariable.PRECBUTTON);

                var ucCutomer = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);
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

        public void IdleTimerStop(string customerId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(customerId, GlobalVariable.PRECBUTTON);

                var ucCutomer = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);
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

        private void UcIdentity_IdleTimerOut(string customerId)
        {
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Start();
                FinalChatTimerTick(customerId);
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
            finally
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            }
        }

        #endregion

        #region 点击用户控件相关事件

        public event IdentityClick IdentityClick;

        BackgroundWorker worker;
        string identityCustomerTempId;
        public string IdentityCustomerTempId
        {
            get { return identityCustomerTempId; }
            set { identityCustomerTempId = value; }
        }

        private void C_CustomerClick(VMIdentity vmIdentity)
        {
            if (IdentityClick != null)
            {
                VMIdentity Identity = vmIdentity;
                if (identityCustomerTempId == null)
                {
                    identityCustomerTempId = Identity.CustomerId;
                }
                else
                {
                    if (identityCustomerTempId == Identity.CustomerId)
                    {
                        return;
                    }
                    else
                    {
                        identityCustomerTempId = Identity.CustomerId;
                    }
                }
                SetIdentityReaded(Identity.CustomerId);



                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync(Identity);
                log.Debug("开始异步加载");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("异步加载完成");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                VMIdentity vmIdentity = e.Argument as VMIdentity;
                
                IdentityClick(vmIdentity);
            }
            catch (Exception ee)
            {
                log.Error(ee.ToString());
            }
        }

        #endregion

        #region 设置用户控件的状态

        /// <summary>
        /// 已读后，用户控件从置顶区移到非置顶区
        /// </summary>
        /// <param name="customerId"></param>
        public void SetIdentityReaded(string customerId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(customerId, GlobalVariable.PRECBUTTON);

                var ucCustomer = (UC_Customer)wpNotTopIdentityList.FindName(ctrlName);
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

        /// <summary>
        /// 收到未读消息，用户控件从非置顶区移到置顶区
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="messageAmount"></param>
        public void SetIdentityUnread(string customerId, int messageAmount)
        {
            Action lambda = () =>
            {
                string ctrlNameNew = PHSuit.StringHelper.SafeNameForWpfControl(customerId, GlobalVariable.PRECBUTTON);
                var u = (UC_Customer)wpTopIdentityList.FindName(ctrlNameNew);
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
        public void AddUIForTopPanel(UC_Customer u,string registerName)
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
        
        #endregion

        #region 播放提示音

        MediaPlayer player = new MediaPlayer();
        public void PlayVoice()
        {
            Action lambda = () =>
            {
                //player.Open(new Uri(System.Environment.CurrentDirectory + @"\Resources\YDBan.wav"));
                ////player.Open(new Uri("E:\\projects\\output\\csclient\\Resources\\YDBan.wav"));
                //player.Play();

                SoundPlayer audio = new SoundPlayer(Properties.Resources.YDBan);
                audio.Play();
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
