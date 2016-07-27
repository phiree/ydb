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

        public event IdentityClick IdentityClick;
        public event FinalChatTimerTick FinalChatTimerTick;

        public void AddIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                //Button btnIdentity =(Button) pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                //if (btnIdentity == null)
                if (ucIdentity == null)
                {
                    //btnIdentity = new Button { Content = serviceOrder.Customer.DisplayName };
                    ucIdentity = new UC_Customer(serviceOrder);
                    //btnIdentity.Tag = serviceOrder;
                    ucIdentity.btnCustomer.Tag = serviceOrder;

                    //btnIdentity.Name = ctrlName;
                    ucIdentity.Name = ctrlName;
                    //btnIdentity.Click += BtnIdentity_Click;
                    ucIdentity.btnCustomer.Click += BtnIdentity_Click;
                    ucIdentity.IdleTimerOut += UcIdentity_IdleTimerOut;
                    //pnlIdentityList.Children.Add(btnIdentity);
                    pnlIdentityList.Children.Add(ucIdentity);
                    //pnlIdentityList.RegisterName(btnIdentity.Name, btnIdentity);
                    pnlIdentityList.RegisterName(ucIdentity.Name, ucIdentity);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void SetCustomerOrder(Guid oldOrderId,Guid newOrderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrderId.ToString());
                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
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

        public void IdleTimerStart(Guid orderId)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(orderId.ToString());
                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
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
                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
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
            Action ac = () =>
            {
                FinalChatTimerTick(orderId);
            };
            NHibernateUnitOfWork.With.Transaction(ac);
        }

        private void BtnIdentity_Click(object sender, RoutedEventArgs e)
        {
            if (IdentityClick != null)
            {
                //Action ac = () =>
                //{
                    ServiceOrder order = (ServiceOrder)((Button)sender).Tag;
                    //NHibernateUnitOfWork.UnitOfWork.Current.Refresh(order);
                    IdentityClick(order);
                    SetIdentityReaded(order);
                //};
                //NHibernateUnitOfWork.With.Transaction(ac);
            }
        }

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

                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);

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

                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);

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

                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);

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

        public void RemoveIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                //Button btnIdentity = (Button)pnlIdentityList.FindName(ctrlName);
                UC_Customer ucIdentity = (UC_Customer)pnlIdentityList.FindName(ctrlName);
                //if (btnIdentity != null)
                if (ucIdentity != null)
                {                    
                    //pnlIdentityList.Children.Remove(btnIdentity);
                    pnlIdentityList.Children.Remove(ucIdentity);
                    //pnlIdentityList.UnregisterName(btnIdentity.Name);
                    pnlIdentityList.UnregisterName(ucIdentity.Name);
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
                
                UC_Customer btnOldIdentity = (UC_Customer)pnlIdentityList.FindName(ctrOldlName);
                if (btnOldIdentity != null)
                {
                    //注销
                    pnlIdentityList.UnregisterName(btnOldIdentity.Name);

                    //重新注册
                    btnOldIdentity.Name = ctrNewlName;
                    pnlIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);

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

        public void SetIdentityReaded(ServiceOrder serviceOrder)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Readed);
        }

        public void SetIdentityUnread(ServiceOrder serviceOrder, int messageAmount)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Unread);
        }
        public void SetIdentityLogOff(ServiceOrder serviceOrder)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.LogOff);
        }
        private void SetIdentityButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        {
            Action lambda = () =>
            {
                var objBtn = pnlIdentityList.FindName(PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString()));
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
                            pnlIdentityList.Children.Remove(ucCustomer);
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
    }
}
