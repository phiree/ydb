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
                    ucIdentity = new UC_Customer(serviceOrder.Customer);
                    //btnIdentity.Tag = serviceOrder;
                    ucIdentity.btnCustomer.Tag = serviceOrder;

                    //btnIdentity.Name = ctrlName;
                    ucIdentity.Name = ctrlName;
                    //btnIdentity.Click += BtnIdentity_Click;
                    ucIdentity.btnCustomer.Click += BtnIdentity_Click;
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

        private void BtnIdentity_Click(object sender, RoutedEventArgs e)
        {
            if (IdentityClick != null)
            {
                ServiceOrder order = (ServiceOrder)((Button)sender).Tag;
                IdentityClick(order);
                SetIdentityReaded(order);
            }
        }

        public void RemoveIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                string ctrlName = PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                Button btnIdentity = (Button)pnlIdentityList.FindName(ctrlName);
                if (btnIdentity != null)
                {                    
                    pnlIdentityList.Children.Remove(btnIdentity);
                    pnlIdentityList.UnregisterName(btnIdentity.Name);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        public void UpdateIdentityBtnName(Guid oldOrder, Guid newOrder)
        {
            Action lambda = () =>
            {
                string ctrOldlName = PHSuit.StringHelper.SafeNameForWpfControl(oldOrder.ToString());
                string ctrNewlName = PHSuit.StringHelper.SafeNameForWpfControl(newOrder.ToString());
                Button btnOldIdentity = (Button)pnlIdentityList.FindName(ctrOldlName);
                if (btnOldIdentity != null)
                {
                    //注销
                    pnlIdentityList.UnregisterName(btnOldIdentity.Name);

                    //重新注册
                    btnOldIdentity.Name = ctrNewlName;
                    pnlIdentityList.RegisterName(btnOldIdentity.Name, btnOldIdentity);
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
